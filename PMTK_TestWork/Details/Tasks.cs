using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace PMTK_TestWork.Details
{
    internal class Tasks
    {
        /// <summary>
        /// Создание таблицы с полями представляющими ФИО, дату рождения, пол
        /// </summary>
        public static void Task1()
        {
           // конструктор создаст бд и таблицу, если таковых не обнаружено
           new ApplicationContext();
        }

        /// <summary>
        /// Создание записи. Использовать следующий формат:
        /// myApp 2 ФИО ДатаРождения Пол
        /// </summary>
        public static void Task2(string[] parameters)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<string> parametersForPostgre = new List<string>();
                string name = "";

                // все элементы кроме двух последних (дата рождения и пол) относятся к имени
                foreach (var s in parameters.ToList().GetRange(0, parameters.Length - 2))
                {
                    name += " " + s;
                }
                parametersForPostgre.Add(name);
                parametersForPostgre.Add(parameters[parameters.Length - 2]);
                parametersForPostgre.Add(parameters[parameters.Length - 1]);
                db.Users.Add(new User(parametersForPostgre));
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Вывод всех строк с уникальным значением ФИО+дата, отсортированным по ФИО,
        /// вывести ФИО, Дату рождения, пол, кол-во полных лет.
        /// </summary>
        public static void Task3()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                FormattableString query = $@"
                SELECT DISTINCT ON (name, dateOfBirth)
	                name,
	                dateofbirth,
	                gender
                FROM public.users
                ORDER BY name";

                // вычисляем количество лет здесь чтобы не создавать дополнительный тип UserInfo,
                // а просто использовать анонимный тип
                var users = db.Users.FromSql(query).Select(user => new
                {
                    Name = user.Name,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                    Age = DateTime.Now.Year - user.DateOfBirth.Year
                })
                .ToList();

                foreach (var user in users)
                {
                    Console.WriteLine(user);
                }
            }
        }

        /// <summary>
        /// Заполнение автоматически 1000000 строк. 
        /// Распределение пола в них должно быть относительно равномерным, 
        /// начальной буквы ФИО также. 
        /// Заполнение автоматически 100 строк в которых пол мужской и ФИО начинается с "F".
        /// </summary>
        public static void Task4()
        {
            List<User> users = new List<User>();

            // генерируем пользователей
            for(var i  = 0; i < 1000000; ++i)
            {
                User user = new User();
                var res = SupportFuncs.generateRandomNameAndGender();
                user.Name = res.name; 
                user.Gender = res.gender;
                user.DateOfBirth = DateOnly.Parse(SupportFuncs.generateRandomDateOfBirth());

                users.Add(user);
            }

            // 100 мужчин с фамилией на F
            for (var i = 0; i < 100; ++i)
            {
                User user = new User();
                user.Name = SupportFuncs.generateRandomMaleStartsWithFName();
                user.Gender = "Male";
                user.DateOfBirth = DateOnly.Parse(SupportFuncs.generateRandomDateOfBirth());

                users.Add(user);
            }

            // Записываю всё сразу - плохо для оперативки, хорошо для скорости работы программы.
            // Можно было бы вставлять группами, скажем, по 50к-100к пользователей для экономии ОЗУ
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.AddRange(users.ToArray());
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Результат выборки из таблицы по критерию: пол мужской, 
        /// ФИО начинается с "F". 
        /// Сделать замер времени выполнения.
        /// </summary>
        public static void Task5()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                FormattableString query = $@"
                SELECT 
                    name,
	                dateofbirth,
	                gender
                FROM public.users
                WHERE gender = 'Male' AND LEFT(name, 1) = 'F'";

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var users = db.Users.FromSql(query).Select(user => new
                {
                    Name = user.Name,
                    DateOfBirth = user.DateOfBirth,
                    Gender = user.Gender,
                }).ToList();
                stopwatch.Stop();

                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
        }

        /// <summary>
        /// Произвести определенные манипуляции над базой данных для ускорения запроса из пункта 5. 
        /// Убедиться, что время исполнения уменьшилось. 
        /// Объяснить смысл произведенных действий. 
        /// Предоставить результаты замера до и после.
        /// </summary>
        public static void Task6()
        {
            // Создадим индекс по полям gender и name для оптимизации запроса
            using (ApplicationContext db = new ApplicationContext())
            {
                FormattableString createIndexSql = $@"CREATE INDEX idx_name_gender ON public.users (name, gender)";
                db.Database.ExecuteSql(createIndexSql);

                FormattableString query = $@"
                SELECT 
                    name,
	                gender
                FROM public.users
                WHERE gender = 'Male' AND LEFT(name, 1) = 'F'";

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                var users = db.Users.FromSql(query).Select(user => new
                {
                    Name = user.Name,
                    Gender = user.Gender
                }).ToList();
                stopwatch.Stop();

                Console.WriteLine(stopwatch.ElapsedMilliseconds);
            }
        }
    }
}

namespace PMTK_TestWork.Details
{
    internal class SupportFuncs
    {
        private static readonly Random random = new Random();

        private static readonly List<string> MaleFirstNameDictionary = new List<string>
        {
            "Ivan",
            "Aleksandr",
            "Dmitriy",
            "Andrey",
            "Sergey",
            "Mikhail",
            "Pavel",
            "Nikolay",
            "Maxim",
            "Artem",
            "Denis",
            "Anton",
            "Vladimir",
            "Konstantin",
            "Evgeniy"
        };

        private static readonly List<string> FemaleFirstNameDictionary = new List<string>
        {
            "Anna",
            "Mariya",
            "Yekaterina",
            "Olga",
            "Sofiya",
            "Aleksandra",
            "Yelena",
            "Natalya",
            "Viktoriya",
            "Irina",
            "Tatyana",
            "Yevgeniya",
            "Lyubov",
            "Alina",
            "Darya"
        };

        private static readonly List<string> LastNameDictionary = new List<string>
        {
            "Adamov",
            "Brodskiy",
            "Chernov",
            "Donskoy",
            "Evstratov",
            "Famusov",
            "Gergiev",
            "Hramovoy",
            "Ivanov",
            "Jukov", // правильнее Zhukov, но не придумал на букву J ничего
            "Kotov",
            "Lanskoy",
            "Miller",
            "Novikov",
            "Odintsov",
            "Podolskiy",
            "Qildeev", // опять же Kildeev, но не придумал на Q
            "Rokossovskiy",
            "Smirnov",
            "Trotskiy",
            "Ulyanov",
            "Vatutin",
            "Wolkov", // не придумал на W, поэтому Volkov
            "Xenofontov", // Ksenofontov ;)
            "Yakovlev",
            "Zhukov"
        };
        private static readonly List<string> PatronymicDictionary = new List<string>
        {
            "Ivanovich",
            "Aleksandrovich",
            "Dmitrievich",
            "Andreevich",
            "Sergeevich",
            "Mikhailovich",
            "Pavlovich",
            "Nikolaevich",
            "Maximovich",
            "Artemovich",
            "Denisovich",
            "Antonovich",
            "Vladimirovich",
            "Konstantinovich",
            "Evgenievich"
        };

        private static readonly List<string> LastFNames = new List<string>
        {
            "Fedorov",
            "Filatov",
            "Fomin",
            "Frolov",
            "Fokin",
            "Fomenko",
            "Frolov",
            "Fadeev",
            "Fursenko",
            "Fokanov",
            "Fofanov",
            "Frolovsky",
            "Fateev"
        };
        public static (string name, string gender) generateRandomNameAndGender()
        {
            string firstName;
            string gender;
            if (random.Next(2) == 0)
            {
                firstName = MaleFirstNameDictionary[random.Next(MaleFirstNameDictionary.Count)];
                gender = "Male";
            }
            else
            {
                firstName = FemaleFirstNameDictionary[random.Next(FemaleFirstNameDictionary.Count)];
                gender = "Female";
            }

            string lastName = LastNameDictionary[random.Next(LastNameDictionary.Count)];
            string patronymic = PatronymicDictionary[random.Next(PatronymicDictionary.Count)];

            return ($"{lastName} {firstName} {patronymic}", gender);
        }

        public static string generateRandomDateOfBirth()
        {
            int year = random.Next(1940, 2024);
            int month = year == 2023 ? random.Next(1, 5) : random.Next(1, 13);
            int day = random.Next(1, DateTime.DaysInMonth(year, month) + 1);

            return $"{year}-{month}-{day}";
        }
        public static string generateRandomMaleStartsWithFName()
        {
            string firstName = MaleFirstNameDictionary[random.Next(MaleFirstNameDictionary.Count)];
            string lastName = LastFNames[random.Next(LastFNames.Count)];
            string patronymic = PatronymicDictionary[random.Next(PatronymicDictionary.Count)];

            return $"{lastName} {firstName} {patronymic}";
        }
    }
}

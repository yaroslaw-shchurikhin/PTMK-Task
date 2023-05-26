Решил реализовать на C# с Postgre, именно C# так как хорошо знаю как на нём работать с БД, хотя в целом мог бы и на C++ написать. 
Кроме Postgre умею ещё работать с Microsoft SQL.

Немного о физическом дизайне:
Все детали реализации в папке Details.
Люблю чистый мейн, так что вынес менеджер с свитчем в отдельный метод в классе TaskManager, 
сама реализация заданий в классе Tasks, а генераторы для задачи 4 в SupportFuncs.
Также классы User и контекст ApplicationContext для удобства работы с бд.

О реализации заданий:
1 - добавил в табличку ещё поле userId. Просто конструктор контекста, который проверяет есть сама бд и табличка, в ином случае создаёт

2 - можно было написать INSERT и сделать execute, но проще так, через датасет

3 - вычисление даты можно было бы поместить в сам запрос например:

SELECT DISTINCT ON (name, gender)
  name,
  dateofbirth,
  gender,
  DATE_PART('year', age(CURRENT_DATE, dateofbirth)) AS age
FROM public.users
ORDER BY name;

Но тогда бы мне пришлось пользоваться не анонимным типом, а классом UserInfo, где было бы поле Age,
поэтому решил поместить вычисление уже в шарп.

4 - просто случайный генератор, попросил чатгпт нагенерить мне фамилий и имён для словарей)

О 5 и 6 заданиях:
Не очень люблю замеры времени, особенно когда это время - несколько сотен миллисекунд) 
Предпочитаю стоимость запроса в тиках процессора с помощь EXPLAIN ANALYZE в Postgre, 
союственно саму оптимизацию проверял именно им - получается сильно меньше рандома.
Собственно в задании 5 около 250мс и Parallel Seq Scan - примерно 17833 cost,
в задании 6 использовал постгре индекс на два столбца - name и gender,
метод поменялся на Parallel Index Only Scan и cost = 15661, время получилось 220мс,
то есть оптимизация получилась.

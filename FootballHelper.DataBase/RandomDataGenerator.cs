using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballHelper.DataBase
{
    public class RandomDataGenerator
    {
        private static readonly Random random = new Random();
        private static readonly string[] firstNames =
                                                    {
                                                    "Александр", "Максим", "Иван", "Дмитрий", "Николай", "Андрей", "Павел", "Сергей", "Евгений", "Владимир",
                                                    "Алексей", "Михаил", "Юрий", "Роман", "Олег", "Игорь", "Константин", "Виктор", "Антон", "Георгий",
                                                    "Борис", "Анатолий", "Валерий", "Виталий", "Вячеслав", "Геннадий", "Григорий", "Денис", "Егор", "Захар",
                                                    "Илья", "Кирилл", "Леонид", "Марк", "Никита", "Пётр", "Ростислав", "Семён", "Тимур", "Фёдор"
                                                    };

        private static readonly string[] lastNames =
                                                    {
                                                    "Иванов", "Петров", "Сидоров", "Смирнов", "Кузнецов", "Попов", "Васильев", "Павлов", "Александров", "Морозов",
                                                    "Волков", "Зайцев", "Соловьёв", "Виноградов", "Богданов", "Воробьёв", "Фёдоров", "Михайлов", "Тарасов", "Белов",
                                                    "Комаров", "Орлов", "Киселёв", "Макаров", "Андреев", "Ковалёв", "Ильин", "Гусев", "Титов", "Кудрявцев",
                                                    "Баранов", "Куликов", "Алексеев", "Степанов", "Яковлев", "Сорокин", "Сергеев", "Романов", "Захаров", "Борисов"
                                                    };

        private static readonly string[] nationalities =
                                                        {
                                                        "Россия", "Украина", "Беларусь", "Казахстан", "Узбекистан", "Армения", "Грузия", "Азербайджан", "Литва", "Латвия",
                                                        "Эстония", "Молдова", "Таджикистан", "Киргизия", "Туркмения", "Польша", "Чехия", "Словакия", "Сербия", "Болгария",
                                                        "Румыния", "Венгрия", "Греция", "Турция", "Израиль", "Германия", "Франция", "Италия", "Испания", "Португалия",
                                                        "Швейцария", "Австрия", "Нидерланды", "Бельгия", "Люксембург", "Великобритания", "Ирландия", "Швеция", "Норвегия", "Финляндия"
                                                        };
        private static readonly string[] positions = { "Вратарь", "Защитник", "Полузащитник", "Нападающий" };
        private static readonly string[] clubNames =
                                                    {
                                                    "Спартак Москва", "ЦСКА Москва", "Зенит Санкт-Петербург", "Локомотив Москва", "Динамо Москва", "Краснодар", "Рубин Казань", "Ростов",
                                                    "Ахмат Грозный", "Арсенал Тула", "Урал Екатеринбург", "Уфа", "Оренбург", "Тамбов", "Сочи", "Химки", "Ротор Волгоград", "Крылья Советов",
                                                    "Спартак Нальчик", "Анжи Махачкала", "Алания Владикавказ", "Торпедо Москва", "Шинник Ярославль", "Томь Томск", "Кубань Краснодар",
                                                    "Балтика Калининград", "Факел Воронеж", "Мордовия Саранск", "Волга Нижний Новгород", "Салют Белгород", "Луч Владивосток", "Химик Дзержинск",
                                                    "Лада Тольятти", "Динамо Ставрополь", "КАМАЗ Набережные Челны", "Зенит Ижевск", "Ротор Волжский", "СКА Ростов-на-Дону", "Авангард Курск",
                                                    "Сахалин Южно-Сахалинск", "Текстильщик Иваново", "Динамо Барнаул", "Металлург Липецк", "Торпедо Владимир", "Носта Новотроицк", "Зенит Пенза",
                                                    "Шинник Екатеринбург", "Сокол Саратов"
                                                    };
        public static string RandomName()
        {
            return firstNames[random.Next(firstNames.Length)] + " " + lastNames[random.Next(lastNames.Length)];
        }

        public static string RandomNationality()
        {
            return nationalities[random.Next(nationalities.Length)];
        }

        public static DateOnly RandomDateOfBirth()
        {
            int year = random.Next(1980, 2005);
            int month = random.Next(1, 13);
            int day = random.Next(1, 29); 
            return new DateOnly(year, month, day);
        }

        public static string RandomPosition()
        {
            return positions[random.Next(positions.Length)];
        }

        public static string RandomClubName()
        {
            return clubNames[random.Next(clubNames.Length)];
        }

        public static int RandomStatistic()
        {
            return random.Next(0, 3);
        }
        public static int RandomGoals()
        {
            return random.Next(0, 2);
        }
        public static int RandomPossesion()
        {
            return random.Next(15, 70);
        }

        public static bool RandomCard()
        {
            return random.Next(2) == 1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ5
{
     class Program
    {
        static void Main(string[] args)
        {
            // Подключение к базе данных
            using (var dbContext = new CountriesEntities())
            {
                // Отобразить всю информацию о странах
                var allCountriesInfo = dbContext.Countries;

                Console.WriteLine("Информация о всех странах:");
                foreach (var country in allCountriesInfo)
                {
                    Console.WriteLine($"ID: {country.CountryID}, Название: {country.Name}, Площадь: {country.Area}, Континент: {country.Continent}");
                }

                // Отобразить название стран
                var countryNames = dbContext.Countries.Select(c => c.Name);

                Console.WriteLine("\nНазвания стран:");
                foreach (var name in countryNames)
                {
                    Console.WriteLine(name);
                }

                // Отобразить название столиц
                var capitalNames = dbContext.Capitals.Select(cap => cap.Name);

                Console.WriteLine("\nНазвания столиц:");
                foreach (var name in capitalNames)
                {
                    Console.WriteLine(name);
                }

                // Отобразить название крупных городов конкретной страны (для примера возьмем Россию)
                var citiesInRussia = dbContext.Cities.Where(city => city.Countries.Name == "Russia").Select(city => city.Name);

                Console.WriteLine("\nНазвания крупных городов в России:");
                foreach (var name in citiesInRussia)
                {
                    Console.WriteLine(name);
                }

                // Отобразить название столиц с количеством жителей больше пяти миллионов
                var capitalsWithPopulationOver5Million = dbContext.Capitals.Where(cap => cap.Population > 5000000).Select(cap => cap.Name);

                Console.WriteLine("\nНазвания столиц с населением более 5 миллионов:");
                foreach (var name in capitalsWithPopulationOver5Million)
                {
                    Console.WriteLine(name);
                }

                // Отобразить название всех европейских стран
                var europeanCountryNames = dbContext.Countries.Where(c => c.Continent == "Europe").Select(c => c.Name);

                Console.WriteLine("\nНазвания всех европейских стран:");
                foreach (var name in europeanCountryNames)
                {
                    Console.WriteLine(name);
                }

                // Отобразить название стран с площадью большей конкретного числа (допустим, 5 миллионов км²)
                var countriesWithAreaGreaterThan5Million = dbContext.Countries.Where(c => c.Area > 5000000).Select(c => c.Name);

                Console.WriteLine("\nНазвания стран с площадью более 5 миллионов км²:");
                foreach (var name in countriesWithAreaGreaterThan5Million)
                {
                    Console.WriteLine(name);
                }

              
              
                // Отобразить название стран, у которых площадь находится в указанном диапазоне (допустим, от 1 до 5 миллионов км²)
                var countriesByAreaRange = dbContext.Countries.Where(c => c.Area >= 1000000 && c.Area <= 5000000).Select(c => c.Name);

                Console.WriteLine("\nНазвания стран с площадью от 1 до 5 миллионов км²:");
                foreach (var name in countriesByAreaRange)
                {
                    Console.WriteLine(name);
                }

                // Показать топ-5 стран по площади
                var top5CountriesByArea = dbContext.Countries.OrderByDescending(c => c.Area).Take(5);

                // Показать топ-5 столиц по количеству жителей
                var top5CapitalsByPopulation = dbContext.Capitals.OrderByDescending(cap => cap.Population).Take(5);

                // Показать страну с самой большой площадью
                var countryWithLargestArea = dbContext.Countries.OrderByDescending(c => c.Area).First();

                // Показать столицу с самым большим количеством жителей
                var capitalWithLargestPopulation = dbContext.Capitals.OrderByDescending(cap => cap.Population).First();
                
                // Показать страну с самой маленькой площадью в Европе
                var smallestAreaInEurope = dbContext.Countries.Where(c => c.Continent == "Europe").OrderBy(c => c.Area).First();

                // Показать среднюю площадь стран в Европе
                var averageAreaInEurope = dbContext.Countries.Where(c => c.Continent == "Europe").Average(c => c.Area);

                // Показать топ-3 городов по количеству жителей для конкретной страны (для примера возьмем Россию)
                var top3CitiesByPopulationInCountry = dbContext.Cities.Where(city => city.Countries.Name == "Russia")
                                                                      .OrderByDescending(city => city.Population)
                                                                      .Take(3);

                // Показать общее количество стран
                var totalNumberOfCountries = dbContext.Countries.Count();

                // Показать часть света с максимальным количеством стран
                var continentWithMostCountries = dbContext.Countries.GroupBy(c => c.Continent)
                                                                    .OrderByDescending(g => g.Count())
                                                                    .Select(g => g.Key)
                                                                    .First();

                // Показать количество стран в каждой части света
                var countriesPerContinent = dbContext.Countries.GroupBy(c => c.Continent)
                                                              .Select(g => new { Continent = g.Key, Count = g.Count() });

                // Вывод результатов
                Console.WriteLine("Топ-5 стран по площади:");
                foreach (var country in top5CountriesByArea)
                {
                    Console.WriteLine($"{country.Name} - {country.Area} км²");
                }

                Console.WriteLine("\nТоп-5 столиц по количеству жителей:");
                foreach (var capital in top5CapitalsByPopulation)
                {
                    Console.WriteLine($"{capital.Name} - {capital.Population} жителей");
                }

                Console.WriteLine($"\nСтрана с самой большой площадью: {countryWithLargestArea.Name}");

                Console.WriteLine($"\nСтолица с самым большим количеством жителей: {capitalWithLargestPopulation.Name}");

                Console.WriteLine($"\nСтрана с самой маленькой площадью в Европе: {smallestAreaInEurope.Name}");

                Console.WriteLine($"\nСредняя площадь стран в Европе: {averageAreaInEurope} км²");

                Console.WriteLine("\nТоп-3 городов по количеству жителей для России:");
                foreach (var city in top3CitiesByPopulationInCountry)
                {
                    Console.WriteLine($"{city.Name} - {city.Population} жителей");
                }

                Console.WriteLine($"\nОбщее количество стран: {totalNumberOfCountries}");

                Console.WriteLine($"\nЧасть света с максимальным количеством стран: {continentWithMostCountries}");

                Console.WriteLine("\nКоличество стран в каждой части света:");
                foreach (var item in countriesPerContinent)
                {
                    Console.WriteLine($"{item.Continent}: {item.Count}");
                }
                Console.ReadLine();
            }
        }
    }
}
/*
 -- Создание таблицы стран
CREATE TABLE Countries (
    CountryID INT PRIMARY KEY,
    Name NVARCHAR(100),
    Area FLOAT,
    Continent NVARCHAR(100) -- Добавление поля для континента
);

-- Создание таблицы городов
CREATE TABLE Cities (
    CityID INT PRIMARY KEY,
    Name NVARCHAR(100),
    Population INT,
    CountryID INT,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);

-- Создание таблицы столиц
CREATE TABLE Capitals (
    CapitalID INT PRIMARY KEY,
    Name NVARCHAR(100),
    Population INT,
    CountryID INT,
    FOREIGN KEY (CountryID) REFERENCES Countries(CountryID)
);
-- Добавление информации о странах
INSERT INTO Countries (CountryID, Name, Area, Continent)
VALUES 
(1, 'USA', 9833517, 'North America'),
(2, 'Russia', 17125200, 'Eurasia'),
(3, 'China', 9596961, 'Asia'),
(4, 'Canada', 9984670, 'North America'),
(5, 'Brazil', 8515770, 'South America');

-- Добавление информации о городах
INSERT INTO Cities (CityID, Name, Population, CountryID)
VALUES
(1, 'New York', 8538000, 1),
(2, 'Los Angeles', 3976000, 1),
(3, 'Moscow', 12615882, 2),
(4, 'Saint Petersburg', 5383890, 2),
(5, 'Beijing', 21542000, 3),
(6, 'Shanghai', 24183300, 3),
(7, 'Toronto', 2930000, 4),
(8, 'Montreal', 1704694, 4),
(9, 'Sao Paulo', 12106920, 5),
(10, 'Rio de Janeiro', 6747815, 5);

-- Добавление информации о столицах
INSERT INTO Capitals (CapitalID, Name, Population, CountryID)
VALUES
(1, 'Washington, D.C.', 705749, 1),
(2, 'Moscow', 12615882, 2),
(3, 'Beijing', 21542000, 3),
(4, 'Ottawa', 989657, 4),
(5, 'Brasilia', 3015268, 5);

INSERT INTO Countries (CountryID, Name, Area, Continent)
VALUES 
(6, 'Germany', 357386, 'Europe'),
(7, 'France', 551695, 'Europe'),
(8, 'Spain', 505990, 'Europe');
 */
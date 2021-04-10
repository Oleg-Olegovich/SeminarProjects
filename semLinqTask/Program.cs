using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace semLinqTask
{
    class Program
    {
        const string Path = ".\\WeatherEvents_Jan2016-Dec2020.csv";

        static string NumbersOfDistinctCities(List<WeatherEvent> wetherEvent)
            => wetherEvent.Select(e => e.City)
                          .ToList()
                          .Distinct()
                          .Count()
                          .ToString();

        static string EachYearDataCount(List<WeatherEvent> wetherEvent)
        {
            int[] years = wetherEvent.Select(e => e.StartTime).Select(e => e.Year).Distinct().ToArray();
            var lines = new List<string>();
            years.ToList().ForEach(year => lines.Add(year.ToString()));
            return string.Join(Environment.NewLine, lines);
        }

        static string NumbersOfEventsIn(int year, List<WeatherEvent> wetherEvent)
            => wetherEvent.Where(e => e.StartTime.Year == year).Count().ToString();

        static string NumbersOfDistinctState(List<WeatherEvent> wetherEvent)
            => wetherEvent.Select(e => e.State)
                          .ToList()
                          .Distinct()
                          .Count()
                          .ToString();

        static Dictionary<string, int> DictionaryOfRainingCitiesIn(int year, List<WeatherEvent> wetherEvent)
        {
            var result = new Dictionary<string, int>();
            wetherEvent.Where(e => e.StartTime.Year == year).Where(e => e.Type == WeatherEventType.Rain)
                       .Select(e => e.City)
                       .GroupBy(e => e)
                       .Where(e => e.Count() > 1)
                       .Select(y => new { Elements = y.Key, Counter = y.Count() })
                       .ToList().ForEach(city => result.Add(city.Elements, city.Counter));
            return result;
        }

        static string LongestSnowEventIn(int year, List<WeatherEvent> wetherEvent)
        {
            WeatherEvent maxSpancity = wetherEvent.Where(e => e.StartTime.Year == year)
                                                  .Where(e => e.Type == WeatherEventType.Snow)
                                                  .OrderByDescending(e => e.EndTime - e.StartTime)
                                                  .ToList()[0];
            var snowTimeSpanDay = (maxSpancity.EndTime - maxSpancity.StartTime).ToString("%d");
            var snowTimeSpanPrecise = (maxSpancity.EndTime - maxSpancity.StartTime).ToString(@"hh\:mm\:ss");
            return $"{maxSpancity.City} and was continued {snowTimeSpanDay} day(s) {snowTimeSpanPrecise}";
        }

        static void Main(string[] args)
        {
            //Нужно дополнить модель WeatherEvent, создать список этого типа List<>
            //И заполнить его, читая файл с данными построчно через StreamReader
            //Ссылка на файл https://www.kaggle.com/sobhanmoosavi/us-weather-events

            //Написать Linq-запросы, используя синтаксис методов расширений
            //и продублировать его, используя синтаксис запросов
            //(возможно с вкраплениями методов расширений, ибо иногда первого может быть недостаточно)

            //0. Linq - сколько различных городов есть в датасете.
            //1. Сколько записей за каждый из годов имеется в датасете.
            //Потом будут еще запросы

            var weatherEvents = new List<WeatherEvent>();
            var data = new List<string>();
            try
            {
                using StreamReader sr = new StreamReader(Path);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    data.Add(line);
                }
                for (int i = 1; i < data.Count; ++i)
                {
                    try
                    {
                        weatherEvents.Add(ConvertFromStringToWeatherEvent(data[i], i));
                    }
                    catch (ArgumentException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.WriteLine(data[i]);
                        break;
                    }
                }
            }
            catch
            {
                Console.WriteLine("Error reading from file.");
            }

            Console.WriteLine($"Numbers of distinct cities: {NumbersOfDistinctCities(weatherEvents)}");
            Console.WriteLine(EachYearDataCount(weatherEvents));
            Console.WriteLine($"Numbers of weather event in 2018: {NumbersOfEventsIn(2018, weatherEvents)} ");
            Console.WriteLine($"Numbers of state in dataset: {NumbersOfDistinctState(weatherEvents)}");
            Console.WriteLine($"Numbers of city in dataset: {NumbersOfDistinctCities(weatherEvents)}");
            Console.WriteLine("Top 3 by rain numbers in different city");
            var RainingDic = DictionaryOfRainingCitiesIn(2019, weatherEvents).OrderByDescending(e => e.Value).ToList();
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i + 1} place : {RainingDic[i].Key}");
                Console.WriteLine($", numbers of rain: {RainingDic[i].Value}");
            }
            var years = weatherEvents.Select(e => e.StartTime).Select(e => e.Year).Distinct().ToArray();
            foreach (var item in years)
            {
                Console.WriteLine($"Longest snow in {item} was at {LongestSnowEventIn(item, weatherEvents)}");
            }
        }

        private static WeatherEvent ConvertFromStringToWeatherEvent(string line, int index)
        {
            var data = line.Split(',');
            if (data.Length != 13)
            {
                throw new ArgumentException($"Invalid data in line {index}!");
            }
            WeatherEvent weatherEvent;
            try
            {
                data[7] = data[7].Replace('.', ',');
                data[8] = data[8].Replace('.', ',');
                weatherEvent = new WeatherEvent()
                {
                    EventId = data[0],
                    Type = Enum.Parse<WeatherEventType>(data[1]),
                    Severity = Enum.Parse<Severity>(data[2]),
                    StartTime = DateTime.Parse(data[3]),
                    EndTime = DateTime.Parse(data[4]),
                    TimeZone = data[5],
                    AirportCode = data[6],
                    LocationLat = double.Parse(data[7]),
                    LocationLng = double.Parse(data[8]),
                    City = data[9],
                    County = data[10],
                    State = data[11],
                    ZipCode = data[12],
                };
            }
            catch
            {
                throw new ArgumentException($"Invalid data in line {index}!");
            }
            return weatherEvent;
        }
    }

    /// <summary>
    /// DTO.
    /// </summary>
    class WeatherEvent
    {
        public string EventId { get; set; }
        public WeatherEventType Type { get; set; }
        public Severity Severity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string TimeZone { get; set; }
        public string AirportCode { get; set; }
        public double LocationLat { get; set; }
        public double LocationLng { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }

    enum WeatherEventType
    {
        Unknown,
        Snow,
        Fog,
        Rain,
        Cold,
        Storm,
        Precipitation,
        Hail
    }

    enum Severity
    {
        Unknown,
        Light,
        Severe,
        Moderate,
        Heavy,
        UNK,
        Other
    }
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Planetas
{
    class Program
    {
        static void Main(string[] args)
        {
            LoadConfiguration();

            Planet ferengi = new Planet(500, 0, 1, Planet.Direction.CLOCKWISE);
            Planet betasoide = new Planet(2000, 0, 3, Planet.Direction.CLOCKWISE);
            Planet vulcano = new Planet(1000, 0, 5, Planet.Direction.COUNTER_CLOCKWISE);

            var system = new SolarSystem(new List<Planet>()
            {
                ferengi, betasoide, vulcano
            });

            var report = WeatherReport.GenerateWeatherReport(system, Config.DaysToSimulate);

            Console.WriteLine("Períodos de sequía: " + report.DraughtDays.ToString());
            Console.WriteLine("Perídoos de lluvia: " + report.RainDays.ToString());
            Console.WriteLine("Períodos de optimo: " + report.OptimumDays.ToString());
            Console.WriteLine("Máxima intensidad de lluvias: " + report.MaxRainIntensity.ToString() + " en el día: " + report.MaxIntensityDay.ToString());

            Console.ReadLine();
        }

        public static void LoadConfiguration()
        {
            string json;
            try
            {
                json = File.ReadAllText("appSettings.json");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not open appSettings.json");
                Console.ReadLine();
                Environment.Exit(-1);
                return;
            }

            try
            {
                var def = new { doubleComparisonPrecision = 0, daysToSimulate = 0 };
                var config = JsonConvert.DeserializeAnonymousType(json, def);
                Config.Precision = config.doubleComparisonPrecision;
                Config.DaysToSimulate = config.daysToSimulate;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error parsing json file");
                Console.ReadLine();
                Environment.Exit(-2);
            }
        }
    }
}

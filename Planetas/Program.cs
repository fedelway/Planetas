using System;
using System.Collections.Generic;
using System.IO;

namespace Planetas
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Config.ReadJson(ReadConfigFile());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
                return;
            }

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

            if(Config.UploadToMongo)
            {
                try
                {
                    Console.WriteLine("Subiendo datos a MongoDb...");
                    var uploader = new ReportUploader(report);
                    uploader.Upload();
                    Console.WriteLine("Datos subidos a MongoDb Atlas exitosamente.");
                }catch(Exception ex)
                {
                    Console.WriteLine("Falla al subir datos a MongoDb Atlas. " + ex.Message);
                }                
            }

            Console.ReadLine();
        }

        public static string ReadConfigFile()
        {
            try
            {
                return File.ReadAllText("appSettings.json");
            }
            catch (Exception ex)
            {
                throw new Exception("Could not open appSettings.json", ex);
            }
        }
    }
}

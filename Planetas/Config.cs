using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.IO;

namespace Planetas
{
    public static class Config
    {
        public static int Precision { get; set; }
        public static int DaysToSimulate { get; set; }

        public static void ReadConfigFile()
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

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
        public static bool UploadToMongo { get; set; }

        public static void ReadConfigFile()
        {
            string json;
            try
            {
                json = File.ReadAllText("appSettings.json");
            }
            catch (Exception ex)
            {
                throw new Exception("Could not open appSettings.json", ex);
            }

            try
            {
                var def = new { doubleComparisonPrecision = 0, daysToSimulate = 0, uploadToMongo = false };
                var config = JsonConvert.DeserializeAnonymousType(json, def);
                Precision = config.doubleComparisonPrecision;
                DaysToSimulate = config.daysToSimulate;
                UploadToMongo = config.uploadToMongo;
            }
            catch (Exception ex)
            {
                throw new Exception("Error parsing json file", ex);
            }
        }
    }
}

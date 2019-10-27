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

        public static void ReadJson(string json)
        {
            try
            {
                int? nullableInt = 0;
                bool? nullableBool = false;
                var def = new { doubleComparisonPrecision = nullableInt, daysToSimulate = nullableInt, uploadToMongo = nullableBool };

                var config = JsonConvert.DeserializeAnonymousType(json, def, new JsonSerializerSettings()
                {
                    MissingMemberHandling = MissingMemberHandling.Error
                });

                if(config.daysToSimulate == null || config.doubleComparisonPrecision == null || config.uploadToMongo == null)
                {
                    throw new Exception("Incomplete json");
                }

                Precision = config.doubleComparisonPrecision.Value;
                DaysToSimulate = config.daysToSimulate.Value;
                UploadToMongo = config.uploadToMongo.Value;
            }
            catch (Exception ex)
            {
                throw new Exception("Error parsing json", ex);
            }
        }
    }
}

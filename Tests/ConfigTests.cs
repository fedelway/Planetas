using System;
using System.Collections.Generic;
using System.Text;
using Planetas;
using Xunit;

namespace Tests
{
    public class ConfigTests
    {
        [Fact]
        public void ReadJsonTest()
        {
            var json = @"{
              ""doubleComparisonPrecision"": 5,
              ""daysToSimulate"": 3650,
              ""uploadToMongo"": true
            }";

            Config.ReadJson(json);

            Assert.Equal(5, Config.Precision);
            Assert.Equal(3650, Config.DaysToSimulate);
            Assert.Equal(true, Config.UploadToMongo);
        }

        [Fact]
        public void BadDataTypeJsonThrows()
        {
            var json = @"{
              ""doubleComparisonPrecision"": ""Bad Type"",
              ""daysToSimulate"": 3650,
              ""uploadToMongo"": true
            }";

            Assert.ThrowsAny<Exception>(() => Config.ReadJson(json));
        }

        [Fact]
        public void IncompleteJsonThrows()
        {
            var json = @"{""doubleComparisonPrecision"": 5}";

            Assert.ThrowsAny<Exception>(() => Config.ReadJson(json));
        }
    }
}

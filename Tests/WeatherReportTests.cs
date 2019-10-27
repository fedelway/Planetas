using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Planetas;

namespace Tests
{
    public class WeatherReportTests : TestSetup
    {
        [Fact]
        public void CorrectNumberOfDaysTest()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(10, 0, 0, Planet.Direction.CLOCKWISE),
                new Planet(20, 0, 0, Planet.Direction.CLOCKWISE),
                new Planet(-10, 0, 0, Planet.Direction.CLOCKWISE)
            });

            var days = 10;
            var report = WeatherReport.GenerateWeatherReport(solarSystem, days);

            Assert.Equal(days, report.WeatherPerDay.Count);
        }
    }
}

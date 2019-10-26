using System;
using System.Collections.Generic;
using System.Text;

namespace Planetas
{
    public class WeatherReport
    {
        public int DraughtDays { get; }
        public int RainDays { get; }
        public double MaxRainIntensity { get; }
        public int MaxIntensityDay { get; }
        public int OptimumDays { get; }
        public List<DayReport> WeatherPerDay { get; }

        private WeatherReport(int draughtDays, int rainDays, double maxRainIntensity, int maxIntensityDay, int optimumDays, List<DayReport> weatherPerDay)
        {
            DraughtDays = draughtDays;
            RainDays = rainDays;
            MaxRainIntensity = maxRainIntensity;
            MaxIntensityDay = maxIntensityDay;
            OptimumDays = optimumDays;
            WeatherPerDay = weatherPerDay;
        }

        public static WeatherReport GenerateWeatherReport(SolarSystem system, int daysToSimulate)
        {
            var weatherList = new List<DayReport>();
            int draughtCount = 0;
            int rainCount = 0;
            double maxRainIntensity = 0;
            int maxIntensityDay = 0;
            int optimumCount = 0;
            for (int day = 1; day <= daysToSimulate; day++)
            {
                system.SimulateDay();

                var weatherToday = system.GetWeather();
                double intensityToday = 0;
                switch (weatherToday)
                {
                    case Weather.DRAUGHT:
                        draughtCount++;
                        break;
                    case Weather.RAINY:
                        rainCount++;
                        intensityToday = system.GetRainIntensity();
                        if (intensityToday > maxRainIntensity)
                        {
                            maxIntensityDay = day;
                            maxRainIntensity = intensityToday;
                        }
                        break;
                    case Weather.OPTIMUM:
                        optimumCount++;
                        break;
                }

                weatherList.Add(new DayReport(weatherToday,intensityToday,day));
            }

            return new WeatherReport(draughtCount, rainCount, maxRainIntensity, maxIntensityDay, optimumCount, weatherList);
        }
    }
}

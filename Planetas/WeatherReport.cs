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

        private WeatherReport(int draughtDays, int rainDays, double maxRainIntensity, int maxIntensityDay, int optimumDays)
        {
            DraughtDays = draughtDays;
            RainDays = rainDays;
            MaxRainIntensity = maxRainIntensity;
            MaxIntensityDay = maxIntensityDay;
            OptimumDays = optimumDays;
        }

        public static WeatherReport GenerateWeatherReport(SolarSystem system, int daysToSimulate)
        {
            int draughtCount = 0;
            int rainCount = 0;
            double maxRainIntensity = 0;
            int maxIntensityDay = 0;
            int optimumCount = 0;
            for (int day = 0; day < 365 * 10; day++)
            {
                system.SimulateDay();

                switch (system.GetWeather())
                {
                    case Weather.DRAUGHT:
                        draughtCount++;
                        break;
                    case Weather.RAINY:
                        rainCount++;
                        var intensity = system.GetRainIntensity();
                        if (intensity > maxRainIntensity)
                        {
                            maxIntensityDay = day;
                            maxRainIntensity = intensity;
                        }
                        break;
                    case Weather.OPTIMUM:
                        optimumCount++;
                        break;
                }
            }

            return new WeatherReport(draughtCount, rainCount, maxRainIntensity, maxIntensityDay, optimumCount);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Planetas
{
    public class Weather
    {
        public enum WeatherType
        {
            RAINY,
            OPTIMUM,
            DRAUGHT,
            NORMAL
        }

        public WeatherType Type { get; }
        public double RainIntensity { get; }

        private Weather(WeatherType type, double rainIntensity)
        {
            this.Type = type;
            this.RainIntensity = rainIntensity;
        }

        public static Weather CreateNormal()
        {
            return new Weather(WeatherType.NORMAL, 0);
        }

        public static Weather CreateOptimum()
        {
            return new Weather(WeatherType.OPTIMUM, 0);
        }

        public static Weather CreateDraught()
        {
            return new Weather(WeatherType.DRAUGHT, 0);
        }

        public static Weather CreateRainy(double intensity)
        {
            return new Weather(WeatherType.RAINY, intensity);
        }
    }
}

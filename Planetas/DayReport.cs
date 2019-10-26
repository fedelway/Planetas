using System;
using System.Collections.Generic;
using System.Text;

namespace Planetas
{
    public class DayReport
    {
        public Weather Weather { get; set; }
        public double RainIntensity { get; set; }
        public int Day { get; set; }

        public DayReport(Weather weather, double rainIntensity, int day)
        {
            Weather = weather;
            RainIntensity = rainIntensity;
            Day = day;
        }
    }
}

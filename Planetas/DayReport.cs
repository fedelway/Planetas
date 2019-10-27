using System;
using System.Collections.Generic;
using System.Text;

namespace Planetas
{
    public class DayReport
    {
        public Weather Weather { get; set; }
        public int Day { get; set; }

        public DayReport(Weather weather, int day)
        {
            Weather = weather;
            Day = day;
        }
    }
}

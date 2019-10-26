using System;
using System.Numerics;

namespace Planetas
{
    public static class NumericExtensions
    {
        public static double ToRadians(this double deg)
        {
            return (Math.PI / 180) * deg;
        }

        public static double ToDegrees(this double rad)
        {
            return rad * 180 / Math.PI;
        }

        public static bool EqualsPrecision(this double val, double other, int precision)
        {
            return Math.Floor(val * Math.Pow(10, precision)) == Math.Floor(other * Math.Pow(10, precision));
        }
    }
}

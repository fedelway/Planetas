using System;
using System.Collections.Generic;
using System.Text;

namespace Planetas
{
    public class PolarCoordinates
    {
        public double Length { get; set; }
        public double Angle { get; set; }

        public PolarCoordinates(double length, double angle)
        {
            this.Length = length;
            this.Angle = MathUtils.NormalizeAngle(angle);
        }
        
        public CartesianCoordinates ToCartesian()
        {
            return new CartesianCoordinates(Length * Math.Cos(Angle), Length * Math.Sin(Angle));
        }
    }
}

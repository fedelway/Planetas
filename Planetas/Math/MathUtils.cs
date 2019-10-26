using System;
using System.Collections.Generic;
using System.Numerics;

namespace Planetas
{
    public static class MathUtils
    {
        public static double NormalizeAngle(double angle)
        {
            int modifier = angle > 0 ? -1 : 1;

            while(angle > Math.PI * 2 || angle < 0)
            {
                angle += Math.PI * 2 * modifier;
            }
            return angle;
        }

        public static bool AreAnglesAligned(double angle1, double angle2)
        {
            angle1 = NormalizeAngle(angle1);
            angle2 = NormalizeAngle(angle2);

            bool negativeAngleEquals;
            if(angle1 > angle2)
            {
                negativeAngleEquals = DoubleEquals(angle1,NormalizeAngle(angle2 + Math.PI));
            }
            else
            {
                negativeAngleEquals = DoubleEquals(NormalizeAngle(angle1 + Math.PI), angle2);
            }

            return DoubleEquals(angle1, angle2) || negativeAngleEquals;
        }

        public static bool LineFromTwoPointPassesOrigin(CartesianCoordinates p1, CartesianCoordinates p2)
        {
            return DoubleEquals( p1.X * (p2.Y - p1.Y) , p1.Y * (p2.X - p1.X) );
        }

        public static bool IsPointInsideTriangle(CartesianCoordinates point, List<CartesianCoordinates> triangle)
        {
            if (triangle.Count != 3)
                throw new ArgumentException("Triangle should have 3 points");

            var bigArea = AreaOfTriangle(triangle[0], triangle[1], triangle[2]);

            var a1 = AreaOfTriangle(point, triangle[0], triangle[1]);
            var a2 = AreaOfTriangle(point, triangle[0], triangle[2]);
            var a3 = AreaOfTriangle(point, triangle[1], triangle[2]);

            return DoubleEquals(bigArea ,(a1 + a2 + a3));
        }

        public static double AreaOfTriangle(CartesianCoordinates p1, CartesianCoordinates p2, CartesianCoordinates p3)
        {
            return Math.Abs((p1.X * (p2.Y - p3.Y) +
                         p2.X * (p3.Y - p1.Y) +
                         p3.X * (p1.Y - p2.Y)) / 2d);
        }

        private static bool DoubleEquals(double a, double b)
        {
            return a.EqualsPrecision(b, Configuration.Precision);
        }
    }
}

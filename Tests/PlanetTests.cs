using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Planetas;
using System.Numerics;

namespace Tests
{
    public class PlanetTests
    {
        [Fact]
        public void CarthesianCoordinatesTest()
        {
            var planet = new Planet(10, 0, 0, Planet.Direction.CLOCKWISE);
            Assert.Equal(new CartesianCoordinates(10, 0), planet.PolarPosition.ToCartesian());
            
            var planet2 = new Planet(10, 90, 0, Planet.Direction.CLOCKWISE);

            var coordinates = planet2.PolarPosition.ToCartesian();

            Assert.Equal(0, planet2.PolarPosition.ToCartesian().X, 5);
            Assert.Equal(10, planet2.PolarPosition.ToCartesian().Y, 5);
        }

        [Fact]
        public void RotationTest()
        {
            var planet = new Planet(10, 0, 1, Planet.Direction.CLOCKWISE);
            var oldAngle = planet.PolarPosition.Angle;

            planet.Rotate();
            Assert.NotEqual(planet.PolarPosition.Angle, oldAngle);
        }

        [Fact]
        public void ClockwiseRotationDecreasesAngle()
        {
            var planet = new Planet(10, 0, 1, Planet.Direction.CLOCKWISE);
            var oldAngle = planet.PolarPosition.Angle;

            planet.Rotate();
            Assert.True(planet.PolarPosition.Angle < oldAngle);
        }

        [Fact]
        public void CounterClockwiseRotationIncreasesAngle()
        {
            var planet = new Planet(10, 0, 1, Planet.Direction.COUNTER_CLOCKWISE);
            var oldAngle = planet.PolarPosition.Angle;

            planet.Rotate();
            Assert.True(planet.PolarPosition.Angle > oldAngle);
        }

        [Fact]
        public void DayRotationTest()
        {
            double dayRotation = 25;
            var planet = new Planet(10, 0, dayRotation, Planet.Direction.COUNTER_CLOCKWISE);
            var oldAngle = planet.PolarPosition.Angle;

            planet.Rotate();
            Assert.Equal(oldAngle + dayRotation.ToRadians(), planet.PolarPosition.Angle);
        }
    }
}

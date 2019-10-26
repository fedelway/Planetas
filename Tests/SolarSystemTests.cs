using System;
using System.Collections.Generic;
using System.Text;
using Planetas;
using Xunit;

namespace Tests
{
    public class SolarSystemTests
    {
        public SolarSystemTests()
        {
            Configuration.Precision = 5;
        }

        [Fact]
        public void PlanetsAlignedTest()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(10, 0, 0, Planet.Direction.CLOCKWISE),
                new Planet(20, 0, 0, Planet.Direction.CLOCKWISE),
                new Planet(-10, 0, 0, Planet.Direction.CLOCKWISE)
            });

            Assert.True(solarSystem.ArePlanetsAligned());
        }

        [Fact]
        public void PlanetsAlignedTest2()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(0, 10, 0, Planet.Direction.CLOCKWISE),
                new Planet(20, 10, 0, Planet.Direction.CLOCKWISE),
                new Planet(-10, 10, 0, Planet.Direction.CLOCKWISE)
            });

            Assert.True(solarSystem.ArePlanetsAligned());
        }

        [Fact]
        public void PlanetsAlignedTest3()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(10, 30, 0, Planet.Direction.CLOCKWISE),
                new Planet(30, 30, 0, Planet.Direction.CLOCKWISE),
                new Planet(-10, 180 + 30, 0, Planet.Direction.CLOCKWISE)
            });

            Assert.True(solarSystem.ArePlanetsAligned());
        }

        [Fact]
        public void PlanetsNotAligned()
        {
            var p1 = new CartesianCoordinates(10, 10).ToPolar();
            var p2 = new CartesianCoordinates(0, 10).ToPolar();
            var p3 = new CartesianCoordinates(0, -10).ToPolar();

            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(p1.Length, p1.Angle.ToDegrees(), 0, Planet.Direction.CLOCKWISE),
                new Planet(p2.Length, p2.Angle.ToDegrees(), 0, Planet.Direction.CLOCKWISE),
                new Planet(p3.Length, p3.Angle.ToDegrees(), 0, Planet.Direction.CLOCKWISE),
            });

            Assert.False(solarSystem.ArePlanetsAligned());
        }

        [Fact]
        public void RainIntensityTest()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(1,0,0,Planet.Direction.CLOCKWISE),
                new Planet(1,90,0,Planet.Direction.CLOCKWISE),
                new Planet(0,0,0,Planet.Direction.CLOCKWISE)
            });

            Assert.Equal(2 + Math.Sqrt(2), solarSystem.GetRainIntensity());
        }

        [Fact]
        public void DraughtWeatherTest()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(10,30.000000001,0,Planet.Direction.CLOCKWISE),
                new Planet(20,30,0,Planet.Direction.CLOCKWISE),
                new Planet(30,30,0,Planet.Direction.CLOCKWISE)
            });

            Assert.Equal(Weather.DRAUGHT, solarSystem.GetWeather());
        }

        [Fact]
        public void OptimumWeatherTest()
        {
            var p1 = new CartesianCoordinates(0, 1);
            var p2 = new CartesianCoordinates(1, 0);
            var p3 = new CartesianCoordinates(2, -1);
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(p1.ToPolar().Length,p1.ToPolar().Angle.ToDegrees(),0,Planet.Direction.CLOCKWISE),
                new Planet(p2.ToPolar().Length,p2.ToPolar().Angle.ToDegrees(),0,Planet.Direction.CLOCKWISE),
                new Planet(p3.ToPolar().Length,p3.ToPolar().Angle.ToDegrees(),0,Planet.Direction.CLOCKWISE)
            });

            Assert.Equal(Weather.OPTIMUM, solarSystem.GetWeather());
        }

        [Fact]
        public void RainyWeatherTest()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(30,30,0,Planet.Direction.CLOCKWISE),
                new Planet(30,150,0,Planet.Direction.CLOCKWISE),
                new Planet(30,260,0,Planet.Direction.CLOCKWISE)
            });

            Assert.Equal(Weather.RAINY, solarSystem.GetWeather());
        }

        [Fact]
        public void NormalWeatherTest()
        {
            var solarSystem = new SolarSystem(new List<Planet>()
            {
                new Planet(30,30,0,Planet.Direction.CLOCKWISE),
                new Planet(30,150,0,Planet.Direction.CLOCKWISE),
                new Planet(30,90,0,Planet.Direction.CLOCKWISE)
            });

            Assert.Equal(Weather.NORMAL, solarSystem.GetWeather());
        }
    }
}

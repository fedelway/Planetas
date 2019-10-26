using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Numerics;

namespace Planetas
{
    public class SolarSystem
    {
        List<Planet> planetList;
        int dayCount;

        public SolarSystem(List<Planet> planetList)
        {
            this.planetList = planetList;
            this.dayCount = 0;
        }

        public void SimulateDay()
        {
            foreach(var planet in planetList)
            {
                planet.Rotate();
            }
            dayCount++;
        }

        public Weather GetWeather()
        {
            if (ArePlanetsAligned())
            {
                //Check if they pass through the center.
                if (MathUtils.LineFromTwoPointPassesOrigin(
                    planetList[0].PolarPosition.ToCartesian(),
                    planetList[1].PolarPosition.ToCartesian()))
                {
                    return Weather.DRAUGHT;
                }
                else return Weather.OPTIMUM;
            }

            if(MathUtils.IsPointInsideTriangle(new CartesianCoordinates(0,0), planetList.Select(p => p.PolarPosition.ToCartesian() ).ToList()))
            {
                return Weather.RAINY;
            }

            return Weather.NORMAL;
        }
        
        public double GetRainIntensity()
        {
            var planetCoordinates = GetPlanetCartesianCoordinates();

            double perimeter = 0;
            var item = planetCoordinates[0];
            for(int i = 1; i<planetCoordinates.Count; i++)
            {
                perimeter += (planetCoordinates[i] - item).Length();
                item = planetCoordinates[i];
            }

            //Add the last length
            perimeter += (item - planetCoordinates[0]).Length();

            return perimeter;
        }
        
        public bool ArePlanetsAligned()
        {
            var diffList = GetDifferences();

            var angle = diffList[0].GetAngle();
            return diffList.All(d => MathUtils.AreAnglesAligned(angle, d.GetAngle()));
        }

        private List<CartesianCoordinates> GetDifferences()
        {
            var planetCoordinates = planetList.Select(p => p.PolarPosition.ToCartesian()).ToList();

            CartesianCoordinates coord = planetCoordinates[0];
            var diffList = new List<CartesianCoordinates>();
            for (int i = 1; i < planetCoordinates.Count; i++)
            {
                diffList.Add(planetCoordinates[i] - coord);
                coord = planetCoordinates[i];
            }

            return diffList;
        }

        private List<CartesianCoordinates> GetPlanetCartesianCoordinates()
        {
            return planetList.Select(p => p.PolarPosition.ToCartesian()).ToList();
        }
    }
}

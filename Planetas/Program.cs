using System;
using System.Collections.Generic;

namespace Planetas
{
    class Program
    {
        static void Main(string[] args)
        {
            Planet ferengi = new Planet(500, 0, 1, Planet.Direction.CLOCKWISE);
            Planet betasoide = new Planet(2000, 0, 3, Planet.Direction.CLOCKWISE);
            Planet vulcano = new Planet(1000, 0, 5, Planet.Direction.COUNTER_CLOCKWISE);

            var system = new SolarSystem(new List<Planet>()
            {
                ferengi, betasoide, vulcano
            });

            int draughtCount = 0;
            int rainCount = 0;
            double maxRainIntensity = 0;
            int maxIntensityDay = 0;
            int optimumCount = 0;
            for(int day = 0; day<365*10; day++)
            {
                system.SimulateDay();

                switch ( system.GetWeather())
                {
                    case Weather.DRAUGHT:
                        draughtCount++;
                        break;
                    case Weather.RAINY:
                        rainCount++;
                        var intensity = system.GetRainIntensity();
                        if(intensity > maxRainIntensity)
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

            Console.WriteLine("Períodos de sequía: " + draughtCount.ToString());
            Console.WriteLine("Perídoos de lluvia: " + rainCount.ToString());
            Console.WriteLine("Períodos de optimo: " + optimumCount.ToString());
            Console.WriteLine("Máxima intensidad de lluvias: " + maxRainIntensity.ToString() + " en el día: " + maxIntensityDay.ToString());

            Console.ReadLine();
        }
    }
}

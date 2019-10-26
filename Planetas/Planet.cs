using System;
using System.Collections.Generic;
using System.Text;

namespace Planetas
{
    public class Planet
    {
        private PolarCoordinates polarPosition;
        public PolarCoordinates PolarPosition {
            get { return polarPosition; }
            set { polarPosition = value; }
        }
        double dayRotation;
        Direction direction;

        public enum Direction
        {
            CLOCKWISE,
            COUNTER_CLOCKWISE
        };

        public Planet(double distance, double degrees, double dayRotation, Direction direction)
        {
            var radians = degrees.ToRadians();
            this.PolarPosition = new PolarCoordinates(distance, radians);

            this.dayRotation = dayRotation.ToRadians();
            this.direction = direction;
        }

        public void Rotate()
        {
            this.polarPosition.Angle += this.dayRotation * GetDirectionModifier(direction);
        }

        private int GetDirectionModifier(Direction direction)
        {
            return direction == Direction.CLOCKWISE ? -1 : 1;
        }
    }
}

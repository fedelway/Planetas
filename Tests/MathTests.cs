using System;
using Xunit;
using System.Numerics;
using Planetas;
using System.Collections.Generic;

namespace Tests
{
    public class MathTests : TestSetup
    {
        [Fact]
        public void GetAngleTest()
        {
            Assert.Equal(0d, new CartesianCoordinates(1,0).GetAngle());
            Assert.Equal(0d, new CartesianCoordinates(2,0).GetAngle());
            Assert.Equal(0d, new CartesianCoordinates(23,0).GetAngle());
            Assert.Equal(Math.PI / 2, new CartesianCoordinates(0, 1).GetAngle());
            Assert.Equal(Math.PI / 4, new CartesianCoordinates(1, 1).GetAngle());
        }

        [Fact]
        public void NormalizeAngleTest()
        {
            Assert.Equal(1, MathUtils.NormalizeAngle(1),4);
            Assert.Equal(1, MathUtils.NormalizeAngle(Math.PI * 2 + 1),4);
            Assert.Equal(1+Math.PI, MathUtils.NormalizeAngle(Math.PI * 3 + 1),4);
        }

        [Fact]
        public void NormalizeNegativeAngleTest()
        {
            Assert.Equal(Math.PI * 2 - 1, MathUtils.NormalizeAngle(-1));
        }

        [Fact]
        public void AreAlignedReturnsTrueWhenEqual()
        {
            Assert.True(MathUtils.AreAnglesAligned(0, 0));
            Assert.True(MathUtils.AreAnglesAligned(1, 1));
            Assert.True(MathUtils.AreAnglesAligned(2, 2));
            Assert.True(MathUtils.AreAnglesAligned(1000, 1000));
        }

        [Fact]
        public void LinePassesThroughOrigin()
        {
            Assert.True(MathUtils.LineFromTwoPointPassesOrigin(new CartesianCoordinates(1, 1), new CartesianCoordinates(2, 2)) );
            Assert.True(MathUtils.LineFromTwoPointPassesOrigin(new CartesianCoordinates(-1, -1), new CartesianCoordinates(2, 2)));
            Assert.False(MathUtils.LineFromTwoPointPassesOrigin(new CartesianCoordinates(1, 1), new CartesianCoordinates(-1, 1)));
        }

        [Fact]
        public void AreaOfTriangle()
        {
            Assert.Equal(0.5, MathUtils.AreaOfTriangle(new CartesianCoordinates(0, 0), new CartesianCoordinates(1, 0), new CartesianCoordinates(0, 1)));
        }

        [Fact]
        public void IsPointInsideTriangle()
        {
            Assert.True(MathUtils.IsPointInsideTriangle(new CartesianCoordinates(0, 0), new List<CartesianCoordinates>()
            {
                new CartesianCoordinates(1,1),
                new CartesianCoordinates(1,-1),
                new CartesianCoordinates(-1,-1)
            })
            );
        }

        [Fact]
        public void IsPointInsideTriangleThrowsWhenListDoesNotContainThreeElements()
        {
            Assert.Throws<ArgumentException>( () =>
            {
                MathUtils.IsPointInsideTriangle(new CartesianCoordinates(0, 0), new List<CartesianCoordinates>()
                {
                    new CartesianCoordinates(1,1)
                });
            });
        }

        [Fact]
        public void PrecisionEqualsTest()
        {
            Assert.True((0.0000000000001).EqualsPrecision(0, 5));
            Assert.False((0.0001).EqualsPrecision(0, 5));
        }

        [Fact]
        public void CartesianPrecisionEqualsTest()
        {
            var a = new CartesianCoordinates(0, 0);
            var b = new CartesianCoordinates(0.000000001, 0.000000001);

            Assert.True(a.Equals(b, 5));
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void PolarToCartesianToPolar()
        {
            var polar = new PolarCoordinates(10, 2);
            var cartesian = polar.ToCartesian();
            var newPolar = cartesian.ToPolar();

            Assert.Equal(polar.Length, newPolar.Length);
            Assert.Equal(polar.Angle, newPolar.Angle);
        }

        [Fact]
        public void CartesianToPolarToCartesian()
        {
            var cartesian = new CartesianCoordinates(10, 2);
            var polar = cartesian.ToPolar();
            var newCartesian = polar.ToCartesian();

            Assert.Equal(cartesian.X, newCartesian.X,10);
            Assert.Equal(cartesian.Y, newCartesian.Y,10);
        }
    }
}

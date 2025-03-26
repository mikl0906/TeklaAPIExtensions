using Tekla.Structures.Geometry3d;

namespace TeklaAPIExtensions.Model.Tests;

public class PointExtensionsTest
{
    [Fact]
    public void Dot_BasicVectors_ReturnsCorrectDotProduct()
    {
        // Arrange
        var point1 = new Point(1, 2, 3);
        var point2 = new Point(4, 5, 6);

        // Act
        double result = point1.Dot(point2);

        // Assert
        Assert.Equal(32, result); // 1*4 + 2*5 + 3*6 = 4 + 10 + 18 = 32
    }

    [Fact]
    public void Dot_WithZeroPoint_ReturnsZero()
    {
        // Arrange
        var point1 = new Point(1, 2, 3);
        var zeroPoint = new Point(0, 0, 0);

        // Act
        double result1 = point1.Dot(zeroPoint);
        double result2 = zeroPoint.Dot(point1);

        // Assert
        Assert.Equal(0, result1);
        Assert.Equal(0, result2);
    }

    [Fact]
    public void Dot_WithNegativeCoordinates_ReturnsCorrectDotProduct()
    {
        // Arrange
        var point1 = new Point(-1, -2, -3);
        var point2 = new Point(4, 5, 6);

        // Act
        double result = point1.Dot(point2);

        // Assert
        Assert.Equal(-32, result); // -1*4 + -2*5 + -3*6 = -4 - 10 - 18 = -32
    }

    [Fact]
    public void Dot_ParallelVectors_ReturnsProductOfMagnitudes()
    {
        // Arrange
        var point1 = new Point(2, 0, 0);
        var point2 = new Point(3, 0, 0);

        // Act
        double result = point1.Dot(point2);
        double magnitude1 = Math.Sqrt(point1.X * point1.X + point1.Y * point1.Y + point1.Z * point1.Z);
        double magnitude2 = Math.Sqrt(point2.X * point2.X + point2.Y * point2.Y + point2.Z * point2.Z);

        // Assert
        Assert.Equal(6, result);
        Assert.Equal(magnitude1 * magnitude2, result);
    }

    [Fact]
    public void Dot_PerpendicularVectors_ReturnsZero()
    {
        // Arrange
        var point1 = new Point(1, 0, 0);
        var point2 = new Point(0, 1, 0);

        // Act
        double result = point1.Dot(point2);

        // Assert
        Assert.Equal(0, result);
    }
}
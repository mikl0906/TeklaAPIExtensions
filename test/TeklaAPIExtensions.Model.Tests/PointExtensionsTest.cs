using Tekla.Structures.Geometry3d;

namespace TeklaAPIExtensions.Model.Tests;

public class PointExtensionsTest
{
    [Test]
    [Arguments(1, 2, 3, 4, 5, 6, 32)] // 1*4 + 2*5 + 3*6 = 4 + 10 + 18 = 32
    [Arguments(0, 0, 0, 4, 5, 6, 0)] // 0*4 + 0*5 + 0*6 = 0
    [Arguments(1, 2, 3, 0, 0, 0, 0)] // 1*0 + 2*0 + 3*0 = 0
    [Arguments(-1, -2, -3, 4, 5, 6, -32)] // -1*4 + -2*5 + -3*6 = -4 - 10 - 18 = -32
    [Arguments(1, 0, 0, 0, 1, 0, 0)] // 1*0 + 0*1 + 0*0 = 0
    public async Task Dot_ReturnsCorrectDotProduct(double x1, double y1, double z1, double x2, double y2, double z2, double expected)
    {
        // Arrange
        var point1 = new Point(x1, y1, z1);
        var point2 = new Point(x2, y2, z2);

        // Act
        double result = point1.Dot(point2);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }

    [Test]
    public async Task Dot_ParallelVectors_ReturnsProductOfMagnitudes()
    {
        // Arrange
        var point1 = new Point(2, 0, 0);
        var point2 = new Point(3, 0, 0);

        // Act
        double result = point1.Dot(point2);
        double magnitude1 = Math.Sqrt(point1.X * point1.X + point1.Y * point1.Y + point1.Z * point1.Z);
        double magnitude2 = Math.Sqrt(point2.X * point2.X + point2.Y * point2.Y + point2.Z * point2.Z);

        // Assert
        await Assert.That(result).IsEqualTo(6);
        await Assert.That(magnitude1 * magnitude2).IsEqualTo(result);
    }
}
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Drawing;

namespace TeklaAPIExtensions.Drawing;

/// <summary>
/// Provides extension methods for converting collections of <see cref="Point"/> objects to <see cref="PointList"/>.
/// </summary>
public static class PointListExtensions
{
    /// <summary>
    /// Converts an <see cref="IEnumerable{T}"/> of <see cref="Point"/> objects to a <see cref="PointList"/>.
    /// </summary>
    /// <param name="points">The collection of <see cref="Point"/> objects to convert.</param>
    /// <returns>A <see cref="PointList"/> containing the points from the input collection.</returns>
    public static PointList ToPointList(this IEnumerable<Point> points)
    {
        var pointList = new PointList();
        foreach (var point in points)
        {
            pointList.Add(point);
        }
        return pointList;
    }
}

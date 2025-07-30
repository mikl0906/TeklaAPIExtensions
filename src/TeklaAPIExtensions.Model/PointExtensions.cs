using Tekla.Structures.Geometry3d;

using SN = System.Numerics;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for the <see cref="Point"/> class
/// </summary>
public static class PointExtensions
{
    /// <summary>
    /// Calculates the dot product of the current point with another point.
    /// </summary>
    /// <param name="point">The current point.</param>
    /// <param name="other">The other point to calculate the dot product with.</param>
    /// <returns>The dot product of the two points.</returns>
    public static double Dot(this Point point, Point other)
    {
        return point.X * other.X + point.Y * other.Y + point.Z * other.Z;
    }

    /// <summary>
    /// Converts a Tekla Structures <see cref="Point"/> to a <see cref="SN.Vector3"/>.
    /// </summary>
    /// <param name="point">The <see cref="Point"/> to convert.</param>
    /// <returns>A <see cref="SN.Vector3"/> representation of the given <see cref="Point"/>.</returns>
    public static SN.Vector3 ToVector3(this Point point)
    {
        return new SN.Vector3((float)point.X, (float)point.Y, (float)point.Z);
    }

    /// <summary>
    /// Transforms the specified <see cref="Point"/> using the given <see cref="CoordinateSystem"/>.
    /// </summary>
    /// <param name="point">The point to transform.</param>
    /// <param name="coordinateSystem">The coordinate system to use for the transformation.</param>
    /// <returns>A new <see cref="Point"/> that is the result of the transformation.</returns>
    public static Point Transform(this Point point, CoordinateSystem coordinateSystem)
    {
        var matrix = MatrixFactory.ToCoordinateSystem(coordinateSystem);
        return matrix.Transform(point);
    }

    /// <summary>
    /// Transforms a collection of points using the specified coordinate system.
    /// </summary>
    /// <param name="points">The collection of points to be transformed.</param>
    /// <param name="coordinateSystem">The coordinate system to use for the transformation.</param>
    /// <returns>An IEnumerable of transformed points.</returns>
    public static IEnumerable<Point> Transform(this IEnumerable<Point> points, CoordinateSystem coordinateSystem)
    {
        var matrix = MatrixFactory.ToCoordinateSystem(coordinateSystem);
        foreach (var point in points)
        {
            yield return matrix.Transform(point);
        }
    }

    /// <summary>
    /// Calculates the average point from a collection of points.
    /// </summary>
    /// <param name="points">The collection of points to calculate the average from.</param>
    /// <returns>The average point. If the collection is empty, returns a point with default coordinates.</returns>
    public static Point GetAverage(this IEnumerable<Point> points)
    {
        var sum = new Point();
        int count = 0;
        foreach (var point in points)
        {
            sum += point;
            count++;
        }

        if (count == 0)
        {
            return sum;
        }

        return new Vector(sum) * (1.0 / count);
    }

    /// <summary>
    /// Removes duplicate points from the given collection based on a specified tolerance.
    /// </summary>
    /// <param name="points">The collection of points to process.</param>
    /// <param name="tolerance">The tolerance within which points are considered duplicates. Default is 0.01mm.</param>
    /// <returns>An enumerable collection of points with duplicates removed.</returns>
    public static IEnumerable<Point> RemoveDuplicates(this IEnumerable<Point> points, double tolerance = 1e-2)
    {
        HashSet<Point> uniquePoints = new(new PointComparer(tolerance));
        foreach (var point in points)
        {
            if (uniquePoints.Add(point))
            {
                yield return point;
            }
        }
    }

    /// <summary>
    /// Gets the leftmost and rightmost points from a list based on their projections onto given X and Y vectors.
    /// </summary>
    /// <param name="points">The list of points to analyze.</param>
    /// <param name="xVector">The vector defining the X direction for projection.</param>
    /// <param name="yVector">The vector defining the Y direction for projection.</param>
    /// <param name="tolerance">The tolerance value for comparing point positions. Defaults to 0.01mm.</param>
    /// <returns>
    /// A list containing two points: the leftmost point with highest Y projection and the rightmost point with highest Y projection. 
    /// If input list is empty, returns empty list. If edge points are the same, returns a list with a single point.
    /// </returns>
    /// <remarks>
    /// For each edge (left/right), the method selects points with extreme X projections that also have the highest Y projection among points
    /// within the tolerance range of the extreme X value.
    /// </remarks>
    public static List<Point> GetEdgePoints(this IEnumerable<Point> points, Vector xVector, Vector yVector, double tolerance = 1e-2)
    {
        var minXDotValue = double.MaxValue;
        var maxXDotValue = double.MinValue;
        var leftYDotValue = double.MinValue;
        var rightYDotValue = double.MinValue;

        Point? leftEdge = null;
        Point? rightEdge = null;

        foreach (var point in points)
        {
            var xDotValue = point.Dot(xVector);
            var yDotValue = point.Dot(yVector);

            if (xDotValue + tolerance < minXDotValue || (xDotValue - tolerance < minXDotValue && yDotValue > leftYDotValue))
            {
                minXDotValue = xDotValue;
                leftYDotValue = yDotValue;
                leftEdge = point;
                continue;
            }

            if (xDotValue - tolerance > maxXDotValue || (xDotValue + tolerance > maxXDotValue && yDotValue > rightYDotValue))
            {
                maxXDotValue = xDotValue;
                rightYDotValue = yDotValue;
                rightEdge = point;
                continue;
            }
        }

        return (leftEdge, rightEdge) switch
        {
            (null, null) => [],
            (null, _) => [rightEdge],
            (_, null) => [leftEdge],
            (Point left, Point right) when Distance.PointToPoint(left, right) < tolerance => [left],
            _ => [leftEdge, rightEdge]
        };
    }

    /// <summary>
    /// Gets the leftmost and rightmost points from a list based on their projections onto given X and Y vectors.
    /// </summary>
    /// <param name="points">The collection of points to analyze.</param>
    /// <param name="yVector">The vector defining the Y direction of the projection plane.</param>
    /// <param name="cs">The coordinate system to use as reference.</param>
    /// <param name="tolerance">The tolerance value for point comparison. Default is 0.01mm.</param>
    /// <returns>
    /// A list containing two points: the leftmost point with highest Y projection and the rightmost point with highest Y projection. 
    /// If input list is empty, returns empty list. If edge points are the same, returns a list with a single point.
    /// </returns>
    /// <remarks>
    /// For each edge (left/right), the method selects points with extreme X projections that also have the highest Y projection among points
    /// within the tolerance range of the extreme X value.
    /// </remarks>
    public static List<Point> GetEdgePoints(this IEnumerable<Point> points, Vector yVector, CoordinateSystem cs, double tolerance = 1e-2)
    {
        var xVector = yVector.Cross(cs.AxisX.Cross(cs.AxisY));
        return GetEdgePoints(points, xVector, yVector, tolerance);
    }

    /// <summary>
    /// Gets the leftmost and rightmost points from a list based on their projections onto given X and Y vectors.
    /// </summary>
    /// <param name="points">The list of points to process.</param>
    /// <param name="yVector">The directional vector used to determine edge points.</param>
    /// <param name="tolerance">The tolerance value for point comparison calculations. Default value is 0.01mm.</param>
    /// <returns>
    /// A list containing two points: the leftmost point with highest Y projection and the rightmost point with highest Y projection. 
    /// If input list is empty, returns empty list. If edge points are the same, returns a list with a single point.
    /// </returns>
    /// <remarks>
    /// For each edge (left/right), the method selects points with extreme X projections that also have the highest Y projection among points
    /// within the tolerance range of the extreme X value.
    /// </remarks>
    public static List<Point> GetEdgePoints(this IEnumerable<Point> points, Vector yVector, double tolerance = 1e-2)
    {
        var cs = new CoordinateSystem(new Point(0, 0, 0), new Vector(1, 0, 0), new Vector(0, 1, 0));
        return GetEdgePoints(points, yVector, cs, tolerance);
    }

    /// <summary>
    /// Gets all points from the collection that are closest to the specified plane within a given tolerance.
    /// </summary>
    /// <param name="points">The collection of points to analyze.</param>
    /// <param name="plane">The geometric plane to measure distance to.</param>
    /// <param name="tolerance">The maximum allowed difference from the minimum distance. Defaults to 1.</param>
    /// <returns>An enumerable collection of points that are closest to the plane within the specified tolerance.</returns>
    /// <remarks>
    /// The method first finds the minimum distance from any point to the plane, then yields all points
    /// whose distance to the plane differs from this minimum by less than the specified tolerance.
    /// </remarks>
    public static IEnumerable<Point> GetClosestToPlane(this IEnumerable<Point> points, GeometricPlane plane, double tolerance = 1)
    {
        var minDistance = points.Min(p => Distance.PointToPlane(p, plane));
        foreach (var point in points)
        {
            if (Math.Abs(Distance.PointToPlane(point, plane) - minDistance) < tolerance)
            {
                yield return point;
            }
        }
    }

    /// <summary>
    /// Gets the points from the collection that have the minimum distance to the specified coordinate system's XY plane.
    /// </summary>
    /// <param name="points">The collection of points to evaluate.</param>
    /// <param name="cs">The coordinate system defining the reference plane.</param>
    /// <returns>A collection of points that have the minimum distance to the specified plane.</returns>
    /// <remarks>
    /// The method creates a geometric plane using the origin and X-Y axes of the provided coordinate system,
    /// then finds the points with the minimum distance to this plane.
    /// Multiple points may be returned if they share the same minimum distance to the plane.
    /// </remarks>
    public static IEnumerable<Point> GetClosestToPlane(this IEnumerable<Point> points, CoordinateSystem cs)
    {
        var plane = new GeometricPlane(cs.Origin, cs.AxisX, cs.AxisY);
        return GetClosestToPlane(points, plane);
    }

    /// <summary>
    /// Gets the points from the collection that have the minimum distance to the specified coordinate system's XY plane.
    /// </summary>
    /// <param name="points">The collection of points to evaluate.</param>
    /// <returns>A collection of points that have the minimum distance to the specified plane.</returns>
    /// <remarks>
    /// The method creates a geometric plane using the origin and X-Y axes of the provided coordinate system,
    /// then finds the points with the minimum distance to this plane.
    /// Multiple points may be returned if they share the same minimum distance to the plane.
    /// </remarks>
    public static IEnumerable<Point> GetClosestToPlane(this IEnumerable<Point> points)
    {
        var plane = new GeometricPlane(new Point(0, 0, 0), new Vector(1, 0, 0), new Vector(0, 1, 0));
        return GetClosestToPlane(points, plane);
    }

    /// <summary>
    /// Projects a set of points onto a specified geometric plane.
    /// </summary>
    /// <param name="points">The collection of points to be projected.</param>
    /// <param name="plane">The geometric plane onto which the points will be projected.</param>
    /// <returns>An IEnumerable of points projected onto the specified plane.</returns>
    public static IEnumerable<Point> ProjectOnPlane(this IEnumerable<Point> points, GeometricPlane plane)
    {
        foreach (var point in points)
        {
            yield return Projection.PointToPlane(point, plane);
        }
    }

    /// <summary>
    /// Projects a set of points onto a specified plane defined by a coordinate system.
    /// </summary>
    /// <param name="points">The set of points to be projected.</param>
    /// <param name="coordinateSystem">The coordinate system defining the plane onto which the points will be projected.</param>
    /// <returns>An enumerable collection of points projected onto the specified plane.</returns>
    public static IEnumerable<Point> ProjectOnPlane(this IEnumerable<Point> points, CoordinateSystem coordinateSystem)
    {
        var plane = new GeometricPlane(coordinateSystem.Origin, coordinateSystem.AxisX, coordinateSystem.AxisY);
        return ProjectOnPlane(points, plane);
    }

    /// <summary>
    /// Projects a set of points onto a predefined geometric plane.
    /// </summary>
    /// <param name="points">The set of points to be projected.</param>
    /// <returns>An IEnumerable of points projected onto the plane.</returns>
    public static IEnumerable<Point> ProjectOnPlane(this IEnumerable<Point> points)
    {
        var plane = new GeometricPlane(new Point(0, 0, 0), new Vector(1, 0, 0), new Vector(0, 1, 0));
        return ProjectOnPlane(points, plane);
    }
}

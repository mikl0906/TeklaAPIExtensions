using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Extension methods for the Solid class.
/// </summary>
public static class SolidExtensions
{
    /// <summary>
    /// Gets an enumerable collection of faces from a solid.
    /// </summary>
    /// <param name="solid">The solid from which to retrieve faces.</param>
    /// <returns>An IEnumerable collection of Face objects representing all faces in the solid.</returns>
    public static IEnumerable<Face> GetFaces(this Solid solid)
    {
        var faceEnumerator = solid.GetFaceEnumerator();
        while (faceEnumerator.MoveNext())
        {
            yield return faceEnumerator.Current;
        }
    }

    /// <summary>
    /// Gets the faces of the solid that are visible from a given view direction.
    /// </summary>
    /// <param name="solid">The solid from which to get the visible faces.</param>
    /// <param name="viewDirection">The direction of the view to determine visibility of faces.</param>
    /// <returns>An enumerable collection of faces that are visible from the specified view direction.</returns>
    public static IEnumerable<Face> GetVisibleFaces(this Solid solid, Vector viewDirection)
    {
        var faceEnumerator = solid.GetFaceEnumerator();
        while (faceEnumerator.MoveNext())
        {
            var face = faceEnumerator.Current;
            if (face.Normal.Dot(viewDirection) > 0)
            {
                yield return face;
            }
        }
    }

    /// <summary>
    /// Calculates the axis-aligned bounding box (AABB) for the given solid.
    /// </summary>
    /// <param name="solid">The solid for which to calculate the AABB.</param>
    /// <param name="marginVector">
    /// An optional vector to add a margin to the bounding box. 
    /// If not provided, a zero vector (0, 0, 0) is used.
    /// </param>
    /// <returns>
    /// An <see cref="AABB"/> representing the axis-aligned bounding box of the solid, 
    /// optionally expanded by the margin vector.
    /// </returns>
    public static AABB GetAxisAlignedBoundingBox(this Solid solid, Vector? marginVector = null)
    {
        marginVector ??= new Vector(0, 0, 0);
        return new(solid.MinimumPoint - marginVector, solid.MaximumPoint + marginVector);
    }

    /// <summary>
    /// Retrieves all edges from the given solid.
    /// </summary>
    /// <param name="solid">The solid from which to retrieve edges.</param>
    /// <returns>An enumerable collection of edges.</returns>
    public static IEnumerable<Edge> GetEdges(this Solid solid)
    {
        var edgeEnumerator = solid.GetEdgeEnumerator();
        while (edgeEnumerator.MoveNext())
        {
            if (edgeEnumerator.Current is Edge edge)
            {
                yield return edge;
            }
        }
    }

    /// <summary>
    /// Retrieves all the points from the edges of the given solid.
    /// </summary>
    /// <param name="solid">The solid from which to extract the points.</param>
    /// <returns>An enumerable collection of points representing the start and end points of the edges of the solid.</returns>
    public static IEnumerable<Point> GetPoints(this Solid solid)
    {
        var edgeEnumerator = solid.GetEdgeEnumerator();
        while (edgeEnumerator.MoveNext())
        {
            if (edgeEnumerator.Current is Edge edge)
            {
                yield return edge.StartPoint;
                yield return edge.EndPoint;
            }
        }
    }

    /// <summary>
    /// Gets the intersection points of a plane defined by three points with a solid.
    /// </summary>
    /// <param name="solid">The solid to find intersection points with.</param>
    /// <param name="point1">The first point defining the plane.</param>
    /// <param name="point2">The second point defining the plane.</param>
    /// <param name="point3">The third point defining the plane.</param>
    /// <returns>A list of points where the plane intersects with the solid.</returns>
    public static List<Point> GetPlaneIntersectionPoints(this Solid solid, Point point1, Point point2, Point point3)
    {
        var points = new List<Point>();
        var intersectionPoints = solid.GetAllIntersectionPoints(point1, point2, point3);
        while (intersectionPoints.MoveNext())
        {
            if (intersectionPoints.Current is Point point)
            {
                points.Add(point);
            }
        }
        return points;
    }

    /// <summary>
    /// Calculates the intersection points between the given solid and a plane defined by the origin and two vectors.
    /// </summary>
    /// <param name="solid">The solid to intersect with the plane.</param>
    /// <param name="origin">The origin point of the plane.</param>
    /// <param name="xAxis">The vector defining the X-axis direction of the plane.</param>
    /// <param name="yAxis">The vector defining the Y-axis direction of the plane.</param>
    /// <returns>A list of points where the solid intersects with the plane.</returns>
    public static List<Point> GetPlaneIntersectionPoints(this Solid solid, Point origin, Vector xAxis, Vector yAxis)
    {
        return solid.GetPlaneIntersectionPoints(origin, origin + xAxis, origin + yAxis);
    }

    /// <summary>
    /// Calculates the intersection points between the given solid and a plane defined by the provided coordinate system.
    /// </summary>
    /// <param name="solid">The solid to intersect with the plane.</param>
    /// <param name="cs">The coordinate system defining the plane. The plane is defined by the origin and the X and Y axes of this coordinate system.</param>
    /// <returns>A list of points where the solid intersects with the plane.</returns>
    public static List<Point> GetPlaneIntersectionPoints(this Solid solid, CoordinateSystem cs)
    {
        return solid.GetPlaneIntersectionPoints(cs.Origin, cs.Origin + cs.AxisX, cs.Origin + cs.AxisY);
    }

    /// <summary>
    /// Calculates the center point of the given solid.
    /// </summary>
    /// <param name="solid">The solid for which to calculate the center point.</param>
    /// <returns>The center point of the solid.</returns>
    public static Point GetCenterPoint(this Solid solid)
    {
        return new Vector(solid.MinimumPoint + solid.MaximumPoint) * 0.5;
    }
}

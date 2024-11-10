using Tekla.Structures.Geometry3d;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for <see cref="LineSegment"/>.
/// </summary>
public static class LineSegmentExtensions
{
    /// <summary>
    /// Cuts the <see cref="LineSegment"/> by the specified <see cref="GeometricPlane"/>.
    /// </summary>
    /// <param name="lineSegment">The line segment to be cut.</param>
    /// <param name="geometricPlane">The geometric plane used to cut the line segment.</param>
    /// <returns>
    /// A new <see cref="LineSegment"/> representing the portion of the original line segment
    /// that lies on the positive side of the plane, or <c>null</c> if the entire line segment
    /// lies on the negative side of the plane.
    /// </returns>
    public static LineSegment? CutByPlane(this LineSegment lineSegment, GeometricPlane geometricPlane)
    {
        var intersectionPoint = Intersection.LineSegmentToPlane(lineSegment, geometricPlane);

        if (intersectionPoint is null)
        {
            if ((lineSegment.StartPoint - geometricPlane.Origin).Dot(geometricPlane.Normal) >= 0)
            {
                return lineSegment;
            }
            else
            {
                return null;
            }
        }
        else
        {
            if ((lineSegment.StartPoint - geometricPlane.Origin).Dot(geometricPlane.Normal) >= 0)
            {
                return new LineSegment(lineSegment.StartPoint, intersectionPoint);
            }
            else
            {
                return new LineSegment(intersectionPoint, lineSegment.EndPoint);
            }
        }
    }

    /// <summary>
    /// Cuts the given line segment by an axis-aligned bounding box (AABB).
    /// </summary>
    /// <param name="lineSegment">The line segment to be cut.</param>
    /// <param name="box">The axis-aligned bounding box used to cut the line segment.</param>
    /// <returns>
    /// A new <see cref="LineSegment"/> that represents the portion of the original line segment
    /// within the bounding box, or <c>null</c> if the line segment does not intersect the bounding box.
    /// </returns>
    public static LineSegment? CutByAxisAlignedBoundingBox(this LineSegment lineSegment, AABB box)
    {
        List<GeometricPlane> planes =
        [
            // Left plane
            new(box.MinPoint, new Vector(1, 0, 0)),
            // Bottom plane
            new(box.MinPoint, new Vector(0, 1, 0)),
            // Back plane
            new(box.MinPoint, new Vector(0, 0, 1)),
            // Right plane
            new(box.MaxPoint, new Vector(-1, 0, 0)),
            // Top plane
            new(box.MaxPoint, new Vector(0, -1, 0)),
            // Front plane
            new(box.MaxPoint, new Vector(0, 0, -1)),
        ];

        LineSegment? result = lineSegment;

        foreach (var plane in planes)
        {
            result = result.CutByPlane(plane);

            if (result is null)
            {
                return null;
            }
        }

        return result;
    }
}

using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for the <see cref="Part"/> class.
/// </summary>
public static class PartExtensions
{
    /// <summary>
    /// Gets all points from the edges of the given <see cref="Part"/>.
    /// </summary>
    /// <param name="part">The part to get points from.</param>
    /// <returns>An enumerable collection of points from the part's edges.</returns>
    public static IEnumerable<Point> GetAllPoints(this Part part)
    {
        var solid = part.GetSolid();
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
    /// Gets all edges from the given <see cref="Part"/> as line segments.
    /// </summary>
    /// <param name="part">The part to get edges from.</param>
    /// <returns>An enumerable collection of line segments representing the part's edges.</returns>
    public static IEnumerable<LineSegment> GetAllEdges(this Part part)
    {
        var solid = part.GetSolid();
        var edgeEnumerator = solid.GetEdgeEnumerator();
        while (edgeEnumerator.MoveNext())
        {
            if (edgeEnumerator.Current is Edge edge)
            {
                yield return new LineSegment(edge.StartPoint, edge.EndPoint);
            }
        }
    }
}

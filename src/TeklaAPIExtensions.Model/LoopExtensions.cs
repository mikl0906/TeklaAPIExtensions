using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Extension methods for the Loop class.
/// </summary>
/// <remarks>
/// Provides utility methods to work with Loop objects.
/// </remarks>
public static class LoopExtensions
{
    /// <summary>
    /// Gets an enumerable collection of vertices from a Loop object.
    /// </summary>
    /// <param name="loop">The Loop object to get vertices from.</param>
    /// <returns>An IEnumerable of Point objects representing the vertices of the loop.</returns>
    public static IEnumerable<Point> GetVertices(this Loop loop)
    {
        var vertexEnumerator = loop.GetVertexEnumerator();
        while (vertexEnumerator.MoveNext())
        {
            yield return vertexEnumerator.Current;
        }
    }
}

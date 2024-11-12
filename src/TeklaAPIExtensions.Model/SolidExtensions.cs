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
}

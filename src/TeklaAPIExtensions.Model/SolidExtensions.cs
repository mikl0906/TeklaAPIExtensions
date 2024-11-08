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
}

using Tekla.Structures.Solid;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Extension methods for working with face enumeration in Tekla Structures.
/// </summary>
/// <remarks>
/// Provides helpers to turn a face enumerator into a standard enumerable sequence,
/// enabling idiomatic foreach iteration and LINQ queries.
/// </remarks>
public static class FaceEnumeratorExtensions
{
    /// <summary>
    /// Converts the specified face enumerator into a lazily evaluated sequence of faces.
    /// </summary>
    /// <param name="faceEnumerator">
    /// An initialized face enumerator to iterate. Must not be null.
    /// </param>
    /// <returns>
    /// A sequence of faces produced by successive iterations of the provided enumerator.
    /// </returns>
    public static IEnumerable<Face> ToEnumerable(this FaceEnumerator faceEnumerator)
    {
        while (faceEnumerator.MoveNext())
        {
            yield return faceEnumerator.Current;
        }
    }
}

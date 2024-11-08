using Tekla.Structures.Solid;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Extension methods for the Face class.
/// </summary>
/// <remarks>
/// Contains utility methods to work with Face objects.
/// </remarks>
public static class FaceExtensions
{
    /// <summary>
    /// Gets an enumerable collection of loops from a face.
    /// </summary>
    /// <param name="face">The face to get loops from.</param>
    /// <returns>An IEnumerable collection of Loop objects representing the loops in the face.</returns>
    public static IEnumerable<Loop> GetLoops(this Face face)
    {
        var loopEnumerator = face.GetLoopEnumerator();
        while (loopEnumerator.MoveNext())
        {
            yield return loopEnumerator.Current;
        }
    }
}

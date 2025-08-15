using Tekla.Structures.Solid;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for working with Tekla Structures <see cref="EdgeEnumerator"/> instances.
/// </summary>
/// <remarks>
/// These extensions enable converting an <see cref="EdgeEnumerator"/> into an <see cref="System.Collections.Generic.IEnumerable{T}"/>
/// to facilitate LINQ queries and idiomatic iteration with <c>foreach</c>.
/// </remarks>
public static class EdgeEnumeratorExtensions
{
    /// <summary>
    /// Enumerates the edges from the specified <see cref="EdgeEnumerator"/> as a typed sequence.
    /// </summary>
    /// <param name="edgeEnumerator">
    /// The edge enumerator to iterate. Must not be <see langword="null"/>. The enumerator will be consumed.
    /// </param>
    /// <returns>
    /// An <see cref="System.Collections.Generic.IEnumerable{T}"/> of <see cref="Edge"/> items yielded by the enumerator.
    /// </returns>
    public static IEnumerable<Edge> ToEnumerable(this EdgeEnumerator edgeEnumerator)
    {
        while (edgeEnumerator.MoveNext())
        {
            if (edgeEnumerator.Current is Edge edge)
            {
                yield return edge;
            }
        }
    }
}

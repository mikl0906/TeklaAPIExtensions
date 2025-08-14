using Tekla.Structures.Catalogs;

namespace TeklaAPIExtensions.Catalogs;

/// <summary>
/// Provides extension methods for working with rebar catalog item enumerators.
/// </summary>
/// <remarks>
/// These helpers enable idiomatic iteration and LINQ operations over catalog items by exposing
/// enumerators as lazy <see cref="System.Collections.Generic.IEnumerable{T}"/> sequences.
/// </remarks>
public static class RebarItemEnumeratorExtensions
{
    /// <summary>
    /// Converts a <c>RebarItemEnumerator</c> into a lazy <see cref="System.Collections.Generic.IEnumerable{T}"/>
    /// sequence of <c>RebarItem</c> instances.
    /// </summary>
    /// <param name="enumerator">
    /// The rebar item enumerator to consume. The enumerator will be advanced until it is exhausted.
    /// </param>
    /// <returns>
    /// An <see cref="System.Collections.Generic.IEnumerable{T}"/> that yields the items provided by
    /// the specified enumerator as they are requested.
    /// </returns>
    public static IEnumerable<RebarItem> ToEnumerable(this RebarItemEnumerator enumerator)
    {
        while (enumerator.MoveNext())
        {
            yield return enumerator.Current;
        }
    }
}
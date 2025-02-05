using Tekla.Structures.Drawing;

namespace TeklaAPIExtensions.Drawing;

/// <summary>
/// Provides extension methods for <see cref="DrawingEnumerator"/>.
/// </summary>
public static class DrawingEnumeratorExtensions
{
    /// <summary>
    /// Filters the elements of a <see cref="DrawingEnumerator"/> based on a specified type.
    /// </summary>
    /// <typeparam name="T">The type to filter the elements of the enumerator on.</typeparam>
    /// <param name="enumerator">The <see cref="DrawingEnumerator"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input enumerator of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> OfType<T>(this DrawingEnumerator enumerator) where T : Tekla.Structures.Drawing.Drawing
    {
        while (enumerator.MoveNext())
        {
            if (enumerator.Current is T drawing)
            {
                yield return drawing;
            }
        }
    }
}

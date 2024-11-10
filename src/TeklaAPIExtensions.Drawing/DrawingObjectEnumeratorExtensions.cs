using Tekla.Structures.Drawing;

namespace TeklaAPIExtensions.Drawing;

/// <summary>
/// Provides extension methods for <see cref="DrawingObjectEnumerator"/>.
/// </summary>
public static class DrawingObjectEnumeratorExtensions
{
    /// <summary>
    /// Filters the elements of the <see cref="DrawingObjectEnumerator"/> based on the specified type.
    /// </summary>
    /// <typeparam name="T">The type to filter the elements of the enumerator on.</typeparam>
    /// <param name="enumerator">The <see cref="DrawingObjectEnumerator"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input enumerator of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> OfType<T>(this DrawingObjectEnumerator enumerator) where T : DrawingObject
    {
        while (enumerator.MoveNext())
        {
            if (enumerator.Current is T drawingObject)
            {
                yield return drawingObject;
            }
        }
    }
}

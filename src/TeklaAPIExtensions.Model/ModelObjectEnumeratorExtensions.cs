using Tekla.Structures.Model;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for <see cref="ModelObjectEnumerator"/>.
/// </summary>
public static class ModelObjectEnumeratorExtensions
{
    /// <summary>
    /// Filters the elements of the <see cref="ModelObjectEnumerator"/> based on the specified type.
    /// </summary>
    /// <typeparam name="T">The type to filter the elements of the enumerator on.</typeparam>
    /// <param name="enumerator">The <see cref="ModelObjectEnumerator"/> to filter.</param>
    /// <returns>An <see cref="IEnumerable{T}"/> that contains elements from the input enumerator of type <typeparamref name="T"/>.</returns>
    public static IEnumerable<T> OfType<T>(this ModelObjectEnumerator enumerator) where T : ModelObject
    {
        while (enumerator.MoveNext())
        {
            if (enumerator.Current is T modelObject)
            {
                yield return modelObject;
            }
        }
    }
}

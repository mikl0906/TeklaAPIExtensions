using Tekla.Structures.Model;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Extension methods for the ModelObjectEnumerator class.
/// </summary>
public static class ModelObjectEnumeratorExtensions
{
    /// <summary>
    /// Filters the model object enumerator to include only elements of the specified type.
    /// </summary>
    /// <typeparam name="T">The type of model objects to include in the results. Must inherit from ModelObject.</typeparam>
    /// <param name="enumerator">The source ModelObjectEnumerator to filter.</param>
    /// <returns>An IEnumerable containing only the elements of the specified type.</returns>
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

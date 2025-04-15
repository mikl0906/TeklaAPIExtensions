using Tekla.Structures.Model;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Extension methods for the <see cref="BaseComponent"/> class to simplify attribute retrieval.
/// </summary>
/// <remarks>
/// These extension methods provide a more modern and convenient way to get attributes from a <see cref="BaseComponent"/>
/// using the TryGet pattern, which avoids out-parameter initialization and improves code readability.
/// </remarks>
public static class BaseComponentExtensions
{
    /// <summary>
    /// Tries to get an integer attribute value from the component.
    /// </summary>
    /// <param name="baseComponent">The component to get the attribute from.</param>
    /// <param name="attributeName">The name of the attribute to get.</param>
    /// <param name="value">When this method returns, contains the attribute value if found; otherwise, 0.</param>
    /// <returns>true if the attribute was found; otherwise, false.</returns>
    public static bool TryGetAttribute(this BaseComponent baseComponent, string attributeName, out int value)
    {
        value = 0;
        return baseComponent.GetAttribute(attributeName, ref value);
    }

    /// <summary>
    /// Tries to get a double attribute value from the component.
    /// </summary>
    /// <param name="baseComponent">The component to get the attribute from.</param>
    /// <param name="attributeName">The name of the attribute to get.</param>
    /// <param name="value">When this method returns, contains the attribute value if found; otherwise, 0.0.</param>
    /// <returns>true if the attribute was found; otherwise, false.</returns>
    public static bool TryGetAttribute(this BaseComponent baseComponent, string attributeName, out double value)
    {
        value = 0;
        return baseComponent.GetAttribute(attributeName, ref value);
    }

    /// <summary>
    /// Tries to get a string attribute value from the component.
    /// </summary>
    /// <param name="baseComponent">The component to get the attribute from.</param>
    /// <param name="attributeName">The name of the attribute to get.</param>
    /// <param name="value">When this method returns, contains the attribute value if found; otherwise, an empty string.</param>
    /// <returns>true if the attribute was found; otherwise, false.</returns>
    public static bool TryGetAttribute(this BaseComponent baseComponent, string attributeName, out string value)
    {
        value = string.Empty;
        return baseComponent.GetAttribute(attributeName, ref value);
    }
}
using Tekla.Structures.Model;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for the <see cref="ModelObject"/> class to retrieve properties and user-defined attributes (UDAs).
/// </summary>
public static class ModelObjectExtensions
{
    /// <summary>
    /// Attempts to get an integer report property value from a model object.
    /// </summary>
    /// <param name="modelObject">The model object to get the property from.</param>
    /// <param name="attributeName">The name of the report property to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, 0.</param>
    /// <returns>true if the property was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetProperty(this ModelObject modelObject, string attributeName, out int value)
    {
        value = 0;
        return modelObject.GetReportProperty(attributeName, ref value);
    }

    /// <summary>
    /// Attempts to get a double report property value from a model object.
    /// </summary>
    /// <param name="modelObject">The model object to get the property from.</param>
    /// <param name="attributeName">The name of the report property to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, 0.0.</param>
    /// <returns>true if the property was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetProperty(this ModelObject modelObject, string attributeName, out double value)
    {
        value = 0;
        return modelObject.GetReportProperty(attributeName, ref value);
    }

    /// <summary>
    /// Attempts to get a string report property value from a model object.
    /// </summary>
    /// <param name="modelObject">The model object to get the property from.</param>
    /// <param name="attributeName">The name of the report property to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, empty string.</param>
    /// <returns>true if the property was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetProperty(this ModelObject modelObject, string attributeName, out string value)
    {
        value = string.Empty;
        return modelObject.GetReportProperty(attributeName, ref value);
    }

    /// <summary>
    /// Attempts to get an integer user-defined attribute (UDA) value from a model object.
    /// </summary>
    /// <param name="modelObject">The model object to get the UDA from.</param>
    /// <param name="attributeName">The name of the UDA to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, 0.</param>
    /// <returns>true if the UDA was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetUDA(this ModelObject modelObject, string attributeName, out int value)
    {
        value = 0;
        return modelObject.GetUserProperty(attributeName, ref value);
    }

    /// <summary>
    /// Attempts to get a double user-defined attribute (UDA) value from a model object.
    /// </summary>
    /// <param name="modelObject">The model object to get the UDA from.</param>
    /// <param name="attributeName">The name of the UDA to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, 0.0.</param>
    /// <returns>true if the UDA was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetUDA(this ModelObject modelObject, string attributeName, out double value)
    {
        value = 0;
        return modelObject.GetUserProperty(attributeName, ref value);
    }

    /// <summary>
    /// Attempts to get a string user-defined attribute (UDA) value from a model object.
    /// </summary>
    /// <param name="modelObject">The model object to get the UDA from.</param>
    /// <param name="attributeName">The name of the UDA to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, empty string.</param>
    /// <returns>true if the UDA was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetUDA(this ModelObject modelObject, string attributeName, out string value)
    {
        value = string.Empty;
        return modelObject.GetUserProperty(attributeName, ref value);
    }
}

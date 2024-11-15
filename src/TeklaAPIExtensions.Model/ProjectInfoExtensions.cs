using Tekla.Structures.Model;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for the <see cref="ProjectInfo"/> class to retrieve user-defined attributes (UDAs).
/// </summary>
public static class ProjectInfoExtensions
{
    /// <summary>
    /// Attempts to get an integer user-defined attribute (UDA) value from a model object.
    /// </summary>
    /// <param name="projectInfo">The model object to get the UDA from.</param>
    /// <param name="attributeName">The name of the UDA to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, 0.</param>
    /// <returns>true if the UDA was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetUDA(this ProjectInfo projectInfo, string attributeName, out int value)
    {
        value = 0;
        return projectInfo.GetUserProperty(attributeName, ref value);
    }

    /// <summary>
    /// Attempts to get a double user-defined attribute (UDA) value from a model object.
    /// </summary>
    /// <param name="projectInfo">The model object to get the UDA from.</param>
    /// <param name="attributeName">The name of the UDA to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, 0.0.</param>
    /// <returns>true if the UDA was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetUDA(this ProjectInfo projectInfo, string attributeName, out double value)
    {
        value = 0;
        return projectInfo.GetUserProperty(attributeName, ref value);
    }

    /// <summary>
    /// Attempts to get a string user-defined attribute (UDA) value from a model object.
    /// </summary>
    /// <param name="projectInfo">The model object to get the UDA from.</param>
    /// <param name="attributeName">The name of the UDA to retrieve.</param>
    /// <param name="value">When this method returns, contains the retrieved value if found; otherwise, empty string.</param>
    /// <returns>true if the UDA was found and successfully retrieved; otherwise, false.</returns>
    public static bool TryGetUDA(this ProjectInfo projectInfo, string attributeName, out string value)
    {
        value = string.Empty;
        return projectInfo.GetUserProperty(attributeName, ref value);
    }
}

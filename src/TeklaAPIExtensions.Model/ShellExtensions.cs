using Tekla.Structures.Solid;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for the <see cref="Shell"/> type to enumerate its topology.
/// </summary>
public static class ShellExtensions
{
    /// <summary>
    /// Enumerates all edges of the specified <see cref="Shell"/>.
    /// </summary>
    /// <param name="shell">The shell whose edges to enumerate. Must not be null.</param>
    /// <returns>An enumerable sequence of <see cref="Edge"/> instances that belong to the shell.</returns>
    public static IEnumerable<Edge> GetEdges(this Shell shell)
    {
        return shell.GetEdgeEnumerator().ToEnumerable();
    }

    /// <summary>
    /// Enumerates all faces of the specified <see cref="Shell"/>.
    /// </summary>
    /// <param name="shell">The shell whose faces to enumerate. Must not be null.</param>
    /// <returns>An enumerable sequence of <see cref="Face"/> instances that belong to the shell.</returns>
    public static IEnumerable<Face> GetFaces(this Shell shell)
    {
        return shell.GetFaceEnumerator().ToEnumerable();
    }
}
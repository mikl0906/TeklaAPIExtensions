using Tekla.Structures.Geometry3d;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Provides extension methods for transforming <see cref="LineSegment"/> objects using a <see cref="Matrix"/>.
/// </summary>
public static class MatrixExtensions
{
    /// <summary>
    /// Transforms the specified <see cref="LineSegment"/> using the given <see cref="Matrix"/>.
    /// </summary>
    /// <param name="matrix">The transformation matrix.</param>
    /// <param name="lineSegment">The line segment to transform.</param>
    /// <returns>A new <see cref="LineSegment"/> that is the result of the transformation.</returns>
    public static LineSegment Transform(this Matrix matrix, LineSegment lineSegment)
    {
        var startPoint = matrix.Transform(lineSegment.StartPoint);
        var endPoint = matrix.Transform(lineSegment.EndPoint);
        return new LineSegment(startPoint, endPoint);
    }

    /// <summary>
    /// Transforms a collection of <see cref="LineSegment"/> objects using the given <see cref="Matrix"/>.
    /// </summary>
    /// <param name="matrix">The transformation matrix.</param>
    /// <param name="lineSegments">The collection of line segments to transform.</param>
    /// <returns>A collection of transformed <see cref="LineSegment"/> objects.</returns>
    public static IEnumerable<LineSegment> Transform(this Matrix matrix, IEnumerable<LineSegment> lineSegments)
    {
        return lineSegments.Select(matrix.Transform);
    }
}

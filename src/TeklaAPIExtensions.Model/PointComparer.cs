using Tekla.Structures.Geometry3d;

namespace TeklaAPIExtensions.Model;

internal class PointComparer(double tolerance) : IEqualityComparer<Point>
{
    public bool Equals(Point x, Point y) =>
        x is not null &&
        y is not null &&
        Distance.PointToPoint(x, y) < tolerance;

    public int GetHashCode(Point point)
    {
        return Math.Round(point.X / tolerance).GetHashCode()
            ^ Math.Round(point.Y / tolerance).GetHashCode()
            ^ Math.Round(point.Z / tolerance).GetHashCode();
    }
}
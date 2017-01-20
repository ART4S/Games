namespace Labyrinth
{
    public static class DirectionExtensions
    {
        public static Point ToPoint(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Up:
                    return new Point(-1, 0);

                case Direction.Down:
                    return new Point(1, 0);

                case Direction.Left:
                    return new Point(0, -1);

                case Direction.Right:
                    return new Point(0, 1);

                default:
                    return new Point();
            }
        }
    }
}

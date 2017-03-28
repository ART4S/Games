using System;
using System.Drawing;

namespace Paint
{
    public enum MoveDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class MoveDirectionExtensions
    {
        public static Point ToPoint(this MoveDirection direction)
        {
            switch (direction)
            {
                case MoveDirection.Up:
                    return new Point(0, -1);
                case MoveDirection.Down:
                    return new Point(0, 1);
                case MoveDirection.Left:
                    return new Point(-1, 0);
                case MoveDirection.Right:
                    return new Point(1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
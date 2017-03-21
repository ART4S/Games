using System;
using System.Drawing;

namespace Painter
{
    public enum MoveDirrection
    {
        Up,
        Down,
        Left,
        Right
    }

    public static class DirectionExtensions
    {
        public static Point ToPoint(this MoveDirrection direction)
        {
            switch (direction)
            {
                case MoveDirrection.Up:
                    return new Point(0, -1);

                case MoveDirrection.Down:
                    return new Point(0, 1);

                case MoveDirrection.Left:
                    return new Point(-1, 0);

                case MoveDirrection.Right:
                    return new Point(1, 0);

                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
        }
    }
}
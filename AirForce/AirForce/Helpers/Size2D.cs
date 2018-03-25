using System.Drawing;

namespace AirForce
{
    public struct Size2D
    {
        public int Width { get; }
        public int Height { get; }

        public Size2D(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public Size2D(Size size) : this(size.Width, size.Height) { }

        public static implicit operator Size(Size2D size)
        {
            return new Size(size.Width, size.Height);
        }

        public static implicit operator SizeF(Size2D size)
        {
            return new SizeF(size.Width, size.Height);
        }
    }
}

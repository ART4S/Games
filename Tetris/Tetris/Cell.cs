using System.Drawing;

namespace Tetris
{
    public struct Cell
    {
        public bool closed;
        public Brush color;

        public Cell(bool status, Brush brush)
        {
            closed = status;
            color = brush;
        }
    }
}

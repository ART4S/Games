using System.Drawing;

namespace SnakeForm
{
    class Cell
    {
        // CONSTRUCTOR
        public Cell() { status = CellStatus.empty; }

        // PUBLIC
        public Point LinkCell;
        public CellStatus status;
        public Direction DirectionCell;

        public void setLinkCell(int x, int y)
        {
            LinkCell.X = x;
            LinkCell.Y = y;
        }
    }
}

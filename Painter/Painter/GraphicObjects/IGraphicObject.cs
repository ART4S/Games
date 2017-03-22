using System.Drawing;

namespace Paint
{
    public interface IGraphicObject
    {
        void Draw(Graphics graphics);
        void Move(MoveDirection direction, int moveRange);
    }
}
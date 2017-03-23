using System.Drawing;

namespace Paint.GraphicObjects
{
    public abstract class GraphicObject
    {
        public abstract void Draw(Graphics graphics);
        public abstract void Move(MoveDirection direction, int moveRange);
    }
}

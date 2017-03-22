using System.Drawing;

namespace Paint
{
    public interface IGraphicObject
    {
        void Draw(Graphics graphics);
        void Move(MoveDirrection dirrection, int moveRange);
    }
}
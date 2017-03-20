using System.Drawing;

namespace Paint
{
    public abstract class DrawingShape
    {
        protected readonly Pen pen;
        protected readonly TextureBrush textureBrush;

        protected DrawingShape(Pen pen, TextureBrush textureBrush)
        {
            this.textureBrush = textureBrush;
            this.pen = pen;
        }

        public abstract void Draw(Graphics graphics);
        public abstract void Move(MoveDirrection dirrection, int moveRange);
    }
}
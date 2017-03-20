using System.Drawing;

namespace Paint
{
    public abstract class DrawingShape
    {
        protected readonly Pen pen;
        protected readonly TextureBrush textruBrush;

        protected DrawingShape(Pen pen, TextureBrush textruBrush)
        {
            this.textruBrush = textruBrush;
            this.pen = pen;
        }

        public abstract void Draw(Graphics graphics);
    }
}
using System.Drawing;

namespace Paint
{
    public class Polygon : DrawingShape
    {
        private readonly PointF[] points;

        public Polygon(PointF[] points, Pen pen, TextureBrush textureBrush) : base(pen, textureBrush)
        {
            this.points = points;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(pen, points);
            graphics.FillPolygon(textureBrush, points);
        }
    }
}
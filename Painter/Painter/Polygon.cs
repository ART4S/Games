using System.Drawing;
using System.Linq;

namespace Paint
{
    public class Polygon : DrawingShape
    {
        private PointF[] points;

        public Polygon(PointF[] points, Pen pen, TextureBrush textureBrush) : base(pen, textureBrush)
        {
            this.points = points;
        }

        public override void Draw(Graphics graphics)
        {
            graphics.DrawPolygon(pen, points);
            graphics.FillPolygon(textureBrush, points);
        }

        public override void Move(MoveDirrection dirrection, int moveRange)
        {
            Point dirrectionPoint = dirrection.ToPoint();

            points = points.Select(point => new PointF(point.X + moveRange * dirrectionPoint.X, point.Y + moveRange * dirrectionPoint.Y)).ToArray();
        }

        public void clockwiseRotation()
        {
            
        }
    }
}
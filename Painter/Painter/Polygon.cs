using System.Collections.Generic;
using System.Drawing;

namespace Paint
{
    public class Polygon
    {
        private readonly List<Point> points;

        private readonly Pen pen;
        private readonly TextureBrush fillingBrush;

        public Polygon(List<Point> points)
        {
            this.points = points;
        }

        public Polygon() : this(new List<Point>())
        {
            
        }

        public void Draw(Graphics graphics)
        {
            //graphics.DrawPolygon(pen, points);
        }
    }
}
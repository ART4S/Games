using System.Drawing;

namespace SimplePainter
{
    public class BezierCurve
    {
        public PointF firstPoint;
        public PointF secondPoint;

        public PointF firstBendingPoint;
        public PointF secondBendingPoint;

        public BezierCurve(PointF firstPoint, PointF secondPoint, PointF firstBendingPoint, PointF secondBendingPoint)
        {
            this.firstPoint = firstPoint;
            this.secondPoint = secondPoint;
            this.firstBendingPoint = firstBendingPoint;
            this.secondBendingPoint = secondBendingPoint;
        }
    }
}
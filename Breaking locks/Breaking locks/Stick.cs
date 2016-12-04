using System;
using System.Drawing;

namespace Breaking_locks
{
    class Stick
    {
        public PointF P0, P1;
        private float length;

        private double angle;
        public double Angle
        {
            get { return angle; }
            set
            {
                angle = value;
                Move();
            }
        }

        public Stick(PointF point, float angle, float length)
        {
            this.length = length;
            this.angle = angle;

            P0 = point;
            P1 = new PointF(P0.X + length * (float)Math.Cos(angle * Math.PI / 180), P0.Y + length * (float)Math.Sin(angle * Math.PI / 180));
        }

        private void Move()
        {
            P1.X = P0.X + length * (float)Math.Cos(Angle * Math.PI / 180);
            P1.Y = P0.Y + length * (float)Math.Sin(Angle * Math.PI / 180);
        }
    }
}

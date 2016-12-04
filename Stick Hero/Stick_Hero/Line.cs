namespace Stick_Hero
{
    public struct Line
    {
        public float X0, X;
        public float Y0, Y;

        public Line(float x0, float y0, float x, float y)
        {
            X0 = x0;
            Y0 = y0;
            X = x;
            Y = y;
        }
    }
}

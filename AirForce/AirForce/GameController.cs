using System.Drawing;

namespace AirForce
{
    public class GameController
    {

        public void DrawAllElements(Graphics graphics, Size windowSize)
        {
            Line deathLine = new Line(
                new Point(0, windowSize.Height - 30),
                new Point(windowSize.Width, windowSize.Height - 30));

            Pen borderLinePen = new Pen(Color.DarkRed, 4);

            graphics.DrawLine(
                borderLinePen,
                deathLine.FirstPoint,
                deathLine.SecondPoint);
        }
    }
}

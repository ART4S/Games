using System;
using System.Drawing;
using System.Windows.Forms;

namespace AirForce
{
    public class GameController
    {
        private Size gameFieldSize;
        private Line deathLine;

        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            deathLine = new Line(
                new Point(0, gameFieldSize.Height - 30),
                new Point(gameFieldSize.Width, gameFieldSize.Height - 30));
        }

        public void ResizeGameFieldBorders(Size newGameFieldSize)
        {
            gameFieldSize = newGameFieldSize;

            deathLine = new Line(
                new Point(0, gameFieldSize.Height - 30),
                new Point(gameFieldSize.Width, gameFieldSize.Height - 30));

        }

        public void DrawAllElements(Graphics graphics)
        {
            Pen borderLinePen = new Pen(Color.DarkRed, 4);

            graphics.DrawLine(
                borderLinePen,
                deathLine.FirstPoint,
                deathLine.SecondPoint);
        }

        public void ChangePlayerShipBehaviour(Keys pressedKey)
        {
            switch (pressedKey)
            {
                case Keys.W:
                case Keys.Up:
                    break;

                case Keys.D:
                case Keys.Down:
                    break;

                case Keys.Space:
                    break;

                default:
                    new ArgumentOutOfRangeException(nameof(pressedKey), "Not found param!");
                    break;
            }
        }

        // modifier "public"  only for Unit test
        public bool IsBodyOutOfGameFieldSize(Point objectPosition, float objectCollisionRadius)
        {

            return true;
        }
    }
}

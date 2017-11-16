using System;
using System.Drawing;
using System.Windows.Forms;
using AirForce.AirObjects;

namespace AirForce
{
    public class GameController
    {
        private readonly Size gameFieldSize;
        private readonly Line groundLine;

        private readonly PlayerShip playerShip;

        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            groundLine = new Line(
                new Point(0, gameFieldSize.Height - 30),
                new Point(gameFieldSize.Width, gameFieldSize.Height - 30));

            const int colisionRadius = 30;
            Point positionInSpace = new Point(colisionRadius, gameFieldSize.Width / 2);

            playerShip = new PlayerShip(positionInSpace, colisionRadius, 5);
        }

        public void DrawAllElements(Graphics graphics)
        {
            Pen borderLinePen = new Pen(Color.DarkRed, 4);
            Pen playerShipPen = new Pen(Color.Blue, 4);

            // drawing groundLine

            graphics.DrawLine(
                borderLinePen,
                groundLine.FirstPoint,
                groundLine.SecondPoint);

            // drawing playerShip

            graphics.DrawEllipse(
                playerShipPen,
                playerShip.PositionInSpace.X - playerShip.CollisionRadius,
                playerShip.PositionInSpace.Y - playerShip.CollisionRadius,
                playerShip.CollisionRadius * 2,
                playerShip.CollisionRadius * 2);
        }

        public void ChangePlayerShipBehaviour(Keys pressedKey)
        {
            Direction movingDirection = Direction.Empty;

            switch (pressedKey)
            {
                case Keys.W:
                case Keys.Up:
                    movingDirection = Direction.Up;
                    break;

                case Keys.S:
                case Keys.Down:
                    movingDirection = Direction.Down;
                    break;

                case Keys.A:
                case Keys.Left:
                    movingDirection = Direction.Left;
                    break;

                case Keys.D:
                case Keys.Right:
                    movingDirection = Direction.Right;
                    break;

                case Keys.Space:
                    break;

                default:
                    new ArgumentOutOfRangeException(nameof(pressedKey), "Not found param!");
                    break;
            }

            playerShip.Move(movingDirection);
        }

        // modifier "public"  only for Unit test
        public bool IsAirObjectInGameField(AirObject airObject, Point nextPosition)
        {
            bool isAirObjectDoesntCrossLeftBorder =
                nextPosition.X - airObject.CollisionRadius >= 0;

            bool isAirObjectDoesntCrossRightBorder =
                nextPosition.X + airObject.CollisionRadius <= gameFieldSize.Width;

            bool isAirObjectDoesntCrossTopBorder =
                nextPosition.Y - airObject.CollisionRadius >= 0;

            bool isAirObjectDoesntCrossDeathLine =
                nextPosition.Y + airObject.CollisionRadius < groundLine.FirstPoint.Y;

            return isAirObjectDoesntCrossLeftBorder &&
                isAirObjectDoesntCrossRightBorder &&
                isAirObjectDoesntCrossTopBorder &&
                isAirObjectDoesntCrossDeathLine;
        }
    }
}

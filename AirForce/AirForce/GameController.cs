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

        private PlayerShip playerShip;
          
        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            groundLine = new Line(
                new Point(0, gameFieldSize.Height - 30),
                new Point(gameFieldSize.Width, gameFieldSize.Height - 30));

            playerShip = new PlayerShip(gameFieldSize);

            playerShip.DeathObjectEvent += RecreateAirObjectsOnGameField;
        }

        public void DrawAllElements(Graphics graphics)
        {
            Brush groundBrush = Brushes.Green;
            Rectangle groundRectangle = new Rectangle(groundLine.FirstPoint, gameFieldSize);

            graphics.FillRectangle(groundBrush, groundRectangle);

            playerShip.Draw(graphics);
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

            playerShip.Move(movingDirection, gameFieldSize, groundLine);
        }

        private void RecreateAirObjectsOnGameField()
        {
            playerShip = new PlayerShip(gameFieldSize);

            playerShip.DeathObjectEvent += RecreateAirObjectsOnGameField;
        }
    }
}

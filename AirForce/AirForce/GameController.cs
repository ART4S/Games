using System;
using System.Drawing;
using System.Windows.Forms;
using AirForce.AirObjects;
using AirForce.Enums;

namespace AirForce
{
    public sealed class GameController
    {
        private GameStatus gameStatus = GameStatus.Play;

        private readonly Size gameFieldSize;
        private readonly Line groundLine;

        private PlayerShip playerShip;
          
        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            groundLine = new Line(
                new Point(0, gameFieldSize.Height - 30),
                new Point(gameFieldSize.Width, gameFieldSize.Height - 30));

            playerShip = new PlayerShip(gameFieldSize, ChangeGameStatusToWaitingState);
        }

        public void DrawAllElements(Graphics graphics)
        {
            if (gameStatus == GameStatus.Wait)
                DrawWaitingStateString(graphics);

            if (gameStatus == GameStatus.Play)
                playerShip.Draw(graphics);

            DrawGround(graphics);         
        }

        private void DrawGround(Graphics graphics)
        {
            Brush groundBrush = Brushes.Green;
            Rectangle groundRectangle = new Rectangle(groundLine.FirstPoint, gameFieldSize);

            graphics.FillRectangle(groundBrush, groundRectangle);
        }

        private void DrawWaitingStateString(Graphics graphics)
        {
            string contentText = "Press SPACE to start game";

            Font font = new Font("Segoe UI", 12, FontStyle.Bold);
            Brush brush = Brushes.DeepPink;
            Rectangle gameFieldRectangle = new Rectangle(new Point(), gameFieldSize);

            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(contentText, font, brush, gameFieldRectangle, stringFormat);
        }

        public void ChangePlayerShipBehaviour(Keys pressedKey)
        {
            if (gameStatus == GameStatus.Wait)
            {
                if (pressedKey == Keys.Space)
                {
                    RecreateAirObjectsOnGameField();
                    gameStatus = GameStatus.Play;
                }

                return;
            }

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
                    // shoot
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(pressedKey), pressedKey, null);
            }

            playerShip.Move(movingDirection, gameFieldSize, groundLine);
        }

        private void RecreateAirObjectsOnGameField()
        {
            playerShip = new PlayerShip(gameFieldSize, ChangeGameStatusToWaitingState);
        }

        private void ChangeGameStatusToWaitingState()
        {
            gameStatus = GameStatus.Wait;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using AirForce.AirObjects;
using AirForce.Enums;

namespace AirForce
{
    public sealed class GameController
    {
        private GameState gameState = GameState.Play;

        private readonly Size gameFieldSize;
        private readonly Line groundLine;

        private PlayerShip playerShip;

        private readonly Timer airObjectsCreatorTimer = new Timer();

        private readonly List<AirObject> airObjectsList = new List<AirObject>();

        private readonly Random random = new Random();

        /// -------------------------------------------------------

        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            groundLine = new Line(
                new Point(0, gameFieldSize.Height - 30),
                new Point(gameFieldSize.Width, gameFieldSize.Height - 30));

            playerShip = new PlayerShip(gameFieldSize, ChangeGameStatusToWaitingState);

            // airObjectsCreatorTimer setting
            airObjectsCreatorTimer.Interval = 2000;
            airObjectsCreatorTimer.Tick += AddNewAirObject;
            airObjectsCreatorTimer.Start();
        }

        public void DrawAllElements(Graphics graphics)
        {
            MoveAllAirObjects();

            if (gameState == GameState.Wait)
                DrawWaitingStateString(graphics);

            if (gameState == GameState.Play)
                playerShip.Draw(graphics);

            DrawGround(graphics);

            foreach (AirObject airObject in airObjectsList)
                airObject.Draw(graphics);
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
            if (gameState == GameState.Wait)
            {
                if (pressedKey == Keys.Space)
                {
                    CreateAirObjects();
                    gameState = GameState.Play;
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
            }

            playerShip.Move(movingDirection, gameFieldSize, groundLine);
        }

        private void CreateAirObjects()
        {
            playerShip = new PlayerShip(gameFieldSize, ChangeGameStatusToWaitingState);
        }

        private void ChangeGameStatusToWaitingState()
        {
            gameState = GameState.Wait;
        }

        private void AddNewAirObject(object sender, EventArgs e)
        {
            Point position = new Point(
                gameFieldSize.Width + 100,
                random.Next(100, gameFieldSize.Height - 100 - 100)
                );

            airObjectsList.Add(new BigEnemyShip(position, DeathBigEnemyShip));
        }

        private void DeathBigEnemyShip()
        {
            airObjectsList.RemoveAt(0);
        }

        private void MoveAllAirObjects()
        {
            foreach (var airObject in airObjectsList)
                airObject.Move(Direction.Left, gameFieldSize, groundLine);
        }
    }
}

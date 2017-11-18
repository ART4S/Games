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
        private readonly Random random = new Random();

        private Dictionary<EnemyAirObject, int> enemysDictionary = new Dictionary<EnemyAirObject, int>();

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
            airObjectsCreatorTimer.Tick += AddNewRandomEnemyAirObject;
            airObjectsCreatorTimer.Start();
        }

        public void DrawAllElements(Graphics graphics)
        {
            if (gameState == GameState.Wait)
                DrawWaitingStateString(graphics);

            if (gameState == GameState.Play)
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

        public void TryPlayerShipMove(Direction movingDirection)
        {
            if (gameState == GameState.Wait)
                return;

            playerShip.Move(movingDirection, gameFieldSize, groundLine);
        }

        public void TryPlayerShipShoot()
        {
            if (gameState == GameState.Wait)
            {
                gameState = GameState.Play;
                CreateAirObjects();
                return;
            }

            playerShip.Shoot();

        }

        private void CreateAirObjects()
        {
            playerShip = new PlayerShip(gameFieldSize, ChangeGameStatusToWaitingState);
        }

        private void ChangeGameStatusToWaitingState(AirObject sender)
        {
            gameState = GameState.Wait;
        }

        private void AddNewRandomEnemyAirObject(object sender, EventArgs e)
        {
            int randomNumber = random.Next(0, 2);

            switch (randomNumber)
            {
                case 0:
                    enemysDictionary.Add(new BigEnemyShip(new Point(gameFieldSize.Width / 2, gameFieldSize.Height / 2), DeleteEnemy), 1);
                    break;
            }
        }

        private void DeleteEnemy(AirObject sender)
        {
            
        }
    }
}

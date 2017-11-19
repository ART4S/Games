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

        private readonly Timer enemysCreatorTimer = new Timer();
        private readonly Timer enemysMoverTimer = new Timer();

        private readonly Random random = new Random();

        private readonly HashSet<EnemyAirObject> enemysSet = new HashSet<EnemyAirObject>();
        private readonly List<EnemyAirObject> enemyObjectsToDeleteQue = new List<EnemyAirObject>();

        /// -------------------------------------------------------

        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            groundLine = new Line(
                new Point(0, gameFieldSize.Height - 30),
                new Point(gameFieldSize.Width, gameFieldSize.Height - 30));

            playerShip = new PlayerShip(gameFieldSize, ChangeGameStatusToWaitingState);

            // enemysCreatorTimer setting
            enemysCreatorTimer.Interval = 2000;
            enemysCreatorTimer.Tick += AddNewRandomEnemyAirObject;
            enemysCreatorTimer.Start();

            // enemysMoverTimer setting
            enemysMoverTimer.Interval = 1;
            enemysMoverTimer.Tick += ClearEnemyObjectsToDeleteQue;
            enemysMoverTimer.Tick += MoveEnemyAirObjects;
            enemysMoverTimer.Tick += FindCollisionAllAirObjects;
            enemysMoverTimer.Start();
        }

        public void DrawAllElements(Graphics graphics)
        {
            if (gameState == GameState.Wait)
                DrawWaitingStateString(graphics);

            if (gameState == GameState.Play)
                playerShip.Draw(graphics);

            DrawGround(graphics);

            foreach (EnemyAirObject enemyAirObject in enemysSet)
                enemyAirObject.Draw(graphics);
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

            enemysSet.Clear();
            enemyObjectsToDeleteQue.Clear();
        }

        private void ChangeGameStatusToWaitingState()
        {
            gameState = GameState.Wait;
        }

        private void AddNewRandomEnemyAirObject(object sender, EventArgs e)
        {
            int randY = random.Next(60, groundLine.FirstPoint.Y - 60);

            enemysSet.Add(new BigEnemyShip(new Point(gameFieldSize.Width + 60, randY), AddAirObjectToDeleteEnemyAirObjectsQue));
        }

        private void AddAirObjectToDeleteEnemyAirObjectsQue(EnemyAirObject sender)
        {
            enemyObjectsToDeleteQue.Add(sender);
        }

        private void MoveEnemyAirObjects(object sender, EventArgs e)
        {
            foreach (EnemyAirObject enemyAirObject in enemysSet)
                enemyAirObject.Move(groundLine);
        }

        private void ClearEnemyObjectsToDeleteQue(object sender, EventArgs e)
        {
            foreach (EnemyAirObject enemyObject in enemyObjectsToDeleteQue)
                enemysSet.Remove(enemyObject);

            enemyObjectsToDeleteQue.Clear();
        }

        private void FindCollisionAllAirObjects(object sender, EventArgs e)
        {
            foreach (EnemyAirObject enemyAirObject in enemysSet)
            {
                if (Math.Pow(playerShip.Radius + enemyAirObject.Radius, 2) >=
                    Math.Pow(enemyAirObject.PositionInSpace.X - playerShip.PositionInSpace.X, 2)
                    + Math.Pow(enemyAirObject.PositionInSpace.Y - playerShip.PositionInSpace.Y, 2))
                {
                    enemyAirObject.CollisionWithOtherAirObject(playerShip);
                    playerShip.CollisionWithOtherAirObject(enemyAirObject);
                }
            }
        }
    }
}

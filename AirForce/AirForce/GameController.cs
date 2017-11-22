using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AirForce.AirObjects;
using AirForce.AirObjects.Bullets;
using AirForce.AirObjects.EnemyAI;
using AirForce.Enums;

namespace AirForce
{
    public sealed class GameController
    {
        private GameState gameState = GameState.Play;

        private readonly Size gameFieldSize;
        private readonly Line groundLine;

        private readonly PlayerShip playerShip;
        private readonly Point2D playerShipStartPosition;

        private readonly Timer enemyCreatorTimer = new Timer();
        private readonly Timer objectMovingTimer = new Timer();

        private readonly Random random = new Random();

        private readonly HashSet<EnemyAI> enemies = new HashSet<EnemyAI>();
        private readonly List<EnemyAI> enemiesToDelete = new List<EnemyAI>();

        private readonly HashSet<Bullet> bullets = new HashSet<Bullet>();
        private readonly List<Bullet> bulletsToDelete = new List<Bullet>();

        /// -------------------------------------------------------

        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            groundLine = new Line(
                new Point2D(0, gameFieldSize.Height - 30),
                new Point2D(gameFieldSize.Width, gameFieldSize.Height - 30));

            playerShipStartPosition = new Point2D
            {
                X = 150,
                Y = groundLine.FirstPoint.Y / 2
            };

            playerShip = new PlayerShip(playerShipStartPosition, 30, 6, () => gameState = GameState.Wait);

            // enemyCreatorTimer setting
            enemyCreatorTimer.Interval = 100; // 1500
            enemyCreatorTimer.Tick += (s, e) => AddNewRandomEnemy();
            enemyCreatorTimer.Start();

            // objectMovingTimer setting
            objectMovingTimer.Interval = 1;
            objectMovingTimer.Tick += (s, e) => MoveEnemies();
            objectMovingTimer.Tick += (s, e) => MoveBullets();
            objectMovingTimer.Tick += (s, e) => FindAllAirObjectsCollisions();
            objectMovingTimer.Tick += (s, e) => ClearEnemiesToDelete();
            objectMovingTimer.Tick += (s, e) => ClearBulletsToDelete();
            objectMovingTimer.Start();
        }

        public void Restart()
        {
            enemiesToDelete.Clear();
            bulletsToDelete.Clear();

            enemies.Clear();
            bullets.Clear();

            playerShip.Restore(playerShipStartPosition);

            gameState = GameState.Play;
        }

        private void FindAllAirObjectsCollisions()
        {
            List<AirObject> allAirObjets = new List<AirObject>();

            allAirObjets.AddRange(enemies);
            allAirObjets.AddRange(bullets);
            allAirObjets.Add(playerShip);

            for (int i = 0; i < allAirObjets.Count; i++)
                for (int j = i + 1; j < allAirObjets.Count; j++)
                    if (IsAirObjectsHaveCollision(allAirObjets[i], allAirObjets[j]))
                    {
                        allAirObjets[i].CollisionWithOtherAirObject(allAirObjets[j]);
                        allAirObjets[j].CollisionWithOtherAirObject(allAirObjets[i]);
                    }
        }

        private bool IsAirObjectsHaveCollision(AirObject airObject1, AirObject airObject2)
        {
            return Math.Pow(airObject1.Radius + airObject2.Radius, 2) >=
                   Math.Pow(airObject1.Position.X - airObject2.Position.X, 2)
                   + Math.Pow(airObject1.Position.Y - airObject2.Position.Y, 2);
        }

        #region playerMethods

        public void TryPlayerShipMove(int playerMovespeedModiferX, int playerMovespeedModiferY)
        {
            if (gameState == GameState.Wait)
                return;

            playerShip.Move(playerMovespeedModiferX, playerMovespeedModiferY, gameFieldSize, groundLine);
        }

        public void TryCreatePlayerBullet()
        {
            if (gameState == GameState.Wait)
            {
                gameState = GameState.Play;
                Restart();
                return;
            }

            Point2D bulletStartPosition = new Point2D
            {
                X = playerShip.Position.X + playerShip.Radius,
                Y = playerShip.Position.Y
            };

            bullets.Add(new PlayerBullet(bulletStartPosition, AddBulletInBulletsToDelete));
        }

        #endregion playerMethods

        #region enemiesMethods

        private void AddNewRandomEnemy()
        {
            Point2D startPosition;
            int radius;
            int movespeedShift;

            int randomNumber = random.Next(0, 4);

            switch (randomNumber)
            {
                case 0:
                    radius = 50;
                    movespeedShift = 5;
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(radius, groundLine.FirstPoint.Y - radius)
                    };

                    enemies.Add(new BigShip(startPosition, radius, movespeedShift, AddEnemyInEnemiesToDelete));
                    break;

                case 1:
                    radius = 30;
                    movespeedShift = 1; //
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(radius, groundLine.FirstPoint.Y - radius)
                    };

                    enemies.Add(new ChaserShip(startPosition, radius, movespeedShift, AddEnemyInEnemiesToDelete, CreateEnemyBullet, playerShip));
                    break;

                case 2:
                    radius = 15;
                    movespeedShift = 2;
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(groundLine.FirstPoint.Y - 10 * radius, groundLine.FirstPoint.Y - radius)
                    };

                    enemies.Add(new Bird(startPosition, radius, movespeedShift, AddEnemyInEnemiesToDelete));
                    break;

                case 3:
                    radius = 70;
                    movespeedShift = 3;
                    startPosition = new Point2D
                    {
                        X = random.Next(0, gameFieldSize.Width),
                        Y = 0
                    };

                    enemies.Add(new Meteor(startPosition, radius, movespeedShift, AddEnemyInEnemiesToDelete));
                    break;
            }
        }

        private void MoveEnemies()
        {
            foreach (EnemyAI enemy in enemies)
                enemy.Move(groundLine, bullets);
        }

        private void AddEnemyInEnemiesToDelete(EnemyAI enemy)
        {
            enemiesToDelete.Add(enemy);
        }

        private void ClearEnemiesToDelete()
        {
            foreach (EnemyAI enemyToDelete in enemiesToDelete)
                enemies.Remove(enemyToDelete);

            enemiesToDelete.Clear();
        }

        #endregion enemiesMethods

        #region bulletsMethods

        private void CreateEnemyBullet(Point2D enemyBulletStartPoint)
        {
            bullets.Add(new EnemyBullet(enemyBulletStartPoint, AddBulletInBulletsToDelete));
        }

        private void MoveBullets()
        {
            foreach (Bullet bullet in bullets)
                bullet.Move(gameFieldSize);
        }

        private void AddBulletInBulletsToDelete(Bullet bullet)
        {
            bulletsToDelete.Add(bullet);
        }

        private void ClearBulletsToDelete()
        {
            foreach (Bullet bulletToDelete in bulletsToDelete)
                bullets.Remove(bulletToDelete);

            bulletsToDelete.Clear();
        }

        #endregion bulletsMethods

        #region drawingMethods

        public void DrawAllElements(Graphics graphics)
        {
            foreach (EnemyAI enemy in enemies)
                enemy.Draw(graphics);

            foreach (Bullet bullet in bullets)
                bullet.Draw(graphics);

            DrawGround(graphics);
            DrawPlayerStrength(graphics);
            playerShip.Draw(graphics);

            if (gameState == GameState.Wait)
                DrawWaitingStateString(graphics);
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

        private void DrawPlayerStrength(Graphics graphics)
        {
            Image image = Properties.Resources.heart;

            for (int i = 0; i < playerShip.Strength; i++)
                graphics.DrawImage(image, new Rectangle(location: new Point2D(i * 20, 0), size: new Size(20, 20)));
        }

        #endregion drawingMethods
    }
}

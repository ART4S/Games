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

        private PlayerShip playerShip;

        private readonly Timer enemyCreatorTimer = new Timer();
        private readonly Timer enemyMovingTimer = new Timer();

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

            playerShip = new PlayerShip(gameFieldSize, () => gameState = GameState.Wait);

            // enemyCreatorTimer setting
            enemyCreatorTimer.Interval = 1;
            enemyCreatorTimer.Tick += AddNewRandomEnemyAI;

            enemyCreatorTimer.Start();

            // enemyMovingTimer setting
            enemyMovingTimer.Interval = 1;
            enemyMovingTimer.Tick += MoveEnemies;
            enemyMovingTimer.Tick += MoveBullets;
            enemyMovingTimer.Tick += FindAllAirObjectsCollisions;
            enemyMovingTimer.Tick += ClearEnemiesToDelete;
            enemyMovingTimer.Tick += ClearBulletsToDelete;

            enemyMovingTimer.Start();
        }

        public void TryPlayerShipMove(Direction movingDirection)
        {
            if (gameState == GameState.Wait)
                return;

            playerShip.Move(movingDirection, gameFieldSize, groundLine);
        }

        public void TryCreatePlayerBullet()
        {
            if (gameState == GameState.Wait)
            {
                gameState = GameState.Play;
                Restart();
                return;
            }

            Point2D playerBulletStartPosition = new Point2D(playerShip.Position.X + playerShip.Radius, playerShip.Position.Y);

            bullets.Add(new PlayerBullet(playerBulletStartPosition, AddBulletInBulletsToDelete));
        }

        public void Restart()
        {
            gameState = GameState.Play;

            playerShip = new PlayerShip(gameFieldSize, () => gameState = GameState.Wait);

            enemies.Clear();
            bullets.Clear();

            enemiesToDelete.Clear();
            bulletsToDelete.Clear();
        }

        private void FindAllAirObjectsCollisions(object sender, EventArgs e)
        {
            List<EnemyBullet> enemyBullets = bullets.OfType<EnemyBullet>().ToList();
            List<PlayerBullet> playerBullets = bullets.OfType<PlayerBullet>().ToList();

            foreach (EnemyAI enemy in enemies)
            {
                if (IsAirObjectsHaveCollision(playerShip, enemy))
                {
                    enemy.CollisionWithOtherAirObject(playerShip);
                    playerShip.CollisionWithOtherAirObject(enemy);
                }

                foreach (PlayerBullet playerBullet in playerBullets)
                {
                    if (IsAirObjectsHaveCollision(enemy, playerBullet))
                    {
                        enemy.CollisionWithOtherAirObject(playerBullet);
                        playerBullet.CollisionWithOtherAirObject(enemy);
                    }
                }
            }

            foreach (EnemyBullet enemyBullet in enemyBullets)
            {
                if (IsAirObjectsHaveCollision(playerShip, enemyBullet))
                {
                    enemyBullet.CollisionWithOtherAirObject(playerShip);
                    playerShip.CollisionWithOtherAirObject(enemyBullet);
                }
            }
        }

        private bool IsAirObjectsHaveCollision(AirObject firstAirObject, AirObject secondAirObject)
        {
            return Math.Pow(firstAirObject.Radius + secondAirObject.Radius, 2) >=
                   Math.Pow(firstAirObject.Position.X - secondAirObject.Position.X, 2)
                   + Math.Pow(firstAirObject.Position.Y - secondAirObject.Position.Y, 2);
        }



        #region enemiesMethods

        private void AddNewRandomEnemyAI(object sender, EventArgs e)
        {
            int randY = random.Next(60, groundLine.FirstPoint.Y - 60);

            enemies.Add(new BigShip(new Point2D(gameFieldSize.Width + 60, randY), AddEnemyInEnemiesToDelete));

            enemies.Add(new ChaserShip(new Point2D(gameFieldSize.Width + 60, randY), AddEnemyInEnemiesToDelete, CreateEnemyBullet, playerShip));
            //enemies.Add(new ChaserShip(new Point2D(gameFieldSize.Width, gameFieldSize.Height / 2), AddEnemyInEnemiesToDelete, CreateEnemyBullet, playerShip));

            //enemies.Add(new Bird(new Point2D(gameFieldSize.Width - 60, groundLine.FirstPoint.Y - 100 ), AddEnemyInEnemiesToDelete));
            enemies.Add(new Bird(new Point2D(gameFieldSize.Width - 60, randY), AddEnemyInEnemiesToDelete));

            enemies.Add(new Meteor(new Point2D(random.Next(0, gameFieldSize.Width), 0), AddEnemyInEnemiesToDelete));
            //random.Next(100, gameFieldSize.Width)
        }

        private void MoveEnemies(object sender, EventArgs e)
        {
            foreach (EnemyAI enemy in enemies)
                enemy.Move(groundLine);
        }

        private void AddEnemyInEnemiesToDelete(EnemyAI enemy)
        {
            enemiesToDelete.Add(enemy);
        }

        private void ClearEnemiesToDelete(object sender, EventArgs e)
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

        private void MoveBullets(object sender, EventArgs e) //
        {
            foreach (Bullet bullet in bullets)
                bullet.Move(gameFieldSize);
        }

        private void AddBulletInBulletsToDelete(Bullet bullet)
        {
            bulletsToDelete.Add(bullet);
        }

        private void ClearBulletsToDelete(object sender, EventArgs e)
        {
            foreach (Bullet bulletToDelete in bulletsToDelete)
                bullets.Remove(bulletToDelete);

            bulletsToDelete.Clear();
        }

        #endregion bulletsMethods



        #region drawingMethods

        public void DrawAllElements(Graphics graphics)
        {
            DrawGround(graphics);

            foreach (EnemyAI enemy in enemies)
                enemy.Draw(graphics);

            foreach (Bullet bullet in bullets)
                bullet.Draw(graphics);

            if (gameState == GameState.Wait)
                DrawWaitingStateString(graphics);

            if (gameState == GameState.Play)
                playerShip.Draw(graphics);
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

        #endregion drawingMethods
    }
}

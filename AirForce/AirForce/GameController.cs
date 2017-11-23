﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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

        private readonly PlayerShip playerShip;
        private readonly Point2D playerShipStartPosition;

        private readonly Timer enemyCreatorTimer = new Timer();
        private readonly Timer objectMovingTimer = new Timer();

        private readonly Random random = new Random();

        private readonly List<AirObject> airObjects = new List<AirObject>();
        private readonly List<EnemyBullet> enemyBulletsToAdd = new List<EnemyBullet>();

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

            playerShip = new PlayerShip(playerShipStartPosition, 30, 6);

            // enemyCreatorTimer setting
            enemyCreatorTimer.Interval = 1000; // 1500
            enemyCreatorTimer.Tick += (s, e) => AddNewRandomEnemy();
            enemyCreatorTimer.Start();

            // objectMovingTimer setting
            objectMovingTimer.Interval = 1;
            objectMovingTimer.Tick += (s, e) => Update();
            objectMovingTimer.Start();
        }

        private void Update()
        {
            airObjects.AddRange(enemyBulletsToAdd);
            enemyBulletsToAdd.Clear();
            airObjects.ForEach(o => o.Move(gameFieldSize, groundLine, airObjects));
            FindAllAirObjectsCollisions();
        }

        public void Restart()
        {
            foreach (ChaserShip chaserShip in airObjects.OfType<ChaserShip>())
                chaserShip.Shooted -= AddEnemyBulletInEnemyBulletsToAdd;

            airObjects.Clear();

            playerShip.Restore(playerShipStartPosition, 5);

            gameState = GameState.Play;
        }

        private void FindAllAirObjectsCollisions()
        {
            for (int i = 0; i < airObjects.Count; i++)
            {
                for (int j = i + 1; j < airObjects.Count; j++)
                    if (IsAirObjectsHaveCollision(airObjects[i], airObjects[j]))
                    {
                        airObjects[i].CollisionWithOtherAirObject(airObjects[j]);
                        airObjects[j].CollisionWithOtherAirObject(airObjects[i]);
                    }

                if (IsAirObjectsHaveCollision(airObjects[i], playerShip))
                {
                    airObjects[i].CollisionWithOtherAirObject(playerShip);
                    playerShip.CollisionWithOtherAirObject(airObjects[i]);
                }
            }

            foreach (ChaserShip deathChaserShip in airObjects.OfType<ChaserShip>().Where(x => x.Durability <= 0))
                deathChaserShip.Shooted -= AddEnemyBulletInEnemyBulletsToAdd;

            airObjects.RemoveAll(o => o.Durability <= 0);

            if (playerShip.Durability <= 0)
            {
                gameState = GameState.Wait;
                playerShip.Restore(new Point2D(-200, -200), -1);
            }
        }

        private bool IsAirObjectsHaveCollision(AirObject airObject1, AirObject airObject2)
        {
            return Math.Pow(airObject1.Radius + airObject2.Radius, 2) >=
                   Math.Pow(airObject1.Position.X - airObject2.Position.X, 2)
                   + Math.Pow(airObject1.Position.Y - airObject2.Position.Y, 2);
        }

        public void TryPlayerShipMove(int playerMovespeedModiferX, int playerMovespeedModiferY)
        {
            if (gameState == GameState.Wait)
                return;

            playerShip.Move(playerMovespeedModiferX, playerMovespeedModiferY, gameFieldSize, groundLine);
        }

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

                    airObjects.Add(new BigShip(startPosition, radius, movespeedShift));
                    break;

                case 1:
                    radius = 30;
                    movespeedShift = 1; //
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(radius, groundLine.FirstPoint.Y - radius)
                    };

                    ChaserShip cs = new ChaserShip(startPosition, radius, movespeedShift, playerShip);
                    cs.Shooted += AddEnemyBulletInEnemyBulletsToAdd;

                    airObjects.Add(cs);
                    break;

                case 2:
                    radius = 15;
                    movespeedShift = 2;
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(groundLine.FirstPoint.Y - 10 * radius, groundLine.FirstPoint.Y - radius)
                    };

                    airObjects.Add(new Bird(startPosition, radius, movespeedShift));
                    break;

                case 3:
                    radius = 70;
                    movespeedShift = 3;
                    startPosition = new Point2D
                    {
                        X = random.Next(0, gameFieldSize.Width),
                        Y = 0
                    };

                    airObjects.Add(new Meteor(startPosition, radius, movespeedShift));
                    break;
            }
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

            airObjects.Add(new PlayerBullet(bulletStartPosition, 10, 10));
        }

        private void AddEnemyBulletInEnemyBulletsToAdd(Point2D enemyBulletStartPoint)
        {
            enemyBulletsToAdd.Add(new EnemyBullet(enemyBulletStartPoint, 10, 10));
        }

        #region drawingMethods

        public void DrawAllElements(Graphics graphics)
        {
            airObjects.ForEach(o => o.Draw(graphics));

            playerShip.Draw(graphics);

            DrawGround(graphics);
            DrawPlayerDurabulity(graphics);

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

        private void DrawPlayerDurabulity(Graphics graphics)
        {
            Image image = Properties.Resources.heart;

            for (int i = 0; i < playerShip.Durability; i++)
                graphics.DrawImage(image, new Rectangle(location: new Point2D(i * 20, 0), size: new Size(20, 20)));
        }

        #endregion drawingMethods
    }
}

using System;
using System.Linq;
using System.Drawing;
using AirForce.Enums;
using AirForce.AirObjects;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AirForce
{
    public sealed class GameController
    {
        private GameState gameState = GameState.Play;

        private readonly Size gameFieldSize;
        private readonly Line groundLine;

        private readonly PlayerShip player;
        private readonly Point2D playerStartPosition;

        private readonly List<AirObject> airObjects = new List<AirObject>();

        private readonly Timer enemiesCreatorTimer = new Timer();
        private readonly Timer airObjectsMovingTimer = new Timer();

        private readonly Random random = new Random();

        /// -------------------------------------------------------

        public GameController(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;

            groundLine = new Line(
                new Point2D(0, gameFieldSize.Height - 30),
                new Point2D(gameFieldSize.Width, gameFieldSize.Height - 30)
            );

            playerStartPosition = new Point2D
            {
                X = 150,
                Y = groundLine.FirstPoint.Y / 2
            };

            player = new PlayerShip(playerStartPosition, 30, 4);

            // enemiesCreatorTimer setting
            enemiesCreatorTimer.Interval = 1000;
            enemiesCreatorTimer.Tick += (s, e) => AddNewRandomEnemy();
            enemiesCreatorTimer.Start();

            // airObjectsMovingTimer setting
            airObjectsMovingTimer.Interval = 1;
            airObjectsMovingTimer.Tick += (s, e) => Update();
            airObjectsMovingTimer.Start();         
        }

        public void TryMovePlayer(Point2D movespeedModifer)
        {
            if (gameState == GameState.Wait)
                return;

            player.Move(movespeedModifer, gameFieldSize, groundLine);
        }

        public void TryCreatePlayerBullet()
        {
            if (gameState == GameState.Wait)
            {
                Restart();
                return;
            }

            Point2D bulletStartPosition = new Point2D
            {
                X = player.Position.X + player.Radius,
                Y = player.Position.Y
            };

            airObjects.Add(new PlayerBullet(bulletStartPosition, 8, 8));
        }

        private void Update()
        {
            // adding new enemy bullets
            List<EnemyBullet> newEnemyBullets = airObjects
                .OfType<ChaserShip>()
                .Where(x => x.IsShoting)
                .Select(x => new EnemyBullet(new Point2D(x.Position.X - x.Radius, x.Position.Y), 8, 8))
                .ToList();

            airObjects.AddRange(newEnemyBullets);
            airObjects.ForEach(a => a.Move(gameFieldSize, groundLine, airObjects));
            FindAirObjectsAllCollisions();
        }

        private void Restart()
        {
            airObjects.Clear();
            player.Refresh(playerStartPosition, 100);
            gameState = GameState.Play;
        }

        private void FindAirObjectsAllCollisions()
        {
            for (int i = 0; i < airObjects.Count; i++)
            {
                for (int j = i + 1; j < airObjects.Count; j++)
                    if (IsAirObjectsHaveCollision(airObjects[i].Position, airObjects[i].Radius, airObjects[j].Position, airObjects[j].Radius))
                    {
                        airObjects[i].CollisionWithOtherAirObject(airObjects[j]);
                        airObjects[j].CollisionWithOtherAirObject(airObjects[i]);
                    }

                if (IsAirObjectsHaveCollision(player.Position, player.Radius, airObjects[i].Position, airObjects[i].Radius))
                {
                    player.CollisionWithOtherAirObject(airObjects[i]);
                    airObjects[i].CollisionWithOtherAirObject(player);
                }
            }

            airObjects.RemoveAll(a => a.Durability <= 0);

            if (player.Durability <= 0)
            {
                gameState = GameState.Wait;
                player.Refresh(new Point2D(-200, -200), 0);
            }
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
                    movespeedShift = 8;
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(radius, groundLine.FirstPoint.Y - radius)
                    };

                    airObjects.Add(new BigShip(startPosition, radius, movespeedShift));
                    break;

                case 1:
                    radius = 30;
                    movespeedShift = 3;
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(radius, groundLine.FirstPoint.Y - radius)
                    };

                    airObjects.Add(new ChaserShip(startPosition, radius, movespeedShift, player));
                    break;

                case 2:
                    radius = 25;
                    movespeedShift = 2;
                    startPosition = new Point2D
                    {
                        X = gameFieldSize.Width + radius,
                        Y = random.Next(groundLine.FirstPoint.Y - 5 * radius, groundLine.FirstPoint.Y - radius)
                    };

                    airObjects.Add(new FlyingSaucer(startPosition, radius, movespeedShift));
                    break;

                case 3:
                    radius = 100;
                    movespeedShift = 2;
                    startPosition = new Point2D
                    {
                        X = random.Next(0, gameFieldSize.Width),
                        Y = 0
                    };

                    airObjects.Add(new Meteor(startPosition, radius, movespeedShift));
                    break;
            }
        }

        public static bool IsAirObjectsHaveCollision(Point2D obj1Position, int obj1Radius, Point2D obj2Position, int obj2Radius)
        {
            return Math.Pow(obj1Radius + obj2Radius, 2) >=
                   Math.Pow(obj1Position.X - obj2Position.X, 2)
                   + Math.Pow(obj1Position.Y - obj2Position.Y, 2);
        }

        #region drawingMethods

        public void DrawAllElements(Graphics graphics)
        {
            DrawPlayerHealthBar(graphics);

            DrawGround(graphics);

            airObjects.ForEach(o => o.Draw(graphics));

            player.Draw(graphics);
            
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
            string message = "Press SPACE to start game";

            Font font = new Font("Segoe UI", 20, FontStyle.Bold);
            Brush brush = Brushes.White;
            Rectangle gameFieldRectangle = new Rectangle(new Point(), gameFieldSize);

            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(message, font, brush, gameFieldRectangle, stringFormat);
        }

        private void DrawPlayerHealthBar(Graphics graphics)
        {
            // frame
            Pen framePen = new Pen(Color.White, 3);
            Rectangle frameRectangle = new Rectangle
            {
                Location = new Point2D(4, 4),
                Size = new Size(303, 33)
            };

            graphics.DrawRectangle(framePen, frameRectangle);

            // healthBar
            Brush healthBarBrush = Brushes.Red;
            Rectangle healthBarRectangle = new Rectangle
            {
                Location = new Point2D(6, 6),
                Size = new Size(3 * player.Durability, 30)
            };

            graphics.FillRectangle(healthBarBrush, healthBarRectangle);

            // text
            string text = player.Durability.ToString();
            Font textPen = new Font("Segoe UI", 13, FontStyle.Bold);
            Brush textBrush = Brushes.White;
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            graphics.DrawString(text, textPen, textBrush, frameRectangle, stringFormat);
        }

        #endregion drawingMethods
    }
}

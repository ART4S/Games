using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirForce.AirObjects
{
    public sealed class ChaserShip : AirObject
    {
        private readonly PlayerShip playerShip;

        private readonly Timer shootTimer = new Timer();
        private int shootingCooldown;
        public bool IsShoting { get; private set; }

        public ChaserShip(Point2D position, int radius, int movespeed, PlayerShip playerShip)
            : base(position, radius, movespeed, Properties.Resources.enemy_ship)
        {
            this.playerShip = playerShip;

            // shootTimer setting
            shootTimer.Interval = 1;
            shootTimer.Tick += (s, e) => ChangeShootingCooldown();
            shootTimer.Start();
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            List<PlayerBullet> playerBullets = airObjects
                .OfType<PlayerBullet>()
                .ToList();

            TryDodgePlayerBullets(playerBullets, groundLine);

            Position -= new Point2D(Movespeed, 0);

            if (IsPositionOutOfGameFieldLeftBorder(Position))
                Durability = 0;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case PlayerShip _:
                case PlayerBullet _:
                case Meteor _:
                    Durability = 0;
                    break;
            }
        }

        private void TryDodgePlayerBullets(List<PlayerBullet> playerBullets, Line groundLine)
        {         
            List<Point2D> minPathToFreeTrajectory = FindMinPathToFreeTrajectory(playerBullets, groundLine);

            if (!minPathToFreeTrajectory.Any())
                return;

            if (minPathToFreeTrajectory.First().Y < Position.Y)
                Position -= new Point2D(0, Movespeed);

            if (minPathToFreeTrajectory.First().Y > Position.Y)
                Position += new Point2D(0, Movespeed);
        }

        private List<Point2D> FindMinPathToFreeTrajectory(List<PlayerBullet> playerBullets, Line groundLine)
        {
            var queue = new Queue<KeyValuePair<Point2D, Point2D>>();
            var savePaths = new Dictionary<Point2D, Point2D>();

            savePaths[Position] = Position;
            queue.Enqueue(new KeyValuePair<Point2D, Point2D>(Position, new Point2D()));

            while (queue.Any())
            {
                KeyValuePair<Point2D, Point2D> pair = queue.Dequeue();

                Point2D currentPosition = pair.Key;
                Point2D currentBulletsShift = pair.Value;

                bool isBulletsInFront = playerBullets.Any(x => IsBulletInFront(currentPosition, x.Position + currentBulletsShift, x.Radius));
                bool isHaveCollisionWithBullets = playerBullets.Any(x => IsHaveCollisionToBullet(currentPosition, x.Position + currentBulletsShift, x.Radius));

                if (!isBulletsInFront || IsPositionOutOfGameFieldLeftBorder(currentPosition))
                    return RestorePath(savePaths, currentPosition);

                if (isHaveCollisionWithBullets ||
                    IsPositionOutOfGroundLine(currentPosition, groundLine) ||
                    IsPositionOutOfGameFieldTopBorder(currentPosition))
                    break;

                Point2D moveUpPosition = currentPosition - new Point2D(Movespeed, Movespeed);
                Point2D moveDownPosition = currentPosition - new Point2D(Movespeed, -Movespeed);

                Point2D newBulletsShift = new Point2D(currentBulletsShift.X + playerBullets.First().Movespeed, 0);

                if (!savePaths.ContainsKey(moveUpPosition))
                {
                    savePaths[moveUpPosition] = currentPosition;
                    queue.Enqueue(new KeyValuePair<Point2D, Point2D>(moveUpPosition, newBulletsShift));
                }

                if (!savePaths.ContainsKey(moveDownPosition))
                {
                    savePaths[moveDownPosition] = currentPosition;
                    queue.Enqueue(new KeyValuePair<Point2D, Point2D>(moveDownPosition, newBulletsShift));
                }
            }

            return new List<Point2D>();
        }

        private List<Point2D> RestorePath(Dictionary<Point2D, Point2D> savePaths, Point2D endPoint)
        {
            List<Point2D> path = new List<Point2D>();
            Point2D currentPoint = endPoint;

            if (savePaths.ContainsKey(currentPoint))
            {
                while (savePaths[currentPoint] != currentPoint)
                {
                    path.Add(currentPoint);
                    currentPoint = savePaths[currentPoint];
                }
            }

            path.Reverse();

            return path;
        }

        private bool IsBulletInFront(Point2D chaserShipPosition, Point2D bulletPosition, int bulletRadius)
        {
            int chaserShipTopBorderY = chaserShipPosition.Y - Radius;
            int chaserShipBottomBorderY = chaserShipPosition.Y + Radius;

            int bulletTopBorderY = bulletPosition.Y - bulletRadius;
            int bulletBottomBorderY = bulletPosition.Y + bulletRadius;

            bool isHaveMutualX = bulletPosition.X - bulletRadius <= chaserShipPosition.X - Radius;
            bool isHaveMutualY = Math.Max(bulletTopBorderY, chaserShipTopBorderY) <= Math.Min(bulletBottomBorderY, chaserShipBottomBorderY);

            return isHaveMutualX && isHaveMutualY;
        }

        private bool IsHaveCollisionToBullet(Point2D chaserShipPosition, Point2D bulletPosition, int bulletRadius)
        {
            return Math.Pow(Radius + bulletRadius, 2) >=
                   Math.Pow(chaserShipPosition.X - bulletPosition.X, 2)
                   + Math.Pow(chaserShipPosition.Y - bulletPosition.Y, 2);
        }

        private void ChangeShootingCooldown()
        {
            if (!playerShip.IsInFrontAirObject(this))
            {
                shootingCooldown = 0;
                IsShoting = false;
                return;
            }

            IsShoting = shootingCooldown == 0;

            shootingCooldown++;

            if (shootingCooldown == 80)
                shootingCooldown = 0;
        }
    }
}

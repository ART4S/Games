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

        public bool IsShoting { get; private set; }
        private int shootingCooldown;

        private readonly Timer shootTimer = new Timer();

        public ChaserShip(Point2D position, int radius, int movespeed, PlayerShip playerShip)
            : base(position, radius, movespeed, Properties.Resources.enemy_ship)
        {
            this.playerShip = playerShip;

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

            Position = new Point2D(Position.X - Movespeed, Position.Y);

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

        private void TryDodgePlayerBullets(List<PlayerBullet> playerBullets, Line groundLine)
        {         
            List<Point2D> minPath = FindPathToFreeTrajectory(playerBullets, groundLine);

            if (!minPath.Any())
                return;

            if (minPath.First().Y < Position.Y)
                Position -= new Point2D(0, Movespeed);
            if (minPath.First().Y > Position.Y)
                Position += new Point2D(0, Movespeed);
        }

        private List<Point2D> FindPathToFreeTrajectory(List<PlayerBullet> playerBullets, Line groundLine)
        {
            Point2D bulletsShift = new Point2D(0, 0);

            Queue<Point2D> queue = new Queue<Point2D>();
            Dictionary<Point2D, Point2D> savePaths = new Dictionary<Point2D, Point2D>();

            queue.Enqueue(Position);
            savePaths.Add(Position, Position);

            while (queue.Any())
            {
                Point2D currentPosition = queue.Dequeue();

                List<PlayerBullet> bulletsInFront = playerBullets
                    .Where(x => IsPlayerBulletInFront(currentPosition, x.Position + bulletsShift, x.Radius))
                    .OrderByDescending(x => x.Position.X + bulletsShift.X)
                    .ToList();

                if (!bulletsInFront.Any() || IsPositionOutOfGameFieldLeftBorder(currentPosition))
                    return RecoveryPath(savePaths, currentPosition);

                if (playerBullets.Any(x => IsHaveCollisionWithBullet(currentPosition, x.Position + bulletsShift, x.Radius)) ||
                    IsPositionOutOfGroundLine(currentPosition, groundLine) ||
                    IsPositionOutOfGameFieldTopBorder(currentPosition)
                )
                    break;

                PlayerBullet nearestBullet = bulletsInFront.First();
                Point2D nearestBulletPosition = nearestBullet.Position + bulletsShift;

                double distToTopBorder = Math.Sqrt(2 * Math.Pow(currentPosition.Y + Radius - (nearestBulletPosition.Y - nearestBullet.Radius), 2));
                double distToInterceptionTopBorder = currentPosition.X - Radius - (currentPosition.Y + Radius - (nearestBulletPosition.Y - nearestBullet.Radius)) - (nearestBulletPosition.X + nearestBullet.Radius);

                double distToBottomBorder = Math.Sqrt(2 * Math.Pow(nearestBulletPosition.Y + nearestBullet.Radius - (currentPosition.Y - Radius), 2));
                double distToInterceptionBottomBorder = currentPosition.X - Radius - (nearestBulletPosition.Y + nearestBullet.Radius - (currentPosition.Y - Radius)) - (nearestBulletPosition.X + nearestBullet.Radius);

                if (distToInterceptionTopBorder / nearestBullet.Movespeed > distToTopBorder / Movespeed)
                {
                    Point2D newPosition = currentPosition - new Point2D(Movespeed, Movespeed);

                    savePaths[newPosition] = currentPosition;
                    queue.Enqueue(newPosition);
                }

                if (distToInterceptionBottomBorder / nearestBullet.Movespeed > distToBottomBorder / Movespeed)
                {
                    Point2D newPosition = currentPosition + new Point2D(-Movespeed, Movespeed);

                    savePaths[newPosition] = currentPosition;
                    queue.Enqueue(newPosition);
                }

                bulletsShift = new Point2D(bulletsShift.X + nearestBullet.Movespeed, 0);
            }

            return new List<Point2D>();
        }

        private bool IsHaveCollisionWithBullet(Point2D chaserShipPosition, Point2D bulletPosition, int bulletRadius)
        {
            return Math.Pow(Radius + bulletRadius, 2) >=
                   Math.Pow(chaserShipPosition.X - bulletPosition.X, 2)
                   + Math.Pow(chaserShipPosition.Y - bulletPosition.Y, 2);
        }

        private bool IsPlayerBulletInFront(Point2D chaserShipPosition, Point2D bulletPosition, int bulletRadius)
        {
            int chaserShipTopBorderY = chaserShipPosition.Y - Radius;
            int chaserShipBottomBorderY = chaserShipPosition.Y + Radius;

            int bulletTopBorderY = bulletPosition.Y - bulletRadius;
            int bulletBottomBorderY = bulletPosition.Y + bulletRadius;

            return bulletPosition.X + bulletRadius < chaserShipPosition.X - Radius
                   && Math.Max(bulletTopBorderY, chaserShipTopBorderY) <= Math.Min(bulletBottomBorderY, chaserShipBottomBorderY);
        }

        List<Point2D> RecoveryPath(Dictionary<Point2D, Point2D> savePaths, Point2D endPoint)
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

                path.Add(currentPoint);
            }

            path.Remove(path.Last());
            path.Reverse();

            return path;
        }
    }
}

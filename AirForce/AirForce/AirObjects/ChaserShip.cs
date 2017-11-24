using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AirForce.Enums;

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
            //shootTimer.Tick += (s, e) => ChangeShootingCooldown();
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
            List<Direction> pathToFreeTrajectory = FindPathToFreeTrajectory(playerBullets, groundLine, new List<Direction>(), Position, 0);

            if (!pathToFreeTrajectory.Any())
                return;

            switch (pathToFreeTrajectory.First())
            {
                case Direction.Up:
                    Position -= new Point2D(0, Movespeed);
                    break;
                case Direction.Down:
                    Position += new Point2D(0, Movespeed);
                    break;
            }
        }

        private List<Direction> FindPathToFreeTrajectory(
            List<PlayerBullet> playerBullets,
            Line groundLine,
            List<Direction> path,
            Point2D position,
            int bulletsShiftX)
        {
            Point2D bulletsShift = new Point2D(bulletsShiftX, 0);

            List<PlayerBullet> dangerBullets = playerBullets
                .Where(x => x.Position.X - x.Radius + bulletsShiftX < position.X + Radius)
                .ToList();

            List<PlayerBullet> bulletsInFront = dangerBullets
                .Where(x => IsPlayerBulletInFront(position, x.Position + bulletsShift, x.Radius))
                .OrderByDescending(x => x.Position.X + bulletsShiftX)
                .ToList();

            if (!bulletsInFront.Any() || IsPositionOutOfGameFieldLeftBorder(position))
                return new List<Direction>(path);

            if (dangerBullets.Any(x => IsHaveCollisionWithBullet(position, x.Position + bulletsShift, x.Radius)) ||
                IsPositionOutOfGroundLine(position, groundLine) ||
                IsPositionOutOfGameFieldTopBorder(position)
                )
                return new List<Direction>();

            PlayerBullet nearestBullet = bulletsInFront.First();
            Point2D nearestBulletPosition = nearestBullet.Position + bulletsShift;

            double distToTopBorder = Math.Sqrt(2 * Math.Pow(position.Y + Radius - (nearestBulletPosition.Y - nearestBullet.Radius), 2));
            double distToInterceptionTopBorder = position.X - Radius - (position.Y + Radius - (nearestBulletPosition.Y - nearestBullet.Radius)) - (nearestBulletPosition.X + nearestBullet.Radius);

            double distToBottomBorder = Math.Sqrt(2 * Math.Pow(nearestBulletPosition.Y + nearestBullet.Radius - (position.Y - Radius), 2));
            double distToInterceptionBottomBorder = position.X - Radius - (nearestBulletPosition.Y + nearestBullet.Radius - (position.Y - Radius)) - (nearestBulletPosition.X + nearestBullet.Radius);

            if (distToInterceptionBottomBorder / nearestBullet.Movespeed > distToBottomBorder / Movespeed)
            {
                List<Direction> resultPath = FindPathToFreeTrajectory(
                    dangerBullets,
                    groundLine,
                    new List<Direction>(path) { Direction.Down },
                    position + new Point2D(-Movespeed, Movespeed),
                    bulletsShiftX + nearestBullet.Movespeed
                );

                if (resultPath.Any())
                    return resultPath;
            }

            if (distToInterceptionTopBorder / nearestBullet.Movespeed > distToTopBorder / Movespeed)
            {
                List<Direction> resultPath = FindPathToFreeTrajectory(
                    dangerBullets,
                    groundLine,
                    new List<Direction>(path) { Direction.Up },
                    position - new Point2D(Movespeed, Movespeed),
                    bulletsShiftX + nearestBullet.Movespeed
                );

                if (resultPath.Any())
                    return resultPath;
            }

            return new List<Direction>();
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
    }
}

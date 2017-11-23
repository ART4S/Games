using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AirForce.AirObjects
{
    public sealed class ChaserShip : AirObject
    {
        public delegate void Method(Point2D startPosition);

        public event Method Shooted;
        private readonly PlayerShip playerShip;

        private readonly Timer shootTimer = new Timer();

        public ChaserShip(Point2D position, int radius, int movespeedShift, PlayerShip playerShip)
            : base(position, radius, movespeedShift, Properties.Resources.enemy_ship)
        {
            this.playerShip = playerShip;

            shootTimer.Interval = 800;
            shootTimer.Tick += (s, e) => OnShootEvent();
        }

        public override void Move(Size gameFieldSize, Line groundLine, List<AirObject> airObjects)
        {
            Position = new Point2D(Position.X - MovespeedShift, Position.Y);

            if (IsPositionOutOfGameFieldLeftBorder(Position))
                Durability = 0;

            TryRunShootTimer();
            TryDodgePlayerBullets(playerBullets: airObjects.OfType<PlayerBullet>().ToList(), groundLine: groundLine);
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

        private void TryRunShootTimer()
        {
            if (playerShip.IsInFrontAirObject(this))
            {
                if (shootTimer.Enabled == false)
                    OnShootEvent();

                shootTimer.Start();
            }
            else
                shootTimer.Stop();
        }

        private void TryDodgePlayerBullets(List<PlayerBullet> playerBullets, Line groundLine)
        {
            //if (playerBullets.Find(x => x.IsInFrontAirObject(this)) != null)
            //{
            //    List<PlayerBullet> u = playerBullets.FindAll(b => b.Position.Y < Position.Y);
            //    List<PlayerBullet> d = playerBullets.FindAll(b => b.Position.Y >= Position.Y);

            //    Point2D nextPosition;

            //    if (u.Count > d.Count)
            //    {
            //        nextPosition = new Point2D(Position.X, Position.Y + MovespeedShift);

            //        if (IsPositionAboveGroundLine(nextPosition, groundLine))
            //        {
            //            Position = nextPosition;
            //        }
            //    }
            //    else
            //    {
            //        nextPosition = new Point2D(Position.X, Position.Y - MovespeedShift);

            //        if (IsPositionAboveGroundLine(nextPosition, groundLine))
            //        {
            //            Position = nextPosition;
            //        }
            //    }
            //}
        }

        private void OnShootEvent()
        {
            Point2D startShootPosition = new Point2D(Position.X - Radius, Position.Y);

            Shooted?.Invoke(startShootPosition);
        }
    }
}

using System;
using System.Windows.Forms;
using AirForce.AirObjects.Bullets;

namespace AirForce.AirObjects.EnemyAI
{
    public sealed class ChaserShip : EnemyAI
    {
        private event Action<Point2D> ShootEvent;
        private readonly PlayerShip playerShip;

        private readonly Timer shootTimer = new Timer();

        public ChaserShip(Point2D position, Action<EnemyAI> objectDeathMethod, Action<Point2D> shootMethod, PlayerShip playerShip)
            : base(position, 30, 3, Properties.Resources.enemy_ship, objectDeathMethod)
        {
            this.playerShip = playerShip;
            
            ShootEvent += shootMethod;

            shootTimer.Interval = 500;
            shootTimer.Tick += (s, e) => OnShootEvent();
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip || otherAirObject is PlayerBullet || otherAirObject is Meteor)
            {
                shootTimer.Dispose();
                OnObjectDeathEvent(this);
            }
        }

        public override void Move(Line groundLine)
        {
            if (Position.X + Radius >= 0) // 0
                Position = new Point2D(Position.X - MovespeedShift, Position.Y);
            else
            {
                shootTimer.Dispose();
                OnObjectDeathEvent(this);
            }

            if (playerShip.IsInFrontAirObject(this))
            {
                if (shootTimer.Enabled == false)
                    OnShootEvent();

                shootTimer.Start();
            }
            else
                shootTimer.Stop();
        }

        private void OnShootEvent()
        {
            Point2D startShootPosition = new Point2D(Position.X - Radius, Position.Y);

            ShootEvent?.Invoke(startShootPosition);
        }
    }
}

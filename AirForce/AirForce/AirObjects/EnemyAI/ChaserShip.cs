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

        public ChaserShip(Point2D position, int radius, int movespeedShift, Action<EnemyAI> objectDeathMethod, Action<Point2D> shootMethod, PlayerShip playerShip)
            : base(position, radius, movespeedShift, Properties.Resources.enemy_ship, objectDeathMethod)
        {
            this.playerShip = playerShip;
            
            ShootEvent += shootMethod;

            shootTimer.Interval = 500;
            shootTimer.Tick += (s, e) => OnShootEvent();
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case PlayerShip _:
                case PlayerBullet _:
                case Meteor _:
                    shootTimer.Dispose();
                    OnObjectDeathEvent(this);
                    break;
            }
        }

        public override void Move(Line groundLine)
        {
            if (IsPositionBehindGameFieldLeftBorder())
            {
                shootTimer.Dispose();
                OnObjectDeathEvent(this);
            }
            else
                Position = new Point2D(Position.X - MovespeedShift, Position.Y);

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

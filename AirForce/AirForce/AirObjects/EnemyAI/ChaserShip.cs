using System;
using System.Windows.Forms;

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
            shootTimer.Tick += RunShootEvent;
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            shootTimer.Dispose();
            OnObjectDeathEvent(this);
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

            if (playerShip.IsInFrontAirObject(this) && shootTimer.Enabled == false)
            {
                shootTimer.Start();
                OnShootEvent();
            }

            if (!playerShip.IsInFrontAirObject(this))
                shootTimer.Stop();
        }

        private void OnShootEvent()
        {
            Point2D startShootPosition = new Point2D(Position.X - Radius, Position.Y);

            ShootEvent?.Invoke(startShootPosition);
        }

        public void RunShootEvent(object sender, EventArgs e)
        {
            OnShootEvent();
        }
    }
}

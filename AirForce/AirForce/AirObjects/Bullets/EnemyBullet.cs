using System;
using System.Drawing;
using AirForce.AirObjects.EnemyAI;

namespace AirForce.AirObjects.Bullets
{
    public sealed class EnemyBullet : Bullet
    {
        public EnemyBullet(Point2D position, Action<Bullet> objectDeathMethod) : base(position, Properties.Resources.enemy_bullet, objectDeathMethod)
        {
        }

        public override void Move(Size gameFieldSize)
        {
            if (Position.X + Radius >= 0)
                Position = new Point2D(Position.X - MovespeedShift, Position.Y);
            else
                OnObjectDeathEvent(this);
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip || otherAirObject is Meteor)
                OnObjectDeathEvent(this);
        }
    }
}

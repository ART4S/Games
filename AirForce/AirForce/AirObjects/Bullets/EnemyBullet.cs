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
            bool isPositionBehindLeftGameFiledBorder = Position.X + Radius < 0;

            if (isPositionBehindLeftGameFiledBorder)
                OnObjectDeathEvent(this);
            else
                Position = new Point2D(Position.X - MovespeedShift, Position.Y);
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            if (otherAirObject is PlayerShip || otherAirObject is Meteor)
                OnObjectDeathEvent(this);
        }
    }
}

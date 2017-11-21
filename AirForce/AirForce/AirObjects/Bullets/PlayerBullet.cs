using System;
using System.Drawing;
using AirForce.AirObjects.EnemyAI;

namespace AirForce.AirObjects.Bullets
{
    public sealed class PlayerBullet : Bullet
    {
        public PlayerBullet(Point2D position, Action<Bullet> objectDeathMethod) : base(position, Properties.Resources.player_bullet, objectDeathMethod)
        {
        }

        public override void Move(Size gameFieldSize)
        {
            if (Position.X - Radius <= gameFieldSize.Width)
                Position = new Point2D(Position.X + MovespeedShift, Position.Y);
            else
                OnObjectDeathEvent(this);
        }

        public override void CollisionWithOtherAirObject(AirObject otherAirObject)
        {
            switch (otherAirObject)
            {
                case BigShip _:
                case ChaserShip _:
                case Meteor _:
                    OnObjectDeathEvent(this);
                    break;
            } 
        }
    }
}

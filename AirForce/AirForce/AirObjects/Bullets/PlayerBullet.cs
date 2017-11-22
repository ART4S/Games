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
            bool isPositionBehindRightGameFieldBorder = Position.X - Radius > gameFieldSize.Width;

            if (isPositionBehindRightGameFieldBorder)
                OnObjectDeathEvent(this);
            else
                Position = new Point2D(Position.X + MovespeedShift, Position.Y);
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

        public bool IsInFrontAirObject(AirObject airObject)
        {
            int playerBulletTopBorderY = Position.Y - Radius;
            int playerBulletBottomBorderY = Position.Y + Radius;

            int airObjectTopBorderY = airObject.Position.Y - airObject.Radius;
            int airObjectBottomBorderY = airObject.Position.Y + airObject.Radius;

            return Position.X < airObject.Position.X
                   && Math.Max(airObjectTopBorderY, playerBulletTopBorderY) < Math.Min(airObjectBottomBorderY, playerBulletBottomBorderY);
        }
    }
}

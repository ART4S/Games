using System.Collections.Generic;

namespace AirForce
{
    public class PlayerBulletMover : IMover
    {
        private readonly FlyingObject playerBullet;

        public PlayerBulletMover(FlyingObject playerBullet)
        {
            this.playerBullet = playerBullet;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects = null)
        {
            playerBullet.Position += new Point2D(playerBullet.Movespeed, 0);
        }
    }
}
using System.Collections.Generic;

namespace AirForce
{
    public class EnemyBulletMover: IMover
    {
        private readonly FlyingObject enemyBullet;

        public EnemyBulletMover(FlyingObject enemyBullet)
        {
            this.enemyBullet = enemyBullet;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects = null)
        {
            enemyBullet.Position -= new Point2D(enemyBullet.Movespeed, 0);
        }
    }
}
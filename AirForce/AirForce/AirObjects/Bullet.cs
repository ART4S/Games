using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirForce.AirObjects
{
    public abstract class Bullet : AirObject
    {
        protected Bullet(Point positionInSpace, int radius, int strength, int movespeedShift, Image image) : base(positionInSpace, radius, strength, movespeedShift, image)
        {
        }

        public void Move()
        {
            // ...
        }
    }

    public sealed class EnemyBullet
    {
        
    }

    public sealed class PlayerBullet
    {
        
    }
}

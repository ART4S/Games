using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class EnemyAirObject : AirObject
    {
        protected EnemyAirObject(Point positionInSpace, int radius, int strength, int movespeedShift, Image image, Action<AirObject> deathObjectMethod) : base(positionInSpace, radius, strength, movespeedShift, image, deathObjectMethod)
        {
        }

        public abstract void Move(Line groundLine);
    }
}

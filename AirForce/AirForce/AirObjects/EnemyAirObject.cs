using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public abstract class EnemyAirObject : AirObject
    {
        private event Action<EnemyAirObject> DeathThisObjectEvent;

        protected EnemyAirObject(Point positionInSpace, int radius, int strength, int movespeedShift, Image image, Action<EnemyAirObject> deathObjectMethod) : base(positionInSpace, radius, strength, movespeedShift, image)
        {
            DeathThisObjectEvent += deathObjectMethod;
        }

        public abstract void Move(Line groundLine);

        protected virtual void OnDeathThisObjectEvent(EnemyAirObject obj)
        {
            DeathThisObjectEvent?.Invoke(obj);
        }
    }
}

using System;
using System.Drawing;

namespace AirForce.AirObjects.EnemyAI
{
    public abstract class EnemyAI : AirObject
    {
        private event Action<EnemyAI> ObjectDeathEvent;

        protected EnemyAI(Point2D position, int radius, int movespeedShift, Image image, Action<EnemyAI> objectDeathMethod) : base(position, radius, movespeedShift, image)
        {
            ObjectDeathEvent += objectDeathMethod;
        }

        public abstract void Move(Line groundLine);

        protected void OnObjectDeathEvent(EnemyAI deathObject)
        {
            ObjectDeathEvent?.Invoke(deathObject);
        }
    }
}

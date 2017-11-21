using System;
using System.Drawing;

namespace AirForce.AirObjects.Bullets
{
    public abstract class Bullet : AirObject
    {
        private event Action<Bullet> ObjectDeathEvent;

        protected Bullet(Point2D position, Image image, Action<Bullet> objectDeathMethod)
            : base(position, 10, 10, image)
        {
            ObjectDeathEvent += objectDeathMethod;
        }

        protected void OnObjectDeathEvent(Bullet deathObject)
        {
            ObjectDeathEvent?.Invoke(deathObject);
        }

        public abstract void Move(Size gameFieldSize);
    }
}

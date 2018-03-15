using System.Collections.Generic;

namespace AirForce
{
    public class MeteorMover : IMover
    {
        private readonly FlyingObject meteor;

        public MeteorMover(FlyingObject meteor)
        {
            this.meteor = meteor;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects = null)
        {
            meteor.Position += new Point2D(-2 * meteor.Movespeed, meteor.Movespeed);
        }
    }
}
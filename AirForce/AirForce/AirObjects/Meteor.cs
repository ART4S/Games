using System;
using System.Drawing;

namespace AirForce.AirObjects
{
    public partial class PlayerShip
    {
        public class Meteor : AirObject
        {
            public Meteor(Point positionInSpace, int collisionRadius) : base(positionInSpace, collisionRadius)
            {
            }

            public override void BumpWithOtherAirObject(AirObject otherAirObject)
            {
                throw new NotImplementedException();
            }
        }
    }
}

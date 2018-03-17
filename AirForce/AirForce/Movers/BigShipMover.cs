using System.Collections.Generic;

namespace AirForce
{
    public class BigShipMover : IMover
    {
        private readonly FlyingObject bigShip;

        public BigShipMover(FlyingObject bigShip)
        {
            this.bigShip = bigShip;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects = null)
        {
            bigShip.Position -= new Point2D(bigShip.Movespeed, 0);
        }

        public void UndoMove()
        {
            throw new System.NotImplementedException();
        }
    }
}
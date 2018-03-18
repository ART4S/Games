
namespace AirForce
{
    public class ManualMover : IManualMover
    {
        private readonly FlyingObject playerShip;

        public ManualMover(FlyingObject playerShip)
        {
            this.playerShip = playerShip;
        }

        public void MoveManually(Point2D movespeedModifer, Field gameField, Ground ground)
        {
            Point2D nextPosition = playerShip.Position + new Point2D(
                                       x: playerShip.Movespeed * movespeedModifer.X,
                                       y: playerShip.Movespeed * movespeedModifer.Y);

            if (CollisionHandler.IsEntirelyOnField(nextPosition, playerShip.Radius, gameField))
                playerShip.Position = nextPosition;
        }
    }
}
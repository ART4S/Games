namespace AirForce.AirObjects
{
    public class PlayerAir : AirObject
    {
        public override int Strength { get; }
        public override int CollisionRadius { get; }

        public PlayerAir()
        {
            Strength = 5;
            CollisionRadius = 5;
        }

        public void Move(Direction movingDirection)
        {

        }

        public override void BumpWithOtherAirObject()
        {
            
        }
    }
}

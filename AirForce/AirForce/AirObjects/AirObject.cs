namespace AirForce.AirObjects
{
    public abstract class AirObject
    {
        public abstract int Strength { get; }
        public abstract int CollisionRadius { get; }

        public abstract void BumpWithOtherAirObject();
    }
}

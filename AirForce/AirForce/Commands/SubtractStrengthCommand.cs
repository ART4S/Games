namespace AirForce
{
    public class SubtractStrengthCommand : ICommand
    {
        private readonly FlyingObject source;
        private readonly int deltaStrength;

        public SubtractStrengthCommand(FlyingObject source, int deltaStrength)
        {
            this.source = source;
            this.deltaStrength = deltaStrength;
        }

        public void Execute()
        {
            source.Strength -= deltaStrength;
        }

        public void Undo()
        {
            source.Strength += deltaStrength;
        }
    }
}
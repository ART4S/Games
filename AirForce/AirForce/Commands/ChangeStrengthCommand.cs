using System;

namespace AirForce
{
    public class ChangeStrengthCommand : IUndoCommand
    {
        private readonly FlyingObject source;
        private int deltaStrength;

        public ChangeStrengthCommand(FlyingObject source)
        {
            this.source = source;
        }

        public void AddStrength(int strength)
        {
            deltaStrength = strength;
            source.Strength += deltaStrength;
        }

        public void SetStrength(int strength)
        {
            deltaStrength = Math.Abs(source.Strength - strength);

            if (strength < source.Strength)
                deltaStrength = -deltaStrength;

            source.Strength += deltaStrength;
        }

        public void Undo()
        {
            source.Strength -= deltaStrength;
        }
    }
}
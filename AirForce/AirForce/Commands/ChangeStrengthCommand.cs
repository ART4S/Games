using System;

namespace AirForce
{
    public class ChangeStrengthCommand : IUndoCommand
    {
        private readonly FlyingObject flyingObject;
        private int strengthShift;

        public ChangeStrengthCommand(FlyingObject flyingObject)
        {
            this.flyingObject = flyingObject;
        }

        public void AddStrength(int strength)
        {
            strengthShift = strength;
            flyingObject.Strength += strength;
        }

        public void SetStrength(int strength)
        {
            strengthShift = Math.Abs(flyingObject.Strength - strength);

            if (strength < flyingObject.Strength)
                strengthShift = -strengthShift;

            flyingObject.Strength = strength;
        }

        public void Undo()
        {
            flyingObject.Strength -= strengthShift;
        }
    }
}
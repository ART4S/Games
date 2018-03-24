using System;

namespace AirForce
{
    public class ChangeCoooldownCommand : IUndoCommand
    {
        private readonly Coooldown coooldown;
        private int deltaValue;

        public ChangeCoooldownCommand(Coooldown coooldown)
        {
            this.coooldown = coooldown;
        }

        public void IncreaseValue()
        {
            deltaValue = 1;
            coooldown.CurrentValue += deltaValue;
        }

        public void SetValue(int value)
        {
            deltaValue = Math.Abs(coooldown.CurrentValue - value);

            if (coooldown.CurrentValue > value)
                deltaValue = -deltaValue;

            coooldown.CurrentValue += deltaValue;
        }

        public void Undo()
        {
            coooldown.CurrentValue -= deltaValue;
        }
    }
}
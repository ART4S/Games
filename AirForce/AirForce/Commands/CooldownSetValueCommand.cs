using System;

namespace AirForce
{
    public class CooldownSetValueCommand : ICommand
    {
        private readonly Coooldown coooldown;
        private readonly int value;
        private int deltaValue;

        public CooldownSetValueCommand(Coooldown coooldown, int value)
        {
            this.coooldown = coooldown;
            this.value = value;
        }

        public void Execute()
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
namespace AirForce
{
    public class CooldownIncreaseValueCommand : ICommand
    {
        private readonly Coooldown coooldown;
        private readonly int deltaValue = 1;

        public CooldownIncreaseValueCommand(Coooldown coooldown)
        {
            this.coooldown = coooldown;
        }

        public void Execute()
        {
            coooldown.CurrentValue += deltaValue;
        }

        public void Undo()
        {
            coooldown.CurrentValue -= deltaValue;
        }
    }
}
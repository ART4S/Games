namespace AirForce
{
    public class Coooldown
    {
        public int MaxValue { get; }
        public int CurrentValue { get; set; }
        public bool IsCollapsed { get; private set; }

        public Coooldown(int maxValue, bool isCollapsed)
        {
            MaxValue = maxValue;
            IsCollapsed = isCollapsed;

            if (isCollapsed)
                CurrentValue = MaxValue - 1;
        }

        public void Tick(RewindMacroCommand rewindMacroCommand)
        {
            rewindMacroCommand.AddAndExecute(new CooldownIncreaseValueCommand(this));

            if (CurrentValue > MaxValue)
                rewindMacroCommand.AddAndExecute(new CooldownSetValueCommand(this, 0));

            IsCollapsed = CurrentValue == MaxValue;
        }

        public void SetOneTickToCollapse(RewindMacroCommand rewindMacroCommand)
        {
            IsCollapsed = false;

            rewindMacroCommand.AddAndExecute(new CooldownSetValueCommand(this, MaxValue - 1));
        }
    }
}
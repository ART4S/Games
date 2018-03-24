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
        }

        public void Tick(RewindMacroCommand rewindMacroCommand = null)
        {
            var increaseCoooldownCommand = new ChangeCoooldownCommand(this);
            increaseCoooldownCommand.IncreaseValue();
            rewindMacroCommand?.AddCommand(increaseCoooldownCommand);

            if (CurrentValue > MaxValue)
            {
                var setValueCoooldownCommand = new ChangeCoooldownCommand(this);
                setValueCoooldownCommand.SetValue(0);
                rewindMacroCommand?.AddCommand(setValueCoooldownCommand);
            }

            IsCollapsed = CurrentValue == MaxValue;
        }

        public void SetOneTickToCollapse(RewindMacroCommand rewindMacroCommand = null)
        {
            IsCollapsed = false;

            var setValueCoooldownCommand = new ChangeCoooldownCommand(this);
            setValueCoooldownCommand.SetValue(MaxValue - 1);
            rewindMacroCommand?.AddCommand(setValueCoooldownCommand);
        }
    }
}
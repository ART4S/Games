namespace AirForce
{
    public class Coooldown
    {
        public int MaxValue { get; }
        public int CurrentValue { get; set; }
        public bool IsElapsed { get; private set; }

        public Coooldown(int maxValue, bool isElapsed)
        {
            MaxValue = maxValue;
            IsElapsed = isElapsed;

            if (isElapsed)
                CurrentValue = MaxValue - 1;
        }

        public void Tick(RewindMacroCommand rewindMacroCommand)
        {
            rewindMacroCommand.AddAndExecute(new CooldownIncreaseValueCommand(this));

            if (CurrentValue > MaxValue)
                rewindMacroCommand.AddAndExecute(new CooldownSetValueCommand(this, 0));

            IsElapsed = CurrentValue == MaxValue;
        }

        public void SetOneTickToElapse(RewindMacroCommand rewindMacroCommand)
        {
            IsElapsed = false;

            rewindMacroCommand.AddAndExecute(new CooldownSetValueCommand(this, MaxValue - 1));
        }
    }
}
namespace AirForce
{
    public class Coooldown
    {
        private readonly int maxValue;
        private int currentValue;

        public Coooldown(int currentValue, int maxValue)
        {
            this.maxValue = maxValue;
            this.currentValue = currentValue;
        }

        public bool Tick()
        {
            currentValue++;

            if (currentValue > maxValue)
            {
                currentValue = 0;
                return true;
            }

            return false;
        }

        public void SetOnTick()
        {
            currentValue = maxValue;
        }
    }
}
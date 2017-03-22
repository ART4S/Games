namespace Paint
{
    public enum PenSize
    {
        Little = 4, 
        Average = 8,
        Big = 12
    }

    public static class PenSizeExtensions
    {
        public static float ToFloat(this PenSize penSize)
        {
            return (float) penSize;
        }

        public static float ToInt(this PenSize penSize)
        {
            return (int) penSize;
        }
    }
}
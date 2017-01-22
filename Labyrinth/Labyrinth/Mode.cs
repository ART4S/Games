using System;

namespace Labyrinth
{
    public enum Mode
    {
        EazyCrazy,
        Eazy,
        Normal,
        Hard
    }

    public static class ModeExtensions
    {
        public static string ToRussianString(this Mode mode)
        {
            switch (mode)
            {
                case Mode.EazyCrazy:
                    return "Очень легко";

                case Mode.Eazy:
                    return "Легко";

                case Mode.Normal:
                    return "Нормально";

                case Mode.Hard:
                    return "Сложно";

                default:
                    throw new ArgumentOutOfRangeException(nameof(mode), mode, null);
            }
        }
    }
}

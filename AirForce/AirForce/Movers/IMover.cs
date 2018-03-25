using System.Collections.Generic;

namespace AirForce
{
    public interface IMover
    {
        void Move(Rectangle2D field, Rectangle2D ground, List<FlyingObject> objectsOnField, RewindMacroCommand rewindMacroCommand);
    }
}
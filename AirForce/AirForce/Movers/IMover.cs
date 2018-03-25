using System.Collections.Generic;

namespace AirForce
{
    public interface IMover
    {
        void Move(Field field, Ground ground, List<FlyingObject> objectsOnField, RewindMacroCommand rewindMacroCommand);
    }
}
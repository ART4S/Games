using System.Collections.Generic;

namespace AirForce
{
    public interface IMover
    {
        ChangePositionCommand Move(Field gameField, Ground ground, List<FlyingObject> objectsOnField);
    }
}
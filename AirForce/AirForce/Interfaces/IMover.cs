using System.Collections.Generic;

namespace AirForce
{
    public interface IMover
    {
        void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects);
    }
}
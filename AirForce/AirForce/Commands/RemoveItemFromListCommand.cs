using System.Collections.Generic;

namespace AirForce.Commands
{
    class RemoveItemFromListCommand : ICommand
    {
        private readonly FlyingObject item;
        private readonly List<FlyingObject> list;

        public RemoveItemFromListCommand(FlyingObject item, List<FlyingObject> list)
        {
            this.item = item;
            this.list = list;
        }

        public void Execute()
        {
            list.Remove(item);
        }

        public void Undo()
        {
            list.Add(item);
        }
    }
}

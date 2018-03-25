using System.Collections.Generic;

namespace AirForce.Commands
{
    class AddItemToListCommand : ICommand
    {
        private readonly FlyingObject item;
        private readonly List<FlyingObject> list;

        public AddItemToListCommand(FlyingObject item, List<FlyingObject> list)
        {
            this.item = item;
            this.list = list;
        }

        public void Execute()
        {
            list.Add(item);
        }

        public void Undo()
        {
            list.Remove(item);
        }
    }
}

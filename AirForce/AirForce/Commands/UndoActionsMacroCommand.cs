using System.Collections.Generic;

namespace AirForce
{
    public class UndoActionsMacroCommand
    {
        private readonly List<IUndoCommand> commands = new List<IUndoCommand>();

        public void AddCommand(IUndoCommand command)
        {
            commands.Add(command);
        }

        public void UndoActions()
        {
            foreach (IUndoCommand command in commands)
                command.Undo();
        }
    }
}
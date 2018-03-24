using System.Collections.Generic;

namespace AirForce
{
    public class RewindMacroCommand
    {
        private readonly List<IUndoCommand> commands = new List<IUndoCommand>();

        public void AddCommand(IUndoCommand command)
        {
            commands.Add(command);
        }

        public void Undo()
        {
            foreach (IUndoCommand command in commands)
                command.Undo();
        }
    }
}
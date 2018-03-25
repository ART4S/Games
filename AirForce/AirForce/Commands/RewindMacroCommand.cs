using System.Collections.Generic;

namespace AirForce
{
    public class RewindMacroCommand
    {
        private readonly List<ICommand> commands = new List<ICommand>();

        public void Undo()
        {
            foreach (ICommand command in commands)
                command.Undo();
        }

        public void AddAndExecute(ICommand command)
        {
            command.Execute();
            commands.Add(command);
        }
    }
}
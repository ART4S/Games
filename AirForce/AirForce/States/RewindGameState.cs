using System.Linq;

namespace AirForce
{
    public class RewindGameState : IGameState
    {
        private readonly Game game;

        public RewindGameState(Game game)
        {
            this.game = game;
        }

        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }

        public void Update()
        {
            if (game.RewindMacroCommands.Any())
                UndoLastMacroCommand();
        }

        private void UndoLastMacroCommand()
        {
            RewindMacroCommand rewindMacroCommand = game.RewindMacroCommands.Last();
            rewindMacroCommand.Undo();
            game.RewindMacroCommands.Remove(rewindMacroCommand);
        }

        public void EndRewind()
        {
            game.State = new PlayingGameState(game);
        }
    }
}
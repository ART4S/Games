namespace AirForce
{
    class RemoveObjectFromGameCommand : ICommand
    {
        private readonly FlyingObject source;
        private readonly Game game;

        public RemoveObjectFromGameCommand(FlyingObject source, Game game)
        {
            this.source = source;
            this.game = game;
        }

        public void Execute()
        {
            game.ObjectsOnField.Remove(source);
        }

        public void Undo()
        {
            game.ObjectsOnField.Add(source);
        }
    }
}

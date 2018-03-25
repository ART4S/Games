namespace AirForce
{
    class AddObjectToGameCommand : ICommand
    {
        private readonly FlyingObject source;
        private readonly Game game;

        public AddObjectToGameCommand(FlyingObject source, Game game)
        {
            this.source = source;
            this.game = game;
        }

        public void Execute()
        {
            game.ObjectsOnField.Add(source);
        }

        public void Undo()
        {
            game.ObjectsOnField.Remove(source);
        }
    }
}
using System.Collections.Generic;
using System.Drawing;

namespace AirForce
{
    public class WaitingGameState : IGameState
    {
        #region Fields

        private readonly GameController game;

        private FlyingObject Player
        {
            set => game.Player = value;
        }

        private Ground Ground
        {
            get => game.Ground;
        }

        private Field GameField
        {
            get => game.GameField;
        }

        private List<FlyingObject> Objects
        {
            get => game.FlyingObjects;
        }

        private FlyingObjectsFactory Factory
        {
            get => game.FlyingObjectsFactory;
        }

        private IGameState GameState
        {
            set => game.GameState = value;
        }

        #endregion

        public WaitingGameState(GameController game)
        {
            this.game = game;
        }

        public void Restart()
        {
            Objects.Clear();
            Player = Factory.GetPlayerShip(GameField, Ground);
            GameState = new PlayingGameState(game);
        }

        public void Update() { }

        public void MovePlayer(Point2D movespeedModifer) { }

        public void PlayerFire() { }
    }
}
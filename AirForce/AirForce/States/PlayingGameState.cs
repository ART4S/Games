using System.Collections.Generic;
using System.Drawing;

namespace AirForce
{
    public class PlayingGameState : IGameState
    {
        #region Fields

        private readonly GameController game;

        private FlyingObject Player
        {
            get => game.Player;
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

        public PlayingGameState(GameController game)
        {
            this.game = game;
        }

        public void MovePlayer(Point2D movespeedModifer)
        {
            Player.MoveManyally(movespeedModifer, GameField, Ground);
        }

        public void Update()
        {
            if (Player.Strength <= 0)
                GameState = new WaitingGameState(game);
        }

        public void PlayerFire()
        {
            Objects.Add(Factory.GetPlayerBullet(GameField, Ground, Player));
        }

        public void Restart() { }
    }
}
using System.Collections.Generic;
using System.Linq;

namespace AirForce
{
    public class RewindGameState : IGameState
    {
        #region Fields

        private readonly GameController game;

        private List<FlyingObject> Objects
        {
            set => game.FlyingObjects = value;
        }

        private Stack<List<FlyingObject>> OldObjects
        {
            get => game.OldFlyingObjects;
        }

        private IGameState GameState
        {
            set => game.GameState = value;
        }

        #endregion

        public void Restart() { }
        public void AddNewRandomEnemy() { }
        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }

        public RewindGameState(GameController game)
        {
            this.game = game;
        }

        public void Update()
        {
                
        }

        public void EndRewind()
        {
            GameState = new PlayingGameState(game);
        }
    }
}
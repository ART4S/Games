using System.Collections.Generic;

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

        private CollisionHandler CollisionHandler
        {
            get => game.CollisionHandler;
        }

        private IGameState GameState
        {
            set => game.GameState = value;
        }

        private Stack<List<FlyingObject>> OldObjects
        {
            get => game.OldFlyingObjects;
        }

        #endregion

        public WaitingGameState(GameController game)
        {
            this.game = game;
        }

        public void MovePlayer(Point2D movespeedModifer) { }
        public void PlayerFire() { }
        public void BeginRewind() { }
        public void EndRewind() { }

        public void Restart()
        {
            OldObjects.Clear();
            Objects.Clear();
            Player = Factory.GetPlayerShip(GameField, Ground);
            GameState = new PlayingGameState(game);
        }

        public void Update()
        {
            Objects.AddRange(CollisionHandler.GetNewEnemyBullets());

            foreach (FlyingObject obj in Objects)
                obj.Move(GameField, Ground, Objects);

            CollisionHandler.FindCollisionsAndChangeStrengths();
            Objects.RemoveAll(f => f.Strength <= 0);
        }

        public void AddNewRandomEnemy()
        {
            Objects.Add(Factory.GetRandomEnemy(GameField, Ground));
        }
    }
}
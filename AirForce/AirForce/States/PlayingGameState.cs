using System.Collections.Generic;

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

        private CollisionHandler CollisionHandler
        {
            get => game.CollisionHandler;
        }

        private Stack<List<FlyingObject>> OldObjects
        {
            get => game.OldFlyingObjects;
        }

        #endregion

        public PlayingGameState(GameController game)
        {
            this.game = game;
        }

        public void EndRewind() { }
        public void Restart() { }

        public void MovePlayer(Point2D movespeedModifer)
        {
            Player.MoveManyally(movespeedModifer, GameField, Ground);
        }

        public void Update()
        {
            Objects.AddRange(CollisionHandler.GetNewEnemyBullets());

            foreach (FlyingObject obj in Objects)
                obj.Move(GameField, Ground, Objects);

            CollisionHandler.FindCollisionsAndChangeStrengths();

            Objects.RemoveAll(f => f.Strength <= 0);

            OldObjects.Push(new List<FlyingObject>(Objects));

            if (Player.Strength <= 0)
                GameState = new WaitingGameState(game);
        }

        public void PlayerFire()
        {
            Objects.Add(Factory.GetPlayerBullet(GameField, Ground, Player));
        }

        public void AddNewRandomEnemy()
        {
            Objects.Add(Factory.GetRandomEnemy(GameField, Ground));
        }

        public void BeginRewind()
        {
            GameState = new RewindGameState(game);
        }
    }
}
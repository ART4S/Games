using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace AirForce
{
    public partial class Game
    {
        #region Fields

        public Ground Ground { get; }
        public Field GameField { get; }
        public CollisionHandler CollisionHandler { get; }
        public Dictionary<FlyingObjectType, FlyingObjectType[]> CollisionTable { get; }

        public List<FlyingObject> FlyingObjects { get; } = new List<FlyingObject>();
        public FlyingObjectsFactory FlyingObjectsFactory { get; } = new FlyingObjectsFactory();

        public Stack<List<FlyingObject>> DeadObjects { get; } = new Stack<List<FlyingObject>>();
        public Stack<List<FlyingObject>> ObjectsForReleaseOnField { get; } = new Stack<List<FlyingObject>>();

        public FlyingObject Player
        {
            get
            {
                if (FlyingObjects.Count == 0 || FlyingObjects.First().Type != FlyingObjectType.PlayerShip)
                    FlyingObjects.Insert(0, FlyingObjectsFactory.CreateDeadPlayer(GameField, Ground));

                return FlyingObjects.First();
            }
            set
            {
                if (FlyingObjects.Any())
                    FlyingObjects[0] = value;
                else
                    FlyingObjects.Add(value);
            }
        }

        public IGameState GameState { get; set; }

        #endregion

        public Game(Size gameFieldSize)
        {
            CollisionTable = new Dictionary<FlyingObjectType, FlyingObjectType[]>
            {
                { FlyingObjectType.PlayerShip, new []{ FlyingObjectType.Meteor, FlyingObjectType.BigShip, FlyingObjectType.ChaserShip, FlyingObjectType.EnemyBullet, FlyingObjectType.FlyingSaucer } },
                { FlyingObjectType.BigShip, new[]{ FlyingObjectType.Meteor, FlyingObjectType.PlayerShip, FlyingObjectType.PlayerBullet } },
                { FlyingObjectType.ChaserShip, new []{ FlyingObjectType.Meteor, FlyingObjectType.PlayerShip, FlyingObjectType.PlayerBullet } },
                { FlyingObjectType.FlyingSaucer, new []{ FlyingObjectType.PlayerShip } },
                { FlyingObjectType.Meteor, new []{ FlyingObjectType.PlayerShip, FlyingObjectType.BigShip, FlyingObjectType.ChaserShip, FlyingObjectType.EnemyBullet, FlyingObjectType.PlayerBullet } },
                { FlyingObjectType.PlayerBullet, new []{ FlyingObjectType.BigShip, FlyingObjectType.ChaserShip, FlyingObjectType.Meteor } },
                { FlyingObjectType.EnemyBullet, new []{ FlyingObjectType.Meteor, FlyingObjectType.Meteor } }
            };

            CollisionHandler = new CollisionHandler(this);
            GameState = new PlayingGameState(this);
            GameField = new Field(new Point2D(), gameFieldSize);
            Ground = new Ground(new Point2D(0, gameFieldSize.Height - 30), gameFieldSize);
            Player = FlyingObjectsFactory.CreatePlayerShip(GameField, Ground);
        }

        public void AddNewRandomEnemy()
        {
            GameState.AddNewRandomEnemy();
        }

        public void Update()
        {
            GameState.Update();
        }

        public void Restart()
        {
            GameState.Restart();
        }

        public void MovePlayer(Point2D movespeedModifer)
        {
            GameState.MovePlayer(movespeedModifer);
        }

        public void PlayerFire()
        {
            GameState.PlayerFire();
        }

        public void BeginRewind()
        {
            GameState.BeginRewind();
        }

        public void EndRewind()
        {
            GameState.EndRewind();
        }
    }
}





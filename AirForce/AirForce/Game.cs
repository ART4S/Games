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

        public List<FlyingObject> ObjectsOnField { get; } = new List<FlyingObject>();
        public FlyingObjectsFactory FlyingObjectsFactory { get; } = new FlyingObjectsFactory();

        public List<FlyingObject> DeadObjects { get; } = new List<FlyingObject>();
        public List<List<FlyingObject>> ObjectsForReleaseOnField { get; } = new List<List<FlyingObject>>();

        public List<UndoActionsMacroCommand> UndoActionsMacroCommands { get; } = new List<UndoActionsMacroCommand>();

        public Coooldown EnemiesCreatingCooldown { get; } = new Coooldown(currentValue: 80, maxValue: 80);

        public FlyingObject Player
        {
            get
            {
                if (ObjectsOnField.Count == 0 || ObjectsOnField.First().Type != FlyingObjectType.PlayerShip)
                    ObjectsOnField.Insert(0, FlyingObjectsFactory.CreateDeadPlayer(GameField, Ground));

                return ObjectsOnField.First();
            }
            set
            {
                if (ObjectsOnField.Any())
                    ObjectsOnField[0] = value;
                else
                    ObjectsOnField.Add(value);
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

            GameField = new Field(
                position: new Point2D(),
                size: gameFieldSize);

            Ground = new Ground(
                position: new Point2D(0, gameFieldSize.Height - 30),
                size: gameFieldSize);

            Player = FlyingObjectsFactory.CreatePlayerShip(GameField, Ground);
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





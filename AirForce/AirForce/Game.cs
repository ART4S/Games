using System.Linq;
using System.Drawing;
using System.Collections.Generic;

namespace AirForce
{
    public partial class Game
    {
        #region Fields

        public Ground Ground { get; }
        public Field Field { get; }
        public CollisionHandler CollisionHandler { get; }
        public Dictionary<FlyingObjectType, FlyingObjectType[]> CollisionTable { get; }

        public List<FlyingObject> ObjectsOnField { get; } = new List<FlyingObject>();
        public List<FlyingObject> DeadObjects { get; } = new List<FlyingObject>();
        public List<List<FlyingObject>> ObjectsPendingReleaseOnField { get; } = new List<List<FlyingObject>>();

        public FlyingObject Player
        {
            get
            {
                if (ObjectsOnField.Count == 0 || ObjectsOnField.First().Type != FlyingObjectType.PlayerShip)
                    ObjectsOnField.Insert(0, FlyingObjectsFactory.CreateDeadPlayer(Field, Ground));

                return ObjectsOnField.First();
            }
            set
            {
                if (ObjectsOnField.Any())
                    ObjectsOnField.Insert(0, value);
                else
                    ObjectsOnField.Add(value);
            }
        }

        public FlyingObjectsFactory FlyingObjectsFactory { get; } = new FlyingObjectsFactory();

        public List<RewindMacroCommand> RewindMacroCommands { get; } = new List<RewindMacroCommand>();

        public Coooldown EnemiesCreatingCooldown { get; } = new Coooldown(maxValue: 80, isCollapsed: true);

        public IGameState State { get; set; }

        #endregion

        public Game(Size fieldSize)
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
            State = new PlayingGameState(this);

            Field = new Field(
                position: new Point2D(),
                size: fieldSize);

            Ground = new Ground(
                position: new Point2D(0, Field.Size.Height - 30),
                size: Field.Size);

            Player = FlyingObjectsFactory.CreatePlayerShip(Field, Ground);
        }

        public void Update(Point2D playerMovespeedModifer)
        {
            State.Update(playerMovespeedModifer);
        }

        public void Restart()
        {
            State.Restart();
        }

        public void PlayerFire()
        {
            State.PlayerFire();
        }

        public void BeginRewind()
        {
            State.BeginRewind();
        }

        public void EndRewind()
        {
            State.EndRewind();
        }
    }
}





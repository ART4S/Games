using System.Collections.Generic;

namespace AirForce
{
    public partial class Game
    {
        #region Fields

        public Rectangle2D Ground { get; }
        public Rectangle2D Field { get; }
        public CollisionHandler CollisionHandler { get; }
        public Dictionary<FlyingObjectType, FlyingObjectType[]> CollisionTable { get; }

        public List<FlyingObject> ObjectsOnField { get; } = new List<FlyingObject>();
        public FlyingObjectsFactory FlyingObjectsFactory { get; } = new FlyingObjectsFactory();
        public List<RewindMacroCommand> RewindMacroCommands { get; } = new List<RewindMacroCommand>();

        public FlyingObject Player
        {
            get { return ObjectsOnField.Find(o => o.Type == FlyingObjectType.PlayerShip) ?? FlyingObjectsFactory.CreateDeadPlayer(Field, Ground); }
            set
            {
                if (!ObjectsOnField.Exists(o => o.Type == FlyingObjectType.PlayerShip))
                    ObjectsOnField.Add(value);
            }
        }

        public Coooldown EnemiesCreatingCooldown { get; } = new Coooldown(maxValue: 80, isElapsed: true);

        public IGameState State { get; set; }

        public const int MinSpeed = 1;
        public const int MaxSpeed = 8;
        public int Speed { get; set; } = MinSpeed;

        #endregion

        public Game(Size2D fieldSize)
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

            Field = new Rectangle2D(
                location: new Point2D(),
                size: fieldSize);

            Ground = new Rectangle2D(
                location: new Point2D(0, Field.Size.Height - 30),
                size: Field.Size);

            Player = FlyingObjectsFactory.CreatePlayerShip(Field, Ground);
        }

        public void Update()
        {
            State.Update();
        }

        public void PlayerFire()
        {
            State.PlayerFire();
        }

        public void MovePlayer(Point2D movespeedModifer)
        {
            State.MovePlayer(movespeedModifer);
        }

        public void BeginRewind()
        {
            State.BeginRewind();
        }

        public void EndRewind()
        {
            State.EndRewind();
        }

        public bool IsOver()
        {
            return Player.Strength <= 0;
        }

        public void IncreaseSpeed()
        {
            State.IncreaseSpeed();
        }

        public void DecreaseSpeed()
        {
            State.DecreaseSpeed();
        }
    }
}





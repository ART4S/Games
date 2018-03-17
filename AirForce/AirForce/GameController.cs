using System;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace AirForce
{
    public class GameController
    {
        #region Fields

        public Ground Ground { get; }

        public Field GameField { get; }

        public List<FlyingObject> FlyingObjects { get; set; } = new List<FlyingObject>();

        public FlyingObjectsFactory FlyingObjectsFactory { get; } = new FlyingObjectsFactory();

        public FlyingObject Player
        {
            get
            {
                if (FlyingObjects.Count == 0 || FlyingObjects.First().Type != FlyingObjectType.PlayerShip)
                    FlyingObjects.Insert(0, FlyingObjectsFactory.GetDeadPlayer(GameField, Ground));

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

        public Dictionary<FlyingObjectType, FlyingObjectType[]> CollisionTable { get; }

        public IGameState GameState { get; set; }

        public Stack<List<FlyingObject>> OldFlyingObjects { get; } = new Stack<List<FlyingObject>>();

        public CollisionHandler CollisionHandler { get; }

        private readonly GamePainter painter;
        private readonly Timer enemiesCreatingTimer = new Timer();
        private readonly Timer objectsMovingTimer = new Timer();

        #endregion

        public GameController(Size gameFieldSize)
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
            painter = new GamePainter(this);
            GameState = new PlayingGameState(this);
            GameField = new Field(new Point2D(), gameFieldSize);
            Ground = new Ground(new Point2D(0, gameFieldSize.Height - 30), gameFieldSize);
            Player = FlyingObjectsFactory.GetPlayerShip(GameField, Ground);

            enemiesCreatingTimer.Interval = 1000;
            enemiesCreatingTimer.Tick += (s, e) => AddNewRandomEnemy();
            enemiesCreatingTimer.Start();

            objectsMovingTimer.Interval = 1;
            objectsMovingTimer.Tick += (s, e) => Update();
            objectsMovingTimer.Start();  
        }

        #region Methods

        private void Update()
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

        private void AddNewRandomEnemy()
        {
            GameState.AddNewRandomEnemy();
        }

        public void Paint(Graphics graphics)
        {
            painter.Paint(graphics);
        }

        public void StartRewind()
        {
            GameState.BeginRewind();
        }

        public void EndRewind()
        {
            GameState.EndRewind();
        }

        #endregion
    }
}





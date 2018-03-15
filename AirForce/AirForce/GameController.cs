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
        public List<FlyingObject> FlyingObjects { get; } = new List<FlyingObject>();
        public FlyingObjectsFactory FlyingObjectsFactory { get; } = new FlyingObjectsFactory();
        public FlyingObject Player
        {
            get
            {
                if (!FlyingObjects.Any())
                    return null;

                if (FlyingObjects[0].Type != FlyingObjectType.PlayerShip)
                    return null;

                return FlyingObjects[0];
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

        private readonly GamePainter painter;
        private readonly CollisionHandler collisionHandler;
        private readonly Timer enemiesCreatingTimer = new Timer();
        private readonly Timer objectsMovingTimer = new Timer();
        private readonly Random random = new Random();

        #endregion

        public GameController(Size gameFieldSize)
        {
            var collisionTable = new Dictionary<FlyingObjectType, FlyingObjectType[]>
            {
                { FlyingObjectType.PlayerShip, new []{ FlyingObjectType.Meteor, FlyingObjectType.BigShip, FlyingObjectType.ChaserShip, FlyingObjectType.EnemyBullet, FlyingObjectType.FlyingSaucer } },
                { FlyingObjectType.BigShip, new[]{ FlyingObjectType.Meteor, FlyingObjectType.PlayerShip, FlyingObjectType.PlayerBullet } },
                { FlyingObjectType.ChaserShip, new []{ FlyingObjectType.Meteor, FlyingObjectType.PlayerShip, FlyingObjectType.PlayerBullet } },
                { FlyingObjectType.FlyingSaucer, new []{ FlyingObjectType.PlayerShip } },
                { FlyingObjectType.Meteor, new []{ FlyingObjectType.PlayerShip, FlyingObjectType.BigShip, FlyingObjectType.ChaserShip, FlyingObjectType.EnemyBullet, FlyingObjectType.PlayerBullet } },
                { FlyingObjectType.PlayerBullet, new []{ FlyingObjectType.BigShip, FlyingObjectType.ChaserShip, FlyingObjectType.Meteor } },
                { FlyingObjectType.EnemyBullet, new []{ FlyingObjectType.Meteor, FlyingObjectType.Meteor } }
            };

            collisionHandler = new CollisionHandler(collisionTable);
            painter = new GamePainter(this);
            GameField = new Field(new Point2D(), gameFieldSize);
            Ground = new Ground(new Point2D(0, gameFieldSize.Height - 30), gameFieldSize);
            Player = FlyingObjectsFactory.GetPlayerShip(GameField, Ground);
            GameState = new PlayingGameState(this);

            enemiesCreatingTimer.Interval = 1000;
            enemiesCreatingTimer.Tick += (s, e) => AddNewRandomEnemy();
            enemiesCreatingTimer.Start();

            objectsMovingTimer.Interval = 1;
            objectsMovingTimer.Tick += (s, e) => Update();
            objectsMovingTimer.Start();  
        }

        #region Methods

        public void MovePlayer(Point2D movespeedModifer)
        {
            GameState.MovePlayer(movespeedModifer);
        }

        public void PlayerFire()
        {
            GameState.PlayerFire();
        }

        private void Update()
        {
            //FlyingObjects.AddRange(collisionHandler.GetNewEnemyBullets(FlyingObjects, GameField, Ground, FlyingObjectsFactory));

            foreach (FlyingObject obj in FlyingObjects)
                obj.Move(GameField, Ground, FlyingObjects);

            collisionHandler.HandleCollisions(FlyingObjects, GameField, Ground);

            if (Player.Strength <= 0)
                GameState = new WaitingGameState(this);
        }

        public void Restart()
        {
            GameState.Restart();
        }

        private void AddNewRandomEnemy()
        {
            switch (random.Next(0, 4))
            {
                case 0:
                    FlyingObjects.Add(FlyingObjectsFactory.GetBigShip(GameField, Ground));
                    break;
                case 1:
                    FlyingObjects.Add(FlyingObjectsFactory.GetChaserShip(GameField, Ground));
                    break;
                case 2:
                    FlyingObjects.Add(FlyingObjectsFactory.GetFlyingSaucer(GameField, Ground));
                    break;
                case 3:
                    FlyingObjects.Add(FlyingObjectsFactory.GetMeteor(GameField, Ground));
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Paint(Graphics graphics)
        {
            painter.Paint(graphics);
        }

        #endregion
    }
}





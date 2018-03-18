using System;

namespace AirForce
{
    public class FlyingObjectsFactory
    {
        private readonly Random random = new Random();

        public FlyingObject CreatePlayerShip(Field gameField, Ground ground)
        {
            var playerShip = new FlyingObject(
                type: FlyingObjectType.PlayerShip,
                position: new Point2D(gameField.TopLeftPoint.X + 150, ground.Position.Y / 2),
                radius: 30,
                movespeed: 4,
                strength: 100,
                image: Properties.Resources.player_ship);

            playerShip.ManualMover = new ManualMover(playerShip);

            return playerShip;
        }

        public FlyingObject CreateBigShip(Field gameField, Ground ground)
        {
            var bigShip = new FlyingObject(
                type: FlyingObjectType.BigShip,
                position: new Point2D(x: gameField.Size.Width + 50, y: random.Next(50, ground.Position.Y - 50)),
                radius: 50,
                movespeed: 8,
                strength: 3,
                image: Properties.Resources.big_enemy_ship);

            bigShip.Mover = new ShiftOnDirrectionMover(bigShip, shift: new Point2D(-1, 0));

            return bigShip;
        }

        public FlyingObject CreateChaserShip(Field gameField, Ground ground)
        {
            var chaserShip = new ShootingFlyingObject(
                type: FlyingObjectType.ChaserShip,
                position: new Point2D(x: gameField.Size.Width + 30, y: random.Next(30, ground.Position.Y - 30)),
                radius: 30,
                movespeed: 3,
                strength: 1,
                image: Properties.Resources.chaser_ship);

            chaserShip.Mover = new DodgingMover(chaserShip);

            return chaserShip;
        }

        public FlyingObject CreateFlyingSaucer(Field gameField, Ground ground)
        {
            var flyingSaucer = new FlyingObject(
                type: FlyingObjectType.FlyingSaucer,
                position: new Point2D(x: gameField.Size.Width + 25, y: random.Next(ground.Position.Y - 5 * 25, ground.Position.Y - 25)),
                radius: 25,
                movespeed: 2,
                strength: 1,
                image: Properties.Resources.flying_saucer);

            flyingSaucer.Mover = new ChaoticMovingMover(flyingSaucer);

            return flyingSaucer;
        }

        public FlyingObject CreateEnemyBullet(Field gameField, Ground ground, FlyingObject enemy)
        {
            var enemyBullet = new FlyingObject(
                type: FlyingObjectType.EnemyBullet,
                position: new Point2D(enemy.Position.X - enemy.Radius, enemy.Position.Y),
                radius: 8,
                movespeed: 8,
                strength: 1,
                image: Properties.Resources.enemy_bullet);

            enemyBullet.Mover = new ShiftOnDirrectionMover(enemyBullet, shift: new Point2D(-1, 0));

            return enemyBullet;
        }

        public FlyingObject CreatePlayerBullet(Field gameField, Ground ground, FlyingObject player)
        {
            var playerBullet = new FlyingObject(
                type: FlyingObjectType.PlayerBullet,
                position: new Point2D(x: player.Position.X + player.Radius, y: player.Position.Y),
                radius: 8,
                movespeed: 8,
                strength: 1,
                image: Properties.Resources.player_bullet);

            playerBullet.Mover = new ShiftOnDirrectionMover(playerBullet, shift: new Point2D(1, 0));

            return playerBullet;
        }

        public FlyingObject CreateMeteor(Field gameField, Ground ground)
        {
            var meteor = new FlyingObject(
                type: FlyingObjectType.Meteor,
                position: new Point2D(random.Next(0, gameField.Size.Width), -100),
                radius: 100,
                movespeed: 2,
                strength: random.Next(8, 16),
                image: Properties.Resources.meteor);

            meteor.Mover = new ShiftOnDirrectionMover(meteor, shift: new Point2D(-2, 1));

            return meteor;
        }

        public FlyingObject CreateDeadPlayer(Field gameField, Ground ground)
        {
            return new FlyingObject(
                type: FlyingObjectType.PlayerShip,
                position: new Point2D(gameField.TopLeftPoint.X + 150, ground.Position.Y / 2),
                radius: 30,
                movespeed: 4,
                strength: 0, 
                image: Properties.Resources.player_ship);
        }

        public FlyingObject CreateRandomEnemy(Field gameField, Ground ground)
        {
            switch (random.Next(0, 4))
            {
                case 0: return CreateBigShip(gameField, ground);
                case 1: return CreateChaserShip(gameField, ground);
                case 2: return CreateFlyingSaucer(gameField, ground);
                case 3: return CreateMeteor(gameField, ground);

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
using System;

namespace AirForce
{
    public class FlyingObjectsFactory
    {
        private readonly Random random = new Random();

        public FlyingObject CreatePlayerShip(Field field, Ground ground)
        {
            var playerShip = new FlyingObject(
                type: FlyingObjectType.PlayerShip,
                position: new Point2D(field.TopLeftPoint.X + 150, ground.Location.Y / 2),
                radius: 30,
                movespeed: 4,
                strength: 100,
                radiusOfSight: 10 * 30,
                image: Properties.Resources.player_ship);

            playerShip.ManualMover = new ManualMover(playerShip);

            return playerShip;
        }

        public FlyingObject CreateBigShip(Field field, Ground ground)
        {
            var bigShip = new FlyingObject(
                type: FlyingObjectType.BigShip,
                position: new Point2D(x: field.Size.Width + 50, y: random.Next(50, ground.Location.Y - 50)),
                radius: 50,
                movespeed: 8,
                strength: 3,
                radiusOfSight: 10 * 50,
                image: Properties.Resources.big_enemy_ship);

            bigShip.Mover = new ShiftOnDirrectionMover(bigShip, shiftVector: new Point2D(-1, 0));

            return bigShip;
        }

        public FlyingObject CreateChaserShip(Field field, Ground ground)
        {
            var chaserShip = new ShootingFlyingObject(
                type: FlyingObjectType.ChaserShip,
                position: new Point2D(x: field.Size.Width + 30, y: random.Next(30, ground.Location.Y - 30)),
                radius: 30,
                movespeed: 3,
                strength: 1,
                radiusOfSight: 10 * 30,
                image: Properties.Resources.chaser_ship);

            chaserShip.Mover = new DodgingMover(chaserShip);

            return chaserShip;
        }

        public FlyingObject CreateFlyingSaucer(Field field, Ground ground)
        {
            var flyingSaucer = new FlyingObject(
                type: FlyingObjectType.FlyingSaucer,
                position: new Point2D(x: field.Size.Width + 25, y: random.Next(ground.Location.Y - 5 * 25, ground.Location.Y - 25)),
                radius: 25,
                movespeed: 2,
                strength: 1,
                radiusOfSight: 10 * 25,
                image: Properties.Resources.flying_saucer);

            flyingSaucer.Mover = new ChaoticMovingMover(flyingSaucer);

            return flyingSaucer;
        }

        public FlyingObject CreateEnemyBullet(Field field, Ground ground, FlyingObject source)
        {
            var enemyBullet = new FlyingObject(
                type: FlyingObjectType.EnemyBullet,
                position: new Point2D(source.Position.X - source.Radius, source.Position.Y),
                radius: 8,
                movespeed: 8,
                strength: 1,
                radiusOfSight: 10 * 8,
                image: Properties.Resources.enemy_bullet);

            enemyBullet.Mover = new ShiftOnDirrectionMover(enemyBullet, shiftVector: new Point2D(-1, 0));

            return enemyBullet;
        }

        public FlyingObject CreatePlayerBullet(Field field, Ground ground, FlyingObject player)
        {
            var playerBullet = new FlyingObject(
                type: FlyingObjectType.PlayerBullet,
                position: new Point2D(x: player.Position.X + player.Radius, y: player.Position.Y),
                radius: 8,
                movespeed: 8,
                strength: 1,
                radiusOfSight: 10 * 8,
                image: Properties.Resources.player_bullet);

            playerBullet.Mover = new ShiftOnDirrectionMover(playerBullet, shiftVector: new Point2D(1, 0));

            return playerBullet;
        }

        public FlyingObject CreateMeteor(Field field, Ground ground)
        {
            var meteor = new FlyingObject(
                type: FlyingObjectType.Meteor,
                position: new Point2D(random.Next(0, field.Size.Width), -100),
                radius: 100,
                movespeed: 2,
                strength: random.Next(8, 16),
                radiusOfSight: 10 * 100,
                image: Properties.Resources.meteor);

            meteor.Mover = new ShiftOnDirrectionMover(meteor, shiftVector: new Point2D(-2, 1));

            return meteor;
        }

        public FlyingObject CreateDeadPlayer(Field field, Ground ground)
        {
            return new FlyingObject(
                type: FlyingObjectType.PlayerShip,
                position: new Point2D(field.TopLeftPoint.X + 150, ground.Location.Y / 2),
                radius: 30,
                movespeed: 4,
                strength: 0,
                radiusOfSight: 10 * 30,
                image: Properties.Resources.player_ship);
        }

        public FlyingObject CreateRandomEnemy(Field field, Ground ground)
        {
            switch (random.Next(0, 4))
            {
                case 0: return CreateBigShip(field, ground);
                case 1: return CreateChaserShip(field, ground);
                case 2: return CreateFlyingSaucer(field, ground);
                case 3: return CreateMeteor(field, ground);

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
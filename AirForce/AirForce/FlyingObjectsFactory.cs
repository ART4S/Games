using System;
using System.Drawing;

namespace AirForce
{
    public class FlyingObjectsFactory
    {
        private readonly Random random = new Random();

        public FlyingObject GetPlayerShip(Field gameField, Ground ground)
        {
            var playerShip = new FlyingObject(
                FlyingObjectType.PlayerShip,
                new Point2D(gameField.TopLeftPoint.X + 150, ground.Position.Y / 2),
                30,
                4,
                100,
                Properties.Resources.player_ship);

            playerShip.Painter = new FlyingObjectPainter(playerShip);
            playerShip.ManualMover = new PlayerShipManualMover(playerShip);

            return playerShip;
        }

        public FlyingObject GetBigShip(Field gameField, Ground ground)
        {
            int radius = 50;

            var bigShip = new FlyingObject(
                FlyingObjectType.BigShip,
                new Point2D(
                    x: gameField.Size.Width + radius,
                    y: random.Next(radius, ground.Position.Y - radius)),
                radius,
                8,
                3,
                Properties.Resources.big_enemy_ship);

            bigShip.Painter = new FlyingObjectPainter(bigShip);
            bigShip.Mover = new BigShipMover(bigShip);

            return bigShip;
        }

        public FlyingObject GetChaserShip(Field gameField, Ground ground)
        {
            int radius = 30;

            var chaserShip = new ShootingFlyingObject(
                FlyingObjectType.ChaserShip,
                new Point2D(
                    x: gameField.Size.Width + radius,
                    y: random.Next(radius, ground.Position.Y - radius)),
                radius,
                3,
                1,
                Properties.Resources.chaser_ship);

            chaserShip.Painter = new FlyingObjectPainter(chaserShip);
            chaserShip.Mover = new ChaserShipMover(chaserShip);

            return chaserShip;
        }

        public FlyingObject GetFlyingSaucer(Field gameField, Ground ground)
        {
            int radius = 25;

            var flyingSaucer = new FlyingObject(
                FlyingObjectType.FlyingSaucer,
                new Point2D(
                    x: gameField.Size.Width + radius,
                    y: random.Next(ground.Position.Y - 5 * radius, ground.Position.Y - radius)),
                radius,
                2,
                1,
                Properties.Resources.flying_saucer);

            flyingSaucer.Painter = new FlyingObjectPainter(flyingSaucer);
            flyingSaucer.Mover = new FlyingSaucerMover(flyingSaucer);

            return flyingSaucer;
        }

        public FlyingObject GetEnemyBullet(Field gameField, Ground ground, FlyingObject enemy)
        {
            var enemyBullet = new FlyingObject(
                FlyingObjectType.EnemyBullet,
                new Point2D(enemy.Position.X - enemy.Radius, enemy.Position.Y),
                8,
                8,
                1,
                Properties.Resources.enemy_bullet);

            enemyBullet.Painter = new FlyingObjectPainter(enemyBullet);
            enemyBullet.Mover = new EnemyBulletMover(enemyBullet);

            return enemyBullet;
        }

        public FlyingObject GetPlayerBullet(Field gameField, Ground ground, FlyingObject player)
        {
            var playerBullet = new FlyingObject(
                FlyingObjectType.PlayerBullet,
                new Point2D(
                    x: player.Position.X + player.Radius,
                    y: player.Position.Y),
                8,
                8,
                1,
                Properties.Resources.player_bullet);

            playerBullet.Painter = new FlyingObjectPainter(playerBullet);
            playerBullet.Mover = new PlayerBulletMover(playerBullet);

            return playerBullet;
        }

        public FlyingObject GetMeteor(Field gameField, Ground ground)
        {
            int strength = random.Next(8, 15 + 1);

            Image image = strength == 15 ?
                Properties.Resources.asteroid :
                Properties.Resources.meteor;

            var meteor = new FlyingObject(
                FlyingObjectType.Meteor,
                new Point2D(random.Next(0, gameField.Size.Width), 0),
                100,
                2,
                strength,
                image);

            meteor.Painter = new FlyingObjectPainter(meteor);
            meteor.Mover = new MeteorMover(meteor);

            return meteor;
        }

        public FlyingObject GetDeadPlayer(Field gameField, Ground ground)
        {
            var playerShip = new FlyingObject(
                FlyingObjectType.PlayerShip,
                new Point2D(gameField.TopLeftPoint.X + 150, ground.Position.Y / 2),
                30,
                4,
                0,
                Properties.Resources.player_ship);

            return playerShip;
        }

        public FlyingObject GetRandomEnemy(Field gameField, Ground ground)
        {
            switch (random.Next(0, 4))
            {
                case 0: return GetBigShip(gameField, ground);
                case 1: return GetChaserShip(gameField, ground);
                case 2: return GetFlyingSaucer(gameField, ground);
                case 3: return GetMeteor(gameField, ground);

                default: throw new ArgumentOutOfRangeException();
            }
        }
    }
}
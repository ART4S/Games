using System;
using System.Drawing;

namespace AirForce
{
    public class FlyingObjectsFactory
    {
        private readonly Random random = new Random();

        public FlyingObject GetPlayerShip(Field gameField, Ground ground)
        {
            FlyingObjectType type = FlyingObjectType.PlayerShip;
            Point2D position = new Point2D(gameField.TopLeftPoint.X + 150, ground.Position.Y / 2);
            int radius = 30;
            int movespeed = 4;
            int strength = 100;
            Image image = Properties.Resources.player_ship;

            var playerShip = new FlyingObject(type, position, radius, movespeed, strength, image);

            playerShip.Painter = new FlyingObjectPainter(playerShip);
            playerShip.ManualMover = new PlayerShipManualMover(playerShip);

            return playerShip;
        }

        public FlyingObject GetBigShip(Field gameField, Ground ground)
        {
            FlyingObjectType type = FlyingObjectType.BigShip;
            int radius = 50;
            Point2D position = new Point2D(
                x: gameField.Size.Width + radius,
                y: random.Next(radius, ground.Position.Y - radius));
            int movespeed = 8;
            int strength = 3;
            Image image = Properties.Resources.big_enemy_ship;

            var bigShip = new FlyingObject(type, position, radius, movespeed, strength, image);

            bigShip.Painter = new FlyingObjectPainter(bigShip);
            bigShip.Mover = new BigShipMover(bigShip);

            return bigShip;
        }

        public FlyingObject GetChaserShip(Field gameField, Ground ground)
        {
            FlyingObjectType type = FlyingObjectType.ChaserShip;
            int radius = 30;
            Point2D position = new Point2D(
                x: gameField.Size.Width + radius,
                y: random.Next(radius, ground.Position.Y - radius));
            int movespeed = 3;
            int strength = 1;
            Image image = Properties.Resources.chaser_ship;

            var chaserShip = new FlyingObject(type, position, radius, movespeed, strength, image);

            chaserShip.Painter = new FlyingObjectPainter(chaserShip);
            chaserShip.Mover = new ChaserShipMover(chaserShip);

            return chaserShip;
        }

        public FlyingObject GetFlyingSaucer(Field gameField, Ground ground)
        {
            FlyingObjectType type = FlyingObjectType.FlyingSaucer;
            int radius = 25;
            Point2D position = new Point2D(
                x: gameField.Size.Width + radius,
                y: random.Next(ground.Position.Y - 5 * radius, ground.Position.Y - radius));
            int movespeed = 2;
            int strength = 1;
            Image image = Properties.Resources.flying_saucer;

            var flyingSaucer = new FlyingObject(type, position, radius, movespeed, strength, image);

            flyingSaucer.Painter = new FlyingObjectPainter(flyingSaucer);
            flyingSaucer.Mover = new FlyingSaucerMover(flyingSaucer);

            return flyingSaucer;
        }

        public FlyingObject GetEnemyBullet(Field gameField, Ground ground, FlyingObject enemy)
        {
            FlyingObjectType type = FlyingObjectType.EnemyBullet;
            Point2D position = new Point2D(enemy.Position.X - enemy.Radius, enemy.Position.Y);
            int radius = 8;
            int movespeed = 8;
            int strength = 1;
            Image image = Properties.Resources.enemy_bullet;

            var enemyBullet = new FlyingObject(type, position, radius, movespeed, strength, image);

            enemyBullet.Painter = new FlyingObjectPainter(enemyBullet);
            enemyBullet.Mover = new EnemyBulletMover(enemyBullet);

            return enemyBullet;
        }

        public FlyingObject GetPlayerBullet(Field gameField, Ground ground, FlyingObject player)
        {
            FlyingObjectType type = FlyingObjectType.PlayerBullet;
            Point2D position = new Point2D(
                x: player.Position.X + player.Radius,
                y: player.Position.Y);
            int radius = 8;
            int movespeed = 8;
            int strength = 1;
            Image image = Properties.Resources.player_bullet;

            var playerBullet = new FlyingObject(type, position, radius, movespeed, strength, image);

            playerBullet.Painter = new FlyingObjectPainter(playerBullet);
            playerBullet.Mover = new PlayerBulletMover(playerBullet);

            return playerBullet;
        }

        public FlyingObject GetMeteor(Field gameField, Ground ground)
        {
            FlyingObjectType type = FlyingObjectType.Meteor;
            Point2D position = new Point2D(random.Next(0, gameField.Size.Width), 0);
            int radius = 100;
            int movespeed = 2;
            int strength = random.Next(8, 15 + 1);
            Image image = strength == 15 ?
                Properties.Resources.asteroid :
                Properties.Resources.meteor;

            var meteor = new FlyingObject(type, position, radius, movespeed, strength, image);

            meteor.Painter = new FlyingObjectPainter(meteor);
            meteor.Mover = new MeteorMover(meteor);

            return meteor;
        }

        public FlyingObject GetDeadPlayer(Field gameField, Ground ground)
        {
            FlyingObjectType type = FlyingObjectType.PlayerShip;
            Point2D position = new Point2D(gameField.TopLeftPoint.X + 150, ground.Position.Y / 2);
            int radius = 30;
            int movespeed = 4;
            int strength = 0;
            Image image = Properties.Resources.player_ship;

            return new FlyingObject(type, position, radius, movespeed, strength, image);
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
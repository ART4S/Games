using System.Collections.Generic;
using System.Drawing;

namespace AirForce
{
    public class FlyingObject
    {
        public Point2D Position { get; set; }
        public int Strength { get; set; }
        public int Movespeed { get; }
        public int Radius { get; }
        public int RadiusOfSight { get; }
        public FlyingObjectType Type { get; }
        private readonly Image image;

        public IMover Mover { get; set; }
        public IManualMover ManualMover { get; set; }
        private readonly Rewinder rewinder;

        public FlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, int radiusOfSight, Image image)
        {
            Type = type;
            Position = position;
            Strength = strength;
            Radius = radius;
            Movespeed = movespeed;
            RadiusOfSight = radiusOfSight;
            this.image = image;

            rewinder = new Rewinder(this);
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects)
        {
            Mover?.Move(gameField, ground, flyingObjects);
        }

        public void MoveManyally(Point2D movespeedModifer, Field gameField, Ground ground)
        {
            ManualMover?.MoveManually(movespeedModifer, gameField, ground);
        }

        public void SaveState()
        {
            rewinder.SaveState();
        }

        public bool CanRestorePreviousState()
        {
            return rewinder.CanRestorePreviousState();
        }

        public void RestorePreviousState()
        {
            rewinder.RestorePreviousState();
        }

        public void Paint(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle(
                location: Position - new Point2D(Radius, Radius),
                size: new Size(2 * Radius, 2 * Radius)
                );

            graphics.DrawImage(image, imageRectangle);
        }
    }
}

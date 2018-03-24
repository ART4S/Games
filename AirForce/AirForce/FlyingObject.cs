using System.Collections.Generic;
using System.Drawing;

namespace AirForce
{
    public class FlyingObject
    {
        public Point2D Position { get; set; }
        public Point2D StartPosition { get; }
        public int Strength { get; set; }
        public int Movespeed { get; }
        public int Radius { get; }
        public int RadiusOfSight { get; }
        public FlyingObjectType Type { get; }
        private readonly Image image;

        public IMover Mover { get; set; }
        public IManualMover ManualMover { get; set; }

        public FlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, int radiusOfSight, Image image)
        {
            Type = type;
            Position = position;
            StartPosition = position;
            Strength = strength;
            Radius = radius;
            Movespeed = movespeed;
            RadiusOfSight = radiusOfSight;
            this.image = image;
        }

        public void Move(Field field, Ground ground, List<FlyingObject> objectsOnField, RewindMacroCommand rewindMacroCommand)
        {
            Mover?.Move(field, ground, objectsOnField, rewindMacroCommand);
        }

        public void MoveManyally(Point2D movespeedModifer, Field field, Ground ground, RewindMacroCommand rewindMacroCommand)
        {
            ManualMover?.MoveManually(movespeedModifer, field, ground, rewindMacroCommand);
        }

        public void Paint(Graphics graphics)
        {
            Rectangle imageRectangle = new Rectangle(
                location: Position - new Point2D(Radius, Radius),
                size: new Size(2 * Radius, 2 * Radius));

            graphics.DrawImage(image, imageRectangle);
        }
    }
}

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
        public FlyingObjectType Type { get; }
        public Image Image { get; }

        public IMover Mover { get; set; }
        public IPainer Painter { get; set; }
        public IManualMover ManualMover { get; set; }

        public FlyingObject(FlyingObjectType type, Point2D position, int radius, int movespeed, int strength, Image image)
        {
            Type = type;
            Position = position;
            Strength = strength;
            Radius = radius;
            Movespeed = movespeed;
            Image = image;
        }

        public void Move(Field gameField, Ground ground, List<FlyingObject> flyingObjects)
        {
            Mover?.Move(gameField, ground, flyingObjects);
        }

        public void MoveManyally(Point2D movespeedModifer, Field gameField, Ground ground)
        {
            ManualMover?.MoveManually(movespeedModifer, gameField, ground);
        }

        public void Paint(Graphics graphics)
        {
            Painter?.Paint(graphics);
        }

        public void UndoMove()
        {
            Mover?.UndoMove();
        }
    }
}

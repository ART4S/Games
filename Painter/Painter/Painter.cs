using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint
{
    public class Painter
    {
        private readonly List<IGraphicObject> graphicObjects;

        public Painter()
        {
            graphicObjects = new List<IGraphicObject>();
        }

        public void AddGraphicObject(IGraphicObject graphicObject)
        {
            graphicObjects.Add(graphicObject);
        }

        public void DrawGraphicObjects(Graphics graphics)
        {
            foreach (IGraphicObject graphicObject in graphicObjects)
                graphicObject.Draw(graphics);
        }

        public void RemoveLastAddedGraphicObject()
        {
            graphicObjects.RemoveLast();
        }

        public void ClearGraphicObjects()
        {
            graphicObjects.Clear();
        }

        public void MoveLastAddedGraphicObject(MoveDirection direction, int moveRange)
        {
            if (!graphicObjects.Any())
                return;

            graphicObjects.Last().Move(direction, moveRange);
        }

        public void RotateClockwiseLastAddedGraphicObject(double angle)
        {
            if (!graphicObjects.Any())
                return;

            IRotatable rotatableGraphicObject = graphicObjects.Last() as IRotatable;

            rotatableGraphicObject?.RotateClockwise(angle);
        }

        public void AddPointToLastAddedCurve(PointF point)
        {
            if (!graphicObjects.Any())
                return;

            Curve lastGraphicObject = graphicObjects.Last() as Curve;

            lastGraphicObject?.AddPoint(point);
        }
    }
}
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphicObjects.ForEach(graphicObject => graphicObject.Draw(graphics));
        }

        public void RemoveLastAddedGraphicObject()
        {
            graphicObjects.RemoveLast();
        }

        public void ClearGraphicObjects()
        {
            graphicObjects.Clear();
        }

        public void MoveLastAddedGraphicObject(MoveDirrection dirrection, int moveRange)
        {
            if (!graphicObjects.Any())
                return;

            graphicObjects.Last().Move(dirrection, moveRange);
        }

        public void RotateClockwiseLastAddedGraphicObject(double angle)
        {
            if (!graphicObjects.Any())
                return;

            IGraphicObject lastAddedGraphicObject = graphicObjects.Last();

            if (lastAddedGraphicObject is Polygon)
            {
                Polygon polygon = lastAddedGraphicObject as Polygon;

                polygon.RotateClockwise(angle);
            }

            if (lastAddedGraphicObject is BezierShape)
            {
                BezierShape bezierShape = lastAddedGraphicObject as BezierShape;

                bezierShape.RotateClockwise(angle);
            }
        }
    }
}
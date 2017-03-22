using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Painter
{
    public class ShapesPainter
    {
        private readonly List<Circle> circles;
        private readonly List<Rectangle> rectangles;
        private readonly List<Polygon> polygons;
        private readonly List<BezierShape> bezierShapes;
        private readonly List<Shape> addedShapes;

        public ShapesPainter()
        {
            circles = new List<Circle>();
            rectangles = new List<Rectangle>();
            polygons = new List<Polygon>();
            bezierShapes = new List<BezierShape>();
            addedShapes = new List<Shape>();
        }

        public void DrawShapes(Graphics graphics)
        {
            if (!addedShapes.Any())
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int circlesCurrentIndex = 0;
            int rectanglesCurrentIndex = 0;
            int polygonsCurrentIndex = 0;
            int bezierShapesCurrentIndex = 0;

            foreach (Shape shape in addedShapes)
            {
                switch (shape)
                {
                    case Shape.Circle:
                        circles[circlesCurrentIndex].Draw(graphics);
                        circlesCurrentIndex++;
                        break;
                    case Shape.Rectangle:
                        rectangles[rectanglesCurrentIndex].Draw(graphics);
                        rectanglesCurrentIndex++;
                        break;
                    case Shape.Polygon:
                        polygons[polygonsCurrentIndex].Draw(graphics);
                        polygonsCurrentIndex++;
                        break;
                    case Shape.Bezier:
                        bezierShapes[bezierShapesCurrentIndex].Draw(graphics);
                        bezierShapesCurrentIndex++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
                }
            }
        }

        public void AddCircle(Circle circle)
        {
            circles.Add(circle);
            addedShapes.Add(Shape.Circle);
        }

        public void AddRectangle(Rectangle rectangle)
        {
            rectangles.Add(rectangle);
            addedShapes.Add(Shape.Rectangle);
        }

        public void AddPolygon(Polygon polygon)
        {
            polygons.Add(polygon);
            addedShapes.Add(Shape.Polygon);
        }

        public void AddBezierShape(BezierShape bezierShape)
        {
            bezierShapes.Add(bezierShape);
            addedShapes.Add(Shape.Bezier);
        }

        public void RemoveLastShape()
        {
            if (!addedShapes.Any())
                return;

            switch (addedShapes.Last())
            {
                case Shape.Circle:
                    circles.RemoveLast();
                    break;
                case Shape.Rectangle:
                    rectangles.RemoveLast();
                    break;
                case Shape.Polygon:
                    polygons.RemoveLast();
                    break;
                case Shape.Bezier:
                    bezierShapes.RemoveLast();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(null);
            }

            addedShapes.RemoveLast();
        }

        public void ClearShapes()
        {
            circles.Clear();
            rectangles.Clear();
            polygons.Clear();
            bezierShapes.Clear();
            addedShapes.Clear();
        }

        public void MoveLastShape(MoveDirrection dirrection, int moveRange)
        {
            if (!addedShapes.Any())
                return;

            switch (addedShapes.Last())
            {
                case Shape.Circle:
                    circles.Last().Move(dirrection, moveRange);
                    break;
                case Shape.Rectangle:
                    rectangles.Last().Move(dirrection, moveRange);
                    break;
                case Shape.Polygon:
                    polygons.Last().Move(dirrection, moveRange);
                    break;
                case Shape.Bezier:
                    bezierShapes.Last().Move(dirrection, moveRange);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(null);
            }
        }

        public void RotateClockwiseLastShape(double angle)
        {
            if (!addedShapes.Any())
                return;

            switch (addedShapes.Last())
            {
                case Shape.Polygon:
                    polygons.Last().RotateClockwise(angle);
                    break;
                case Shape.Bezier:
                    break;
            }
        }
    }
}
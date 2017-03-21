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
        private readonly List<Shape> addedShapesList;

        public ShapesPainter()
        {
            circles = new List<Circle>();
            rectangles = new List<Rectangle>();
            polygons = new List<Polygon>();
            addedShapesList = new List<Shape>();
        }

        public void DrawShapes(Graphics graphics)
        {
            if (!addedShapesList.Any())
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int circlesCurrentIndex = 0;
            int rectanglesCurrentIndex = 0;
            int polygonsCurrentIndex = 0;

            foreach (Shape shape in addedShapesList)
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
                    default:
                        throw new ArgumentOutOfRangeException(nameof(shape), shape, null);
                }
            }
        }

        public void AddCircle(Circle circle)
        {
            circles.Add(circle);
            addedShapesList.Add(Shape.Circle);
        }

        public void AddRectangle(Rectangle rectangle)
        {
            rectangles.Add(rectangle);
            addedShapesList.Add(Shape.Rectangle);
        }

        public void AddPolygon(Polygon polygon)
        {
            polygons.Add(polygon);
            addedShapesList.Add(Shape.Polygon);
        }

        public void RemoveLastShape()
        {
            if (!addedShapesList.Any())
                return;

            switch (addedShapesList.Last())
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
                default:
                    throw new ArgumentOutOfRangeException(null);
            }

            addedShapesList.RemoveLast();
        }

        public void ClearShapes()
        {
            circles.Clear();
            rectangles.Clear();
            polygons.Clear();
            addedShapesList.Clear();
        }

        public void MoveLastShape(MoveDirrection dirrection, int moveRange)
        {
            if (!addedShapesList.Any())
                return;

            switch (addedShapesList.Last())
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
                default:
                    throw new ArgumentOutOfRangeException(null);
            }
        }

        public void RotateClockwiseLastPolygon()
        {
            if (!addedShapesList.Any() || addedShapesList.Last() != Shape.Polygon)
                return;

            polygons.Last().RotateClockwise();
        }

        public void RotateCounter—lockwiseLastPolygon()
        {
            if (!addedShapesList.Any() || addedShapesList.Last() != Shape.Polygon)
                return;

            polygons.Last().RotateCounter—lockwise();
        }
    }
}
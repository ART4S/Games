using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace Paint
{
    public class ShapesPainter
    {
        private readonly List<Circle> circles;
        private readonly List<Rectangle> rectangles;
        private readonly List<Polygon> polygons;

        private readonly Stack<Shape> addedShapesStack;

        public ShapesPainter(List<Circle> circles, List<Rectangle> rectangles, List<Polygon> polygons)
        {
            this.circles = circles;
            this.rectangles = rectangles;
            this.polygons = polygons;

            addedShapesStack = new Stack<Shape>();
        }

        public ShapesPainter() : this(new List<Circle>(), new List<Rectangle>(), new List<Polygon>())
        {
            
        }

        public void DrawShapes(Graphics graphics)
        {
            if (!addedShapesStack.Any())
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Queue<Shape> drawingQueue = new Queue<Shape>(addedShapesStack.Reverse());

            int circlesCurrentIndex = 0;
            int rectanglesCurrentIndex = 0;
            int polygonsCurrentIndex = 0;

            while (drawingQueue.Any())
            {
                Shape currentShape = drawingQueue.Dequeue();

                switch (currentShape)
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
                        throw new ArgumentOutOfRangeException(nameof(circlesCurrentIndex), circlesCurrentIndex, null);
                }
            }
        }

        public void AddCircle(Circle circle)
        {
            circles.Add(circle);
            addedShapesStack.Push(Shape.Circle);
        }

        public void AddRectangle(Rectangle rectangle)
        {
            rectangles.Add(rectangle);
            addedShapesStack.Push(Shape.Rectangle);
        }

        public void AddPolygon(Polygon polygon)
        {
            polygons.Add(polygon);
            addedShapesStack.Push(Shape.Polygon);
        }

        public void DeleteLastShape()
        {
            if (!addedShapesStack.Any())
                return;

            Shape lastShape = addedShapesStack.Pop();

            switch (lastShape)
            {
                case Shape.Circle:
                    circles.RemoveAt(circles.Count - 1);
                    break;
                case Shape.Rectangle:
                    rectangles.RemoveAt(rectangles.Count - 1);
                    break;
                case Shape.Polygon:
                    polygons.RemoveAt(polygons.Count - 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lastShape), lastShape, null);
            }
        }

        public void ClearShapes()
        {
            circles.Clear();
            rectangles.Clear();
            polygons.Clear();
            addedShapesStack.Clear();
        }

        public void MoveLastShape(MoveDirrection dirrection, int moveRange)
        {
            if (!addedShapesStack.Any())
                return;

            Shape lastShape = addedShapesStack.Peek();

            switch (lastShape)
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
                    throw new ArgumentOutOfRangeException(nameof(lastShape), lastShape, null);
            }
        }
    }
}
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

        private readonly Stack<ShapeType> addedShapesStack;

        public ShapesPainter(List<Circle> circles, List<Rectangle> rectangles, List<Polygon> polygons)
        {
            this.circles = circles;
            this.rectangles = rectangles;
            this.polygons = polygons;

            addedShapesStack = new Stack<ShapeType>();
        }

        public ShapesPainter() : this(new List<Circle>(), new List<Rectangle>(), new List<Polygon>())
        {
            
        }

        public void DrawShapes(Graphics graphics)
        {
            if (!addedShapesStack.Any())
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            Queue<ShapeType> drawingQueue = new Queue<ShapeType>(addedShapesStack.Reverse());

            int circlesCurrentIndex = 0;
            int rectanglesCurrentIndex = 0;
            int polygonsCurrentIndex = 0;

            while (drawingQueue.Any())
            {
                ShapeType currentShapeType = drawingQueue.Dequeue();

                switch (currentShapeType)
                {
                    case ShapeType.Circle:
                        circles[circlesCurrentIndex].Draw(graphics);
                        circlesCurrentIndex++;
                        break;
                    case ShapeType.Rectangle:
                        rectangles[rectanglesCurrentIndex].Draw(graphics);
                        rectanglesCurrentIndex++;
                        break;
                    case ShapeType.Polygon:
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
            addedShapesStack.Push(ShapeType.Circle);
        }

        public void AddRectangle(Rectangle rectangle)
        {
            rectangles.Add(rectangle);
            addedShapesStack.Push(ShapeType.Rectangle);
        }

        public void AddPolygon(Polygon polygon)
        {
            polygons.Add(polygon);
            addedShapesStack.Push(ShapeType.Polygon);
        }

        public void DeleteLastShape()
        {
            if (!addedShapesStack.Any())
                return;

            ShapeType lastShapeType = addedShapesStack.Pop();

            switch (lastShapeType)
            {
                case ShapeType.Circle:
                    circles.RemoveAt(circles.Count - 1);
                    break;
                case ShapeType.Rectangle:
                    rectangles.RemoveAt(rectangles.Count - 1);
                    break;
                case ShapeType.Polygon:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(lastShapeType), lastShapeType, null);
            }
        }

        public void ClearFigures()
        {
            circles.Clear();
            rectangles.Clear();
            polygons.Clear();
            addedShapesStack.Clear();
        }
    }
}
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

        private readonly Stack<ShapeType> addedShapesStack;

        public ShapesPainter(List<Circle> circles, List<Rectangle> rectangles)
        {
            this.circles = circles;
            this.rectangles = rectangles;

            addedShapesStack = new Stack<ShapeType>();
        }

        public ShapesPainter() : this(new List<Circle>(), new List<Rectangle>())
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

            while (drawingQueue.Any())
            {
                ShapeType currentShapeType = drawingQueue.Dequeue();

                switch (currentShapeType)
                {
                    case ShapeType.Rectangle:
                        rectangles[rectanglesCurrentIndex].Draw(graphics);
                        rectanglesCurrentIndex++;
                        break;
                    case ShapeType.Circle:
                        circles[circlesCurrentIndex].Draw(graphics);
                        circlesCurrentIndex++;
                        break;
                    case ShapeType.Polygon:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
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

        public void DeleteLastShape()
        {
            if (!addedShapesStack.Any())
                return;

            ShapeType lastShapeType = addedShapesStack.Pop();

            switch (lastShapeType)
            {
                case ShapeType.Rectangle:
                    rectangles.RemoveAt(rectangles.Count - 1);
                    break;
                case ShapeType.Circle:
                    circles.RemoveAt(circles.Count - 1);
                    break;
                case ShapeType.Polygon:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ClearFigures()
        {
            circles.Clear();
            rectangles.Clear();
            addedShapesStack.Clear();
        }
    }
}
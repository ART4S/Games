using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;

namespace SimplePainter
{
    public class Painter
    {
        private readonly List<Circle> circles;
        private readonly List<Rectangle> rectangles;
        private readonly List<Polygon> polygons;
        private readonly List<BezierShape> bezierShapes;
        private readonly List<DrawingImage> images;
        private readonly List<GraphicObject> addedGraphicObjects;

        public Painter()
        {
            circles = new List<Circle>();
            rectangles = new List<Rectangle>();
            polygons = new List<Polygon>();
            bezierShapes = new List<BezierShape>();
            images = new List<DrawingImage>();
            addedGraphicObjects = new List<GraphicObject>();
        }

        public void DrawGraphicObjects(Graphics graphics)
        {
            if (!addedGraphicObjects.Any())
                return;

            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int circlesCurrentIndex = 0;
            int rectanglesCurrentIndex = 0;
            int polygonsCurrentIndex = 0;
            int bezierShapesCurrentIndex = 0;
            int imagesCurrentIndex = 0;

            foreach (GraphicObject graphicObject in addedGraphicObjects)
            {
                switch (graphicObject)
                {
                    case GraphicObject.Circle:
                        circles[circlesCurrentIndex].Draw(graphics);
                        circlesCurrentIndex++;
                        break;
                    case GraphicObject.Rectangle:
                        rectangles[rectanglesCurrentIndex].Draw(graphics);
                        rectanglesCurrentIndex++;
                        break;
                    case GraphicObject.Polygon:
                        polygons[polygonsCurrentIndex].Draw(graphics);
                        polygonsCurrentIndex++;
                        break;
                    case GraphicObject.BezierShape:
                        bezierShapes[bezierShapesCurrentIndex].Draw(graphics);
                        bezierShapesCurrentIndex++;
                        break;
                    case GraphicObject.Image:
                        images[imagesCurrentIndex].Display(graphics);
                        imagesCurrentIndex++;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(graphicObject), graphicObject, null);
                }
            }
        }

        public void AddCircle(Circle circle)
        {
            circles.Add(circle);
            addedGraphicObjects.Add(GraphicObject.Circle);
        }

        public void AddRectangle(Rectangle rectangle)
        {
            rectangles.Add(rectangle);
            addedGraphicObjects.Add(GraphicObject.Rectangle);
        }

        public void AddPolygon(Polygon polygon)
        {
            polygons.Add(polygon);
            addedGraphicObjects.Add(GraphicObject.Polygon);
        }

        public void AddBezierShape(BezierShape bezierShape)
        {
            bezierShapes.Add(bezierShape);
            addedGraphicObjects.Add(GraphicObject.BezierShape);
        }

        public void AddDrawingImage(DrawingImage drawingImage)
        {
            images.Add(drawingImage);
            addedGraphicObjects.Add(GraphicObject.Image);
        }

        public void RemoveLastAddedGraphicObject()
        {
            if (!addedGraphicObjects.Any())
                return;

            switch (addedGraphicObjects.Last())
            {
                case GraphicObject.Circle:
                    circles.RemoveLast();
                    break;
                case GraphicObject.Rectangle:
                    rectangles.RemoveLast();
                    break;
                case GraphicObject.Polygon:
                    polygons.RemoveLast();
                    break;
                case GraphicObject.BezierShape:
                    bezierShapes.RemoveLast();
                    break;
                case GraphicObject.Image:
                    images.RemoveLast();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(null);
            }

            addedGraphicObjects.RemoveLast();
        }

        public void ClearGraphicObjects()
        {
            circles.Clear();
            rectangles.Clear();
            polygons.Clear();
            bezierShapes.Clear();
            images.Clear();
            addedGraphicObjects.Clear();
        }

        public void MoveLastAddedGraphicObject(MoveDirrection dirrection, int moveRange)
        {
            if (!addedGraphicObjects.Any())
                return;

            switch (addedGraphicObjects.Last())
            {
                case GraphicObject.Circle:
                    circles.Last().Move(dirrection, moveRange);
                    break;
                case GraphicObject.Rectangle:
                    rectangles.Last().Move(dirrection, moveRange);
                    break;
                case GraphicObject.Polygon:
                    polygons.Last().Move(dirrection, moveRange);
                    break;
                case GraphicObject.BezierShape:
                    bezierShapes.Last().Move(dirrection, moveRange);
                    break;
                case GraphicObject.Image:
                    images.Last().Move(dirrection, moveRange);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(null);
            }
        }

        public void RotateClockwiseLastAddedGraphicObject(double angle)
        {
            if (!addedGraphicObjects.Any())
                return;

            switch (addedGraphicObjects.Last())
            {
                case GraphicObject.Polygon:
                    polygons.Last().RotateClockwise(angle);
                    break;
                case GraphicObject.BezierShape:
                    bezierShapes.Last().RotateClockwise(angle);
                    break;
            }
        }
    }
}
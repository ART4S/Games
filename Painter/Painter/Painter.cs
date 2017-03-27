using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using Paint.GraphicObjects;

namespace Paint
{
    public class Painter
    {
        private readonly List<GraphicObject> graphicObjects;

        public Painter()
        {
            graphicObjects = new List<GraphicObject>();
        }

        public void AddGraphicObject(GraphicObject graphicObject)
        {
            graphicObjects.Add(graphicObject);
        }

        public void DrawGraphicObjects(Graphics graphics)
        {
            foreach (GraphicObject graphicObject in graphicObjects)
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

            Curve curve = graphicObjects.Last() as Curve;

            curve?.AddPoint(point);
        }

        public Bitmap ToBitmap(int bitmapWidth, int bitmapHeight)
        {
            Bitmap bitmap = new Bitmap(bitmapWidth, bitmapHeight);
            Graphics bitmapGraphics = Graphics.FromImage(bitmap);
            RectangleF bitmapRectangle = new RectangleF(new PointF(0, 0), new SizeF(bitmapWidth, bitmapHeight));
            Brush whiteBrush = new SolidBrush(Color.White);

            bitmapGraphics.SmoothingMode = SmoothingMode.AntiAlias;

            bitmapGraphics.FillRectangle(whiteBrush, bitmapRectangle);

            DrawGraphicObjects(bitmapGraphics);

            return bitmap;
        }
    }
}
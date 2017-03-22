using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Painter.Properties;

namespace Painter
{
    public partial class DrawingForm : Form
    {
        private readonly ShapesPainter shapesPainter;
        private Image selectedImageForFilling;

        private Shape currentShape;
        private DrawingState drawingState;

        private Point selectedFirstPoint;
        private Point selectedSecondPoint;
        private Point cursorPoint;

        public DrawingForm()
        {
            InitializeComponent();

            shapesPainter = new ShapesPainter();
            selectedImageForFilling = new Bitmap(patternsList.Images[0]);
            currentShape = Shape.Circle;
            drawingState = DrawingState.Waiting;

            cursorPoint = new Point(0, 0);
            selectedFirstPoint = new Point(0, 0);
            selectedSecondPoint = new Point(0, 0);

            BringToFontDrawingFormElements();
            AddPatternsInPatternsListView();
        }

        private void BringToFontDrawingFormElements()
        {
            patternsListView.BringToFront();
            menuStrip.BringToFront();
            toolStrip.BringToFront();
        }

        private void AddPatternsInPatternsListView()
        {
            foreach (var namePattern in patternsList.Images.Keys)
            {
                ListViewItem newItem = new ListViewItem
                {
                    Text = namePattern.Split('.').First(),
                    ImageKey = namePattern
                };

                patternsListView.Items.Add(newItem);
            }
        }

        // изменить currentShape
        private void drawCircleButton_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Circle;
        }

        private void drawRectangleButton_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Rectangle;
        }

        private void drawPolygonButton_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Polygon;
        }

        private void drawBezierShapeButton_Click(object sender, EventArgs e)
        {
            currentShape = Shape.Bezier;
        }

        // рисовние фигруы на доске
        private void drawingPictureBox_Paint(object sender, PaintEventArgs e)
        {
            shapesPainter.DrawShapes(e.Graphics);

            if (drawingState == DrawingState.Drawing)
                e.Graphics.DrawLine(new Pen(Color.Black, 3), selectedFirstPoint, cursorPoint);
        }

        private void drawingPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawingState == DrawingState.Drawing)
                return;

            drawingState = DrawingState.Drawing;
            selectedFirstPoint = cursorPoint;
        }

        private void drawingPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawingState == DrawingState.Waiting)
                return;

            drawingState = DrawingState.Waiting;
            selectedSecondPoint = cursorPoint;
            AddCurrentShapeInShapesPainter();
        }

        private void AddCurrentShapeInShapesPainter()
        {
            TextureBrush textureBrush = new TextureBrush(selectedImageForFilling);
            Pen pen = new Pen(Color.Black, 3);

            int distanceBetweenFirstPointAndSecondPoint =
                (int) Math.Sqrt(Math.Pow(selectedFirstPoint.X - selectedSecondPoint.X, 2)
                + Math.Pow(selectedFirstPoint.Y - selectedSecondPoint.Y, 2));

            switch (currentShape)
            {
                case Shape.Circle:
                    shapesPainter.AddCircle(
                        new Circle(
                            selectedFirstPoint,
                            distanceBetweenFirstPointAndSecondPoint,
                            pen,
                            textureBrush));
                    break;

                case Shape.Rectangle:
                    shapesPainter.AddRectangle(
                        new Rectangle(
                            selectedFirstPoint,
                            2 * distanceBetweenFirstPointAndSecondPoint,
                            2 * distanceBetweenFirstPointAndSecondPoint,
                            pen,
                            textureBrush));
                    break;

                case Shape.Polygon:
                    shapesPainter.AddPolygon(GetPolygon12(selectedFirstPoint, pen, textureBrush));
                    break;

                case Shape.Bezier:
                    shapesPainter.AddBezierShape(GetBezierShape12(selectedFirstPoint, pen, textureBrush));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(currentShape), currentShape, null);
            }
        }

        // Позиция курсора
        private void drawingPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPoint = new Point(e.X, e.Y);
            drawingPictureBox.Refresh();
        }

        // Очистить экран
        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            ClearDrawingBoard();
        }

        // Отобразить тулбар
        private void toolbarMenuItem_Click(object sender, EventArgs e)
        {
            toolbarMenuItem.Checked = !toolbarMenuItem.Checked;
            toolStrip.Visible = toolbarMenuItem.Checked;
        }

        // Установить выбранный паттерн в качестве заливки
        private void patternsListView_ItemActivate(object sender, EventArgs e)
        {
            if (patternsListView.SelectedItems.Count == 0)
                return;

            selectedImageForFilling = patternsList.Images[patternsListView.SelectedIndices[0]];
        }

        // Сохранение файла
        private void saveAsFIleMenuItem_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void saveFileButton_Click(object sender, EventArgs e)
        {
            SaveFile();
        }

        private void SaveFile()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Ptr format|*.ptr",
                FilterIndex = 0
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // TODO: чоделать
            }
        }

        // Отображение инфы разработчиков
        private void helpButton_Click(object sender, EventArgs e)
        {
            ShowHelpDialog();
        }

        private void aboutPainterMenuItem_Click(object sender, EventArgs e)
        {
            ShowHelpDialog();
        }

        private static void ShowHelpDialog()
        {
            MessageBox.Show(Resources.InfoAboutProgram, "About program");
        }

        // Удалить последнюю нарисованную фигуру
        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLastShape();
        }

        // Клавиши
        private void drawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            if (pressedKey == Keys.Z && e.Control)
                RemoveLastShape();

            if (pressedKey == Keys.Delete)
                ClearDrawingBoard();

            if (drawingState != DrawingState.Waiting)
                return;

            const double rotationAngle = 0.05;
            const int moveRange = 3;
                                             
            switch (pressedKey)
            {
                case Keys.Up:
                    shapesPainter.MoveLastShape(MoveDirrection.Up, moveRange);
                    break;
                case Keys.Down:
                    shapesPainter.MoveLastShape(MoveDirrection.Down, moveRange);
                    break;
                case Keys.Left:
                    shapesPainter.MoveLastShape(MoveDirrection.Left, moveRange);
                    break;
                case Keys.Right:
                    shapesPainter.MoveLastShape(MoveDirrection.Right, moveRange);
                    break;
                case Keys.Q:
                    shapesPainter.RotateClockwiseLastShape(-rotationAngle);
                    break;
                case Keys.E:
                    shapesPainter.RotateClockwiseLastShape(rotationAngle);
                    break;
            }

            drawingPictureBox.Refresh();
        }

        private void RemoveLastShape()
        {
            shapesPainter.RemoveLastShape();
            drawingPictureBox.Refresh();
        }

        private void ClearDrawingBoard()
        {
            shapesPainter.ClearShapes();
            drawingPictureBox.Refresh();
        }

        private void pagePropertyMenuItem_Click(object sender, EventArgs e)
        {
            new SettingSizePictureBoxForm(drawingPictureBox).ShowDialog();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // полигон и полигон через кривые безье 12-го варианта

        private static Polygon GetPolygon12(Point middlePoint, Pen pen, TextureBrush textureBrush)
        {
            PointF[] polygonPoints =
            {
                new PointF(middlePoint.X - 10, middlePoint.Y + 10),
                new PointF(middlePoint.X - 20, middlePoint.Y + 20),
                new PointF(middlePoint.X - 20, middlePoint.Y - 20),
                new PointF(middlePoint.X + 20, middlePoint.Y - 20),
                new PointF(middlePoint.X + 10, middlePoint.Y - 10),
                new PointF(middlePoint.X + 50, middlePoint.Y + 30),
                new PointF(middlePoint.X + 30, middlePoint.Y + 50)
            };

            return new Polygon(polygonPoints, middlePoint, pen, textureBrush);
        }

        private static BezierShape GetBezierShape12(Point middlePoint, Pen pen, TextureBrush textureBrush)
        {
            List<BezierCurve> curves = new List<BezierCurve>
            {
                new BezierCurve(
                    new PointF(middlePoint.X - 10, middlePoint.Y + 10),
                    new PointF(middlePoint.X - 20, middlePoint.Y + 20),
                    new PointF(middlePoint.X - 15, middlePoint.Y + 15),
                    new PointF(middlePoint.X - 15, middlePoint.Y + 15)),

                new BezierCurve(
                    new PointF(middlePoint.X - 20, middlePoint.Y + 20),
                    new PointF(middlePoint.X - 20, middlePoint.Y - 20),
                    new PointF(middlePoint.X - 20, middlePoint.Y),
                    new PointF(middlePoint.X - 20, middlePoint.Y)),

                new BezierCurve(                
                    new PointF(middlePoint.X - 20, middlePoint.Y - 20),
                    new PointF(middlePoint.X + 20, middlePoint.Y - 20),
                    new PointF(middlePoint.X, middlePoint.Y - 20),
                    new PointF(middlePoint.X, middlePoint.Y - 20)),

                new BezierCurve(
                    new PointF(middlePoint.X + 20, middlePoint.Y - 20),
                    new PointF(middlePoint.X + 10, middlePoint.Y - 10),
                    new PointF(middlePoint.X + 15, middlePoint.Y - 15),
                    new PointF(middlePoint.X + 15, middlePoint.Y - 15)),

                new BezierCurve(                
                    new PointF(middlePoint.X + 10, middlePoint.Y - 10),
                    new PointF(middlePoint.X + 50, middlePoint.Y + 30),
                    new PointF(middlePoint.X, middlePoint.Y),
                    new PointF(middlePoint.X, middlePoint.Y)),

                new BezierCurve(                
                    new PointF(middlePoint.X + 50, middlePoint.Y + 30),
                    new PointF(middlePoint.X + 30, middlePoint.Y + 50),
                    new PointF(middlePoint.X + 30, middlePoint.Y + 30),
                    new PointF(middlePoint.X + 30, middlePoint.Y + 30)),

                new BezierCurve(                
                    new PointF(middlePoint.X + 30, middlePoint.Y + 50),
                    new PointF(middlePoint.X - 10, middlePoint.Y + 10),
                    new PointF(middlePoint.X, middlePoint.Y),
                    new PointF(middlePoint.X, middlePoint.Y))
            };

            return new BezierShape(curves, middlePoint, pen, textureBrush);
        }
    }
}

using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Paint.Properties;

namespace Paint
{
    public partial class DrawingForm : Form
    {
        private readonly Painter painter;
        private Image selectedImageForFilling;

        private GraphicObject selectedGraphicObject;
        private DrawingState drawingState;

        private Point selectedFirstPoint;
        private Point selectedSecondPoint;
        private Point cursorPoint;

        public DrawingForm()
        {
            InitializeComponent();

            painter = new Painter();
            selectedImageForFilling = new Bitmap(patternsList.Images[0]);
            selectedGraphicObject = GraphicObject.Circle;
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

        // изменить selectedGraphicObject
        private void drawCircleButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObject.Circle;
        }

        private void drawRectangleButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObject.Rectangle;
        }

        private void drawPolygonButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObject.Polygon;
        }

        private void drawBezierShapeButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObject.BezierShape;
        }

        private void drawImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Resources.ImageFilterPattern
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = new Bitmap(openFileDialog.FileName);

                painter.AddGraphicObject(new DrawingImage(selectedImage, selectedFirstPoint));

                drawingPictureBox.Refresh();
            }
        }

        // рисовние фигруы на доске
        private void drawingPictureBox_Paint(object sender, PaintEventArgs e)
        {
            painter.DrawGraphicObjects(e.Graphics);

            if (drawingState == DrawingState.ShapeDrawing)
                e.Graphics.DrawLine(new Pen(Color.Black, 3), selectedFirstPoint, cursorPoint);
        }

        private void drawingPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (drawingState == DrawingState.ShapeDrawing)
                return;

            drawingState = DrawingState.ShapeDrawing;
            selectedFirstPoint = cursorPoint;
        }

        private void drawingPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (drawingState == DrawingState.Waiting)
                return;

            drawingState = DrawingState.Waiting;
            selectedSecondPoint = cursorPoint;
            AddSelectedGraphicObjectInPainter();
        }

        private void AddSelectedGraphicObjectInPainter()
        {
            TextureBrush textureBrush = new TextureBrush(selectedImageForFilling);
            Pen pen = new Pen(Color.Black, 3);

            int distanceBetweenFirstPointAndSecondPoint =
                (int) Math.Sqrt(Math.Pow(selectedFirstPoint.X - selectedSecondPoint.X, 2)
                              + Math.Pow(selectedFirstPoint.Y - selectedSecondPoint.Y, 2));

            switch (selectedGraphicObject)
            {
                case GraphicObject.Circle:
                    painter.AddGraphicObject(
                        new Circle(
                            selectedFirstPoint,
                            distanceBetweenFirstPointAndSecondPoint,
                            pen,
                            textureBrush));
                    break;

                case GraphicObject.Rectangle:
                    painter.AddGraphicObject(
                        new Rectangle(
                            selectedFirstPoint,
                            2 * distanceBetweenFirstPointAndSecondPoint,
                            2 * distanceBetweenFirstPointAndSecondPoint,
                            pen,
                            textureBrush));
                    break;

                case GraphicObject.Polygon:
                    painter.AddGraphicObject(GetPolygon12(selectedFirstPoint, pen, textureBrush));
                    break;

                case GraphicObject.BezierShape:
                    painter.AddGraphicObject(GetBezierShape12(selectedFirstPoint, pen));
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedGraphicObject), selectedGraphicObject, null);
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

        private static void SaveFile()
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

        private void ShowHelpDialog()
        {
            MessageBox.Show(Resources.InfoAboutProgram, "About program");
        }

        // Удалить последнюю нарисованную фигуру
        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLastAddedGraphicObject();
        }

        // Клавиши
        private void drawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            if (pressedKey == Keys.Z && e.Control)
                RemoveLastAddedGraphicObject();

            if (pressedKey == Keys.Delete)
                ClearDrawingBoard();

            if (drawingState != DrawingState.Waiting)
                return;

            const double rotationAngle = 0.05;
            const int moveRange = 3;
                                             
            switch (pressedKey)
            {
                case Keys.Up:
                    painter.MoveLastAddedGraphicObject(MoveDirrection.Up, moveRange);
                    break;
                case Keys.Down:
                    painter.MoveLastAddedGraphicObject(MoveDirrection.Down, moveRange);
                    break;
                case Keys.Left:
                    painter.MoveLastAddedGraphicObject(MoveDirrection.Left, moveRange);
                    break;
                case Keys.Right:
                    painter.MoveLastAddedGraphicObject(MoveDirrection.Right, moveRange);
                    break;
                case Keys.Q:
                    painter.RotateClockwiseLastAddedGraphicObject(-rotationAngle);
                    break;
                case Keys.E:
                    painter.RotateClockwiseLastAddedGraphicObject(rotationAngle);
                    break;
            }

            drawingPictureBox.Refresh();
        }

        private void RemoveLastAddedGraphicObject()
        {
            painter.RemoveLastAddedGraphicObject();
            drawingPictureBox.Refresh();
        }

        private void ClearDrawingBoard()
        {
            painter.ClearGraphicObjects();
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

        // 12-й вариант
        // полигон и фигура, построенная из кривых безье

        private Polygon GetPolygon12(Point middlePoint, Pen pen, TextureBrush textureBrush)
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

        private BezierShape GetBezierShape12(Point middlePoint, Pen pen)
        {
            BezierCurve[] curves =
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

            return new BezierShape(curves, middlePoint, pen);
        }
    }
}

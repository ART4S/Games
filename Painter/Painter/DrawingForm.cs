using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Paint.GraphicObjects;
using Paint.Properties;
using Rectangle = Paint.GraphicObjects.Rectangle;

namespace Paint
{
    public partial class DrawingForm : Form
    {
        private readonly Painter painter;
        private Image selectedImageForFilling;

        private GraphicObject selectedGraphicObject;
        private MouseState mouseState;
        private PenSize selectedPenSize;

        private Color selectedColor;

        private Point selectedFirstPoint;
        private Point selectedSecondPoint;
        private Point cursorPoint;

        public DrawingForm()
        {
            InitializeComponent();

            painter = new Painter();
            selectedImageForFilling = new Bitmap(patternsList.Images[0]);
            selectedPenSize = PenSize.Little;
            selectedGraphicObject = GraphicObject.Empty;
            mouseState = MouseState.MouseKeyDepressed;

            selectedColor = Color.Black;

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

        private void drawCurveButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObject.Curve;
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

        // изменить selectedPenSize
        private void littlePenSizeMenuItem_Click(object sender, EventArgs e)
        {
            selectedPenSize = PenSize.Little;
        }

        private void averagePenSizeMenuItem_Click(object sender, EventArgs e)
        {
            selectedPenSize = PenSize.Average;
        }

        private void bigPenSizeMenuItem_Click(object sender, EventArgs e)
        {
            selectedPenSize = PenSize.Big;
        }

        // изменить selectedColor
        private void selectColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog.Color;
                selectColorButton.BackColor = colorDialog.Color;
            }
        }

        // изменить selectedImageForFilling
        private void patternsListView_ItemActivate(object sender, EventArgs e)
        {
            if (patternsListView.SelectedItems.Count == 0)
                return;

            selectedImageForFilling = patternsList.Images[patternsListView.SelectedIndices[0]];
        }

        // рисовние фигруы на доске
        private void drawingPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            painter.DrawGraphicObjects(e.Graphics);

            if (mouseState == MouseState.MouseKeyPressed
                && (selectedGraphicObject == GraphicObject.Circle
                    || selectedGraphicObject == GraphicObject.Rectangle))
                e.Graphics.DrawLine(new Pen(selectedColor, selectedPenSize.ToInt()), selectedFirstPoint, cursorPoint);
        }

        private void drawingPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseState == MouseState.MouseKeyPressed)
                return;

            selectedFirstPoint = cursorPoint;

            if (selectedGraphicObject == GraphicObject.Curve)
                AddSelectedGraphicObjectInPainter();

            mouseState = MouseState.MouseKeyPressed;
        }

        private void drawingPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseState == MouseState.MouseKeyDepressed)
                return;

            selectedSecondPoint = cursorPoint;

            if (selectedGraphicObject != GraphicObject.Curve)
                AddSelectedGraphicObjectInPainter();

            mouseState = MouseState.MouseKeyDepressed;
        }

        private void AddSelectedGraphicObjectInPainter()
        {
            TextureBrush textureBrush = new TextureBrush(selectedImageForFilling);
            Pen pen = new Pen(selectedColor, selectedPenSize.ToInt());

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
                case GraphicObject.Curve:
                    painter.AddGraphicObject(new Curve(selectedFirstPoint, selectedFirstPoint, pen));
                    break;
                case GraphicObject.DrawingImage:
                    break;
                case GraphicObject.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedGraphicObject), selectedGraphicObject, null);
            }
        }

        // Позиция курсора
        private void drawingPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPoint = new Point(e.X, e.Y);

            if (mouseState == MouseState.MouseKeyPressed && selectedGraphicObject == GraphicObject.Curve)
                painter.AddPointToLastAddedCurve(cursorPoint);

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

            if (mouseState != MouseState.MouseKeyDepressed)
                return;

            const double rotationAngle = 0.05;
            const int moveRange = 3;
                                             
            switch (pressedKey)
            {
                case Keys.Up:
                    painter.MoveLastAddedGraphicObject(MoveDirection.Up, moveRange);
                    break;
                case Keys.Down:
                    painter.MoveLastAddedGraphicObject(MoveDirection.Down, moveRange);
                    break;
                case Keys.Left:
                    painter.MoveLastAddedGraphicObject(MoveDirection.Left, moveRange);
                    break;
                case Keys.Right:
                    painter.MoveLastAddedGraphicObject(MoveDirection.Right, moveRange);
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
            PointF[] curve =
            {
                new PointF(middlePoint.X - 10, middlePoint.Y + 10),
                new PointF(middlePoint.X - 15, middlePoint.Y + 15),
                new PointF(middlePoint.X - 15, middlePoint.Y + 15),
                new PointF(middlePoint.X - 20, middlePoint.Y + 20),

                new PointF(middlePoint.X - 20, middlePoint.Y + 20),
                new PointF(middlePoint.X - 20, middlePoint.Y),
                new PointF(middlePoint.X - 20, middlePoint.Y),
                new PointF(middlePoint.X - 20, middlePoint.Y - 20),

                new PointF(middlePoint.X - 20, middlePoint.Y - 20),
                new PointF(middlePoint.X, middlePoint.Y - 20),
                new PointF(middlePoint.X, middlePoint.Y - 20),
                new PointF(middlePoint.X + 20, middlePoint.Y - 20),

                new PointF(middlePoint.X + 20, middlePoint.Y - 20),
                new PointF(middlePoint.X + 15, middlePoint.Y - 15),
                new PointF(middlePoint.X + 15, middlePoint.Y - 15),
                new PointF(middlePoint.X + 10, middlePoint.Y - 10),

                new PointF(middlePoint.X + 10, middlePoint.Y - 10),
                new PointF(middlePoint.X, middlePoint.Y),
                new PointF(middlePoint.X, middlePoint.Y),
                new PointF(middlePoint.X + 60, middlePoint.Y + 30),

                new PointF(middlePoint.X + 60, middlePoint.Y + 30),
                new PointF(middlePoint.X + 30, middlePoint.Y + 30),
                new PointF(middlePoint.X + 30, middlePoint.Y + 30),
                new PointF(middlePoint.X + 30, middlePoint.Y + 50),

                new PointF(middlePoint.X + 30, middlePoint.Y + 50),
                new PointF(middlePoint.X, middlePoint.Y),
                new PointF(middlePoint.X, middlePoint.Y),
                new PointF(middlePoint.X - 10, middlePoint.Y + 10)
            };

            return new BezierShape(curve, middlePoint, pen);
        }
    }
}

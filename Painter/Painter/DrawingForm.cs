using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Painter;
using Painter.Properties;

namespace Paint
{
    public partial class drawingForm : Form
    {
        private readonly ShapesPainter shapesPainter;
        private Image selectedImageForFilling;

        private Shape currentShape;
        private DrawingState drawingState;

        private Point selectedFirstPoint;
        private Point selectedSecondPoint;
        private Point cursorPoint;

        public drawingForm()
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
                    shapesPainter.AddCircle(new Circle(selectedFirstPoint,
                        distanceBetweenFirstPointAndSecondPoint,
                        pen,
                        textureBrush));
                    break;

                case Shape.Rectangle:
                    shapesPainter.AddRectangle(new Rectangle(selectedFirstPoint,
                        2 * distanceBetweenFirstPointAndSecondPoint,
                        2 * distanceBetweenFirstPointAndSecondPoint,
                        pen,
                        textureBrush));
                    break;

                case Shape.Polygon:
                    shapesPainter.AddPolygon(Polygon12(pen, textureBrush));
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

        private void ShowHelpDialog()
        {
            MessageBox.Show(Resources.InfoAboutProgram, "About program");
        }

        // Удалить последнюю нарисованную фигуру
        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            EraseLastShape();
        }

        // Клавиши
        private void drawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            Keys pressedKey = e.KeyCode;

            if (pressedKey == Keys.Z && e.Control)
                EraseLastShape();

            if (pressedKey == Keys.Delete)
                ClearDrawingBoard();

            if (drawingState != DrawingState.Waiting)
                return;
                                             
            switch (pressedKey)
            {
                case Keys.Up:
                    shapesPainter.MoveLastShape(MoveDirrection.Up, 3);
                    break;
                case Keys.Down:
                    shapesPainter.MoveLastShape(MoveDirrection.Down, 3);
                    break;
                case Keys.Left:
                    shapesPainter.MoveLastShape(MoveDirrection.Left, 3);
                    break;
                case Keys.Right:
                    shapesPainter.MoveLastShape(MoveDirrection.Right, 3);
                    break;
            }

            drawingPictureBox.Refresh();
        }

        private void EraseLastShape()
        {
            shapesPainter.DeleteLastShape();
            drawingPictureBox.Refresh();
        }

        private void ClearDrawingBoard()
        {
            shapesPainter.ClearShapes();
            drawingPictureBox.Refresh();
        }

        private void pagePropertyMenuItem_Click(object sender, EventArgs e)
        {
            new PagePropertyForm(drawingPictureBox).ShowDialog();
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // полигон 12-го варианта

        private Polygon Polygon12(Pen pen, TextureBrush textureBrush)
        {
            PointF[] points =
            {
                new PointF(selectedFirstPoint.X + 10, selectedFirstPoint.Y - 10),
                new PointF(selectedFirstPoint.X - 10, selectedFirstPoint.Y + 10),
                new PointF(selectedFirstPoint.X - 60, selectedFirstPoint.Y - 40),
                new PointF(selectedFirstPoint.X - 80, selectedFirstPoint.Y - 20),
                new PointF(selectedFirstPoint.X - 80, selectedFirstPoint.Y - 80),
                new PointF(selectedFirstPoint.X - 20, selectedFirstPoint.Y - 80),
                new PointF(selectedFirstPoint.X - 40, selectedFirstPoint.Y - 60)
            };

            return new Polygon(points, pen, textureBrush);
        } 
    }
}

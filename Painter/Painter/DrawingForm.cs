using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Windows.Forms;
using Paint.GraphicObjects;
using Paint.Properties;
using Rectangle = Paint.GraphicObjects.Rectangle;

namespace Paint
{
    public partial class DrawingForm : Form
    {
        private const int DrawingPictureBoxMaxWidth = 1920;
        private const int DrawingPictureBoxMaxHeight = 1080;

        private Painter painter;
        private Image selectedImageForFilling;

        private GraphicObjectType selectedGraphicObject;
        private MouseState mouseState;
        private PenSize selectedPenSize;

        private Color selectedColor;

        private Point selectedFirstPoint;
        private Point selectedSecondPoint;
        private Point cursorPoint;

        private string drawnImageFileName;

        public DrawingForm()
        {
            InitializeComponent();
            SetDefaultValuesForFields();
            AddPatternsInPatternsListView();
        }

        private void SetDefaultValuesForFields()
        {
            painter = new Painter();
            selectedImageForFilling = new Bitmap(patternsList.Images[0]);

            selectedGraphicObject = GraphicObjectType.Empty;
            mouseState = MouseState.MouseKeyDepressed;
            selectedPenSize = PenSize.Little;

            selectedColor = Color.Black;

            cursorPoint = new Point(0, 0);
            selectedFirstPoint = new Point(0, 0);
            selectedSecondPoint = new Point(0, 0);

            drawnImageFileName = string.Empty;
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

        // Изменение selectedColor
        private void selectColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog.Color;
                selectColorButton.BackColor = colorDialog.Color;
            }
        }

        // Изменение selectedPenSize
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

        // Изменение selectedGraphicObject
        private void drawCircleButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObjectType.Circle;
        }

        private void drawRectangleButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObjectType.Rectangle;
        }

        private void drawPolygonButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObjectType.Polygon;
        }

        private void drawBezierShapeButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObjectType.BezierShape;
        }

        private void drawCurveButton_Click(object sender, EventArgs e)
        {
            selectedGraphicObject = GraphicObjectType.Curve;
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

                painter.AddGraphicObject(new DrawingImage(selectedImage, new PointF(0, 0)));

                drawingPictureBox.Refresh();
            }
        }

        // Изменение selectedImageForFilling
        private void patternsListView_ItemActivate(object sender, EventArgs e)
        {
            if (patternsListView.SelectedItems.Count == 0)
                return;

            selectedImageForFilling = patternsList.Images[patternsListView.SelectedIndices[0]];
        }

        // Рисование
        private void drawingPictureBox_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            painter.DrawGraphicObjects(e.Graphics);

            if (mouseState == MouseState.MouseKeyPressed
                && (selectedGraphicObject == GraphicObjectType.Circle
                    || selectedGraphicObject == GraphicObjectType.Rectangle))
                e.Graphics.DrawLine(new Pen(selectedColor, selectedPenSize.ToInt()), selectedFirstPoint, cursorPoint);
        }

        private void drawingPictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (mouseState == MouseState.MouseKeyPressed)
                return;

            selectedFirstPoint = cursorPoint;

            if (selectedGraphicObject == GraphicObjectType.Curve)
                AddSelectedGraphicObjectInPainter();

            mouseState = MouseState.MouseKeyPressed;
        }

        private void drawingPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseState == MouseState.MouseKeyDepressed)
                return;

            selectedSecondPoint = cursorPoint;

            if (selectedGraphicObject != GraphicObjectType.Curve)
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
                case GraphicObjectType.Circle:
                    painter.AddGraphicObject(
                        new Circle(
                            selectedFirstPoint,
                            distanceBetweenFirstPointAndSecondPoint,
                            pen,
                            textureBrush));
                    break;

                case GraphicObjectType.Rectangle:
                    painter.AddGraphicObject(
                        new Rectangle(
                            selectedFirstPoint,
                            2 * distanceBetweenFirstPointAndSecondPoint,
                            2 * distanceBetweenFirstPointAndSecondPoint,
                            pen,
                            textureBrush));
                    break;

                case GraphicObjectType.Curve:
                    painter.AddGraphicObject(
                        new Curve(
                            selectedFirstPoint,
                            selectedFirstPoint,
                            pen));
                    break;

                case GraphicObjectType.Polygon:
                    painter.AddGraphicObject(GetPolygon12(selectedFirstPoint, pen, textureBrush));
                    break;

                case GraphicObjectType.BezierShape:
                    painter.AddGraphicObject(GetBezierShape12(selectedFirstPoint, pen));
                    break;
                case GraphicObjectType.DrawingImage:
                    break;
                case GraphicObjectType.Empty:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(selectedGraphicObject), selectedGraphicObject, null);
            }
        }

        // Изменение cursorPoint
        private void drawingPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPoint = new Point(e.X, e.Y);

            if (mouseState == MouseState.MouseKeyPressed && selectedGraphicObject == GraphicObjectType.Curve)
                painter.AddPointToLastAddedCurve(cursorPoint);

            drawingPictureBox.Refresh();
        }

        // Отображение тулбара
        private void toolbarMenuItem_Click(object sender, EventArgs e)
        {
            toolbarMenuItem.Checked = !toolbarMenuItem.Checked;
            toolStrip.Visible = toolbarMenuItem.Checked;
        }

        // Оторажение панели паттернов
        private void patternsPanelMenuItem_Click(object sender, EventArgs e)
        {
            patternsPanelMenuItem.Checked = !patternsPanelMenuItem.Checked;
            patternsListView.Visible = patternsPanelMenuItem.Checked;
        }

        // Отображение инструкции к программе
        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.HelpText, @"Help");
        }

        // Отображение информации о программе и разработчиках
        private void aboutPainterMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.InfoAboutProgram, @"About program");
        }

        // Удаление последнего нарисованного графического объекта
        private void undoMenuItem_Click(object sender, EventArgs e)
        {
            RemoveLastAddedGraphicObject();
        }

        // Очистка экрана
        private void clearMenuItem_Click(object sender, EventArgs e)
        {
            ClearDrawingBoard();
        }

        // Настройка размера drawingPictureBox
        private void pagePropertyMenuItem_Click(object sender, EventArgs e)
        {
            var settingSizeDrawingPictureBoxDialog = new SettingSizePictureBoxForm(
                drawingPictureBox,
                DrawingPictureBoxMaxWidth,
                DrawingPictureBoxMaxHeight);

            settingSizeDrawingPictureBoxDialog.ShowDialog();
        }

        // Выход
        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Нажатие на клавиши
        private void drawingForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (mouseState != MouseState.MouseKeyDepressed)
                return;

            Keys pressedKey = e.KeyCode;

            if (pressedKey == Keys.Z && e.Control)
                RemoveLastAddedGraphicObject();

            if (pressedKey == Keys.Delete)
                ClearDrawingBoard();

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

        // Сохранение изображения
        private void saveFileButton_Click(object sender, EventArgs e)
        {
            FastSaveDrawnImage();
        }

        private void saveFileMenuItem_Click(object sender, EventArgs e)
        {
            FastSaveDrawnImage();
        }

        private void pngMenuItem_Click(object sender, EventArgs e)
        {
            SaveDrawnImageToFormat(ImageFormat.Png);
        }

        private void jpegMenuItem_Click(object sender, EventArgs e)
        {
            SaveDrawnImageToFormat(ImageFormat.Jpeg);
        }

        private void bmpMenuItem_Click(object sender, EventArgs e)
        {
            SaveDrawnImageToFormat(ImageFormat.Bmp);
        }

        private void gifMenuItem_Click(object sender, EventArgs e)
        {
            SaveDrawnImageToFormat(ImageFormat.Gif);
        }

        private void FastSaveDrawnImage()
        {
            Bitmap resultImage = painter.ToBitmap(drawingPictureBox.Width, drawingPictureBox.Height);

            if (drawnImageFileName != string.Empty)
                resultImage.Save(drawnImageFileName);
            else
                SaveDrawnImageToFormat(ImageFormat.Png);
        }

        private void SaveDrawnImageToFormat(ImageFormat imageFormat)
        {
            Bitmap resultImage = painter.ToBitmap(drawingPictureBox.Width, drawingPictureBox.Height);

            if (TrySetDrawnImageFileNameViaDialogBox(imageFormat))
                resultImage.Save(drawnImageFileName);
        }

        private bool TrySetDrawnImageFileNameViaDialogBox(ImageFormat imageFormat)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Image." + imageFormat
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    drawnImageFileName = saveFileDialog.FileName;
                    return true;
                }
                catch
                {
                    MessageBox.Show(Resources.savingImageErrorText, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return false;
        }

        // Создание нового файла
        private void newFileMenuItem_Click(object sender, EventArgs e)
        {
            if (TrySaveChanges())
                SetDefaultValuesForFields();
        }

        private void newFileButton_Click(object sender, EventArgs e)
        {
            if (TrySaveChanges())
                SetDefaultValuesForFields();
        }

        private bool TrySaveChanges()
        {
            DialogResult saveChangesDialogResult = MessageBox.Show(Resources.saveChangesText, Resources.programName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (saveChangesDialogResult)
            {
                case DialogResult.Yes:
                    FastSaveDrawnImage();
                    return true;
                case DialogResult.No:
                    return true;
                case DialogResult.Cancel:
                    return false;
                default:
                    throw new ArgumentOutOfRangeException(nameof(saveChangesDialogResult), saveChangesDialogResult, null);
            }
        }

        // Открытие файла изображения в программе
        private void openFileButton_Click(object sender, EventArgs e)
        {
            SaveChangesAndOpenSelectedFileInProgram();
        }

        private void openFileMenuItem_Click(object sender, EventArgs e)
        {
            SaveChangesAndOpenSelectedFileInProgram();
        }

        private void SaveChangesAndOpenSelectedFileInProgram()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Resources.ImageFilterPattern
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK && TrySaveChanges())
            {
                Image selectedImage = new Bitmap(openFileDialog.FileName);

                SetDefaultValuesForFields();

                painter.AddGraphicObject(new DrawingImage(selectedImage, new PointF(0, 0)));

                drawnImageFileName = openFileDialog.FileName;

                bool isSelectedImageSizeExceedsDrawingBoardMaxSize = selectedImage.Width > DrawingPictureBoxMaxWidth ||
                                                                        selectedImage.Height > DrawingPictureBoxMaxHeight;

                drawingPictureBox.Size = isSelectedImageSizeExceedsDrawingBoardMaxSize ? new Size(DrawingPictureBoxMaxWidth, DrawingPictureBoxMaxHeight) : selectedImage.Size;

                drawingPictureBox.Refresh();
            }
        }

        // Окно выбора сохранения нарисованной картинки при закрытии приложения
        private void DrawingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // если пользователь нажал "Отмена", TrySaveChanges() вернёт false, значит сигнал на отмену закрытия приложения нужно ставить true
            e.Cancel = !TrySaveChanges();
        }

        // Фигуры для 12-го варианта

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

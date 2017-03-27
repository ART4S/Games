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

        private string finalImageSavingPath;

        public DrawingForm()
        {
            InitializeComponent();
            SetDefaultValuesForFields();
            AddPatternsInPatternsListView();
            SetChangeSelectedPenSizeThroughMouseClickOnButton();
            SetChangeSelectedGraphicObjectThroughMouseClickOnButton();

            helpButton.Click += (sender, e) => MessageBox.Show(Resources.HelpText, "Help");
            aboutPainterMenuItem.Click += (sender, e) => MessageBox.Show(Resources.InfoAboutProgram, "About program");
        }

        private void SetDefaultValuesForFields()
        {
            painter = new Painter();
            selectedImageForFilling = new Bitmap(patternsList.Images[0]);

            selectedGraphicObject = GraphicObjectType.Empty;
            mouseState = MouseState.MouseKeyDepressed;
            selectedPenSize = PenSize.Little;

            selectedColor = Color.Black;
            selectColorButton.BackColor = Color.Black;

            cursorPoint = new Point(0, 0);
            selectedFirstPoint = new Point(0, 0);
            selectedSecondPoint = new Point(0, 0);

            finalImageSavingPath = string.Empty;
        }

        private void AddPatternsInPatternsListView()
        {
            foreach (string patternName in patternsList.Images.Keys)
            {
                ListViewItem newItem = new ListViewItem
                {
                    Text = patternName.Split('.').First(),
                    ImageKey = patternName
                };

                patternsListView.Items.Add(newItem);
            }
        }

        private void SetChangeSelectedPenSizeThroughMouseClickOnButton()
        {
            littlePenSizeMenuItem.Click  += (sender, e) => selectedPenSize = PenSize.Little;
            averagePenSizeMenuItem.Click += (sender, e) => selectedPenSize = PenSize.Average;
            bigPenSizeMenuItem.Click     += (sender, e) => selectedPenSize = PenSize.Big;
        }

        private void SetChangeSelectedGraphicObjectThroughMouseClickOnButton()
        {
            drawCircleButton.Click += (sender, e) => selectedGraphicObject = GraphicObjectType.Circle;
            drawRectangleButton.Click += (sender, e) => selectedGraphicObject = GraphicObjectType.Rectangle;
            drawPolygonButton.Click += (sender, e) => selectedGraphicObject = GraphicObjectType.Polygon;
            drawBezierShapeButton.Click += (sender, e) => selectedGraphicObject = GraphicObjectType.BezierShape;
            drawCurveButton.Click += (sender, e) => selectedGraphicObject = GraphicObjectType.Curve;
            drawImageButton.Click += (sender, e) => SelectImageViaOpenDialogBoxAndAddToPainter();
        }

        private void SelectImageViaOpenDialogBoxAndAddToPainter()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Resources.ImageFilterPattern
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Image selectedImage = Image.FromFile(openFileDialog.FileName);

                painter.AddGraphicObject(new DrawingImage(selectedImage, new PointF(0, 0)));

                drawingPictureBox.Refresh();
            }
        }

        // Изменение SelectColor
        private void selectColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                selectedColor = colorDialog.Color;
                selectColorButton.BackColor = colorDialog.Color;
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
                AddSelectedGraphicObjectToPainter();

            mouseState = MouseState.MouseKeyPressed;
        }

        private void drawingPictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (mouseState == MouseState.MouseKeyDepressed)
                return;

            selectedSecondPoint = cursorPoint;

            if (selectedGraphicObject != GraphicObjectType.Curve)
                AddSelectedGraphicObjectToPainter();

            mouseState = MouseState.MouseKeyDepressed;
        }

        private void AddSelectedGraphicObjectToPainter()
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

        // Изменение cursorPoint и добавление точек кривой, рисуемой пером, в Painter
        private void drawingPictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            cursorPoint = new Point(e.X, e.Y);

            if (mouseState == MouseState.MouseKeyPressed && selectedGraphicObject == GraphicObjectType.Curve)
                painter.AddPointToLastAddedCurve(cursorPoint);

            drawingPictureBox.Refresh();
        }

        // Скрытие/отображение тулбара
        private void toolbarMenuItem_Click(object sender, EventArgs e)
        {
            toolbarMenuItem.Checked = !toolbarMenuItem.Checked;
            toolStrip.Visible = toolbarMenuItem.Checked;
        }

        // Скрытие/отображение панели паттернов
        private void patternsPanelMenuItem_Click(object sender, EventArgs e)
        {
            patternsPanelMenuItem.Checked = !patternsPanelMenuItem.Checked;
            patternsListView.Visible = patternsPanelMenuItem.Checked;
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

            if (pressedKey == Keys.V && e.Control)
                AddImageToPainterFromClipboardAndResizeDrawingBoard();

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

        private void AddImageToPainterFromClipboardAndResizeDrawingBoard()
        {
            Image imageFromClipboard = Clipboard.GetImage();

            if (imageFromClipboard == null)
                return;

            selectedGraphicObject = GraphicObjectType.DrawingImage;

            painter.AddGraphicObject(new DrawingImage(imageFromClipboard, new PointF(0, 0)));

            drawingPictureBox.Size = imageFromClipboard.Size;

            drawingPictureBox.Refresh();

            Clipboard.Clear();
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
            SaveFinalImage();
        }

        private void saveFileMenuItem_Click(object sender, EventArgs e)
        {
            SaveFinalImage();
        }

        private void saveAsPngMenuItem_Click(object sender, EventArgs e)
        {
            SaveFinalImageToFormat(ImageFormat.Png);
        }

        private void saveAsJpegMenuItem_Click(object sender, EventArgs e)
        {
            SaveFinalImageToFormat(ImageFormat.Jpeg);
        }

        private void saveAsBmpMenuItem_Click(object sender, EventArgs e)
        {
            SaveFinalImageToFormat(ImageFormat.Bmp);
        }

        private void saveAsGifMenuItem_Click(object sender, EventArgs e)
        {
            SaveFinalImageToFormat(ImageFormat.Gif);
        }

        private void SaveFinalImage()
        {
            Bitmap finalImage = painter.ToBitmap(drawingPictureBox.Width, drawingPictureBox.Height);

            if (finalImageSavingPath != string.Empty)
                finalImage.Save(finalImageSavingPath);
            else
                SaveFinalImageToFormat(ImageFormat.Png);
        }

        private void SaveFinalImageToFormat(ImageFormat imageFormat)
        {
            Bitmap finalImage = painter.ToBitmap(drawingPictureBox.Width, drawingPictureBox.Height);

            if (TrySetFinalImageSavingPathViaSaveFileDialog(imageFormat))
                finalImage.Save(finalImageSavingPath);
        }

        private bool TrySetFinalImageSavingPathViaSaveFileDialog(ImageFormat imageFormat)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "Image." + imageFormat
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                finalImageSavingPath = saveFileDialog.FileName;
                return true;
            }

            return false;
        }

        // Создание нового файла
        private void newFileMenuItem_Click(object sender, EventArgs e)
        {
            if (TrySaveChangesViaDialogBox())
                SetDefaultValuesForFields();
        }

        private void newFileButton_Click(object sender, EventArgs e)
        {
            if (TrySaveChangesViaDialogBox())
                SetDefaultValuesForFields();
        }

        private bool TrySaveChangesViaDialogBox()
        {
            DialogResult saveChangesDialogResult = MessageBox.Show(Resources.saveChangesText, Resources.programName, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

            switch (saveChangesDialogResult)
            {
                case DialogResult.Yes:
                    SaveFinalImage();
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
        private void openNewImageButton_Click(object sender, EventArgs e)
        {
            SaveChangesAndOpenNewImageInProgram();
        }

        private void openNewImageMenuItem_Click(object sender, EventArgs e)
        {
            SaveChangesAndOpenNewImageInProgram();
        }

        private void SaveChangesAndOpenNewImageInProgram()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = Resources.ImageFilterPattern
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK && TrySaveChangesViaDialogBox())
            {
                Image newImage = Image.FromFile(openFileDialog.FileName);

                SetDefaultValuesForFields();

                painter.AddGraphicObject(new DrawingImage(newImage, new PointF(0, 0)));

                finalImageSavingPath = openFileDialog.FileName;

                bool isNewImageSizeExceedsDrawingBoardMaxSize = newImage.Width > DrawingPictureBoxMaxWidth ||
                                                                        newImage.Height > DrawingPictureBoxMaxHeight;

                drawingPictureBox.Size = isNewImageSizeExceedsDrawingBoardMaxSize ? new Size(DrawingPictureBoxMaxWidth, DrawingPictureBoxMaxHeight) : newImage.Size;

                drawingPictureBox.Refresh();
            }
        }

        // Окно сохранения нарисованной картинки при закрытии приложения
        private void DrawingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (selectedGraphicObject != GraphicObjectType.Empty)
                e.Cancel = !TrySaveChangesViaDialogBox();
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
                new PointF(middlePoint.X, middlePoint.Y),
                new PointF(middlePoint.X - 20, middlePoint.Y - 50),
                new PointF(middlePoint.X - 20, middlePoint.Y - 50),
                new PointF(middlePoint.X, middlePoint.Y - 100),
                new PointF(middlePoint.X + 5, middlePoint.Y - 125),
                new PointF(middlePoint.X + 5, middlePoint.Y - 125),
                new PointF(middlePoint.X, middlePoint.Y - 150),
                new PointF(middlePoint.X + 20, middlePoint.Y - 150),
                new PointF(middlePoint.X + 20, middlePoint.Y - 150),
                new PointF(middlePoint.X + 40, middlePoint.Y - 150),
                new PointF(middlePoint.X + 35, middlePoint.Y - 125),
                new PointF(middlePoint.X + 35, middlePoint.Y - 125),
                new PointF(middlePoint.X + 40, middlePoint.Y - 100),
                new PointF(middlePoint.X + 60, middlePoint.Y - 50),
                new PointF(middlePoint.X + 60, middlePoint.Y - 50),
                new PointF(middlePoint.X + 40, middlePoint.Y),
                new PointF(middlePoint.X + 20, middlePoint.Y),
                new PointF(middlePoint.X + 20, middlePoint.Y),
                new PointF(middlePoint.X, middlePoint.Y)
            };

            return new BezierShape(curve, middlePoint, pen);
        }
    }
}

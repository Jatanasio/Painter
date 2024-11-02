namespace main;

public partial class Form1 : Form
{
    private Bitmap canvas;
    private readonly DrawingTool drawingTool;
    private readonly ErasingTool erasingTool;
    private readonly Customize customize;
    private string drawingsFolderPath = @"drawings";
    private ListBox drawingsListBox;

    public Form1()
    {
        InitializeComponent(); //initialize the page

        customize = new Customize();
        canvas = new Bitmap(ClientSize.Width, ClientSize.Height);

        if (!Directory.Exists(drawingsFolderPath))
        {
            Directory.CreateDirectory(drawingsFolderPath);
        }

        TrackBar opacitySlider = customize.GetOpacitySlider();
        opacitySlider.Location = new Point(10, 10); // Set position
        Controls.Add(opacitySlider);

        TrackBar sizeSlider = customize.GetSizeSlider();
        sizeSlider.Location = new Point(10, 100); // Set position below the color button
        Controls.Add(sizeSlider);

        Button colorButton = new Button
        {
            Text = "Select Color",
            Location = new Point(10, 60)
        };
        colorButton.Click += (s, e) => customize.ShowColorPicker();
        Controls.Add(colorButton);

        Button btnSave = new Button
        {
            Text = "Save",
            Location = new Point(10, 400)
        };
        btnSave.Click += new EventHandler(btnSave_Click);
        this.Controls.Add(btnSave);

        Button btnLoad = new Button
        {
            Text = "Load",
            Location = new Point(100, 400)
        };
        btnLoad.Click += new EventHandler(btnLoad_Click);
        this.Controls.Add(btnLoad);

        drawingTool = new DrawingTool(Color.Black, 2); //default color and width of pen
        erasingTool = new ErasingTool(Color.White, 5); //eraser

        this.MouseDown += Form1_MouseDown;
        this.MouseMove += Form1_MouseMove;
        this.MouseUp += Form1_MouseUp;
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        e.Graphics.FillRectangle(Brushes.White, e.ClipRectangle);
    }
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e); // Ensures the base class performs any required painting
        e.Graphics.DrawImage(canvas, 0, 0); // Draws the canvas bitmap on the form
    }

    private void UpdateBrushColorAndSize()
    {
        drawingTool.Color = customize.selectedColor;
        drawingTool.Width = customize.brushSize; // Set the brush size
    }
    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
        UpdateBrushColorAndSize();
        drawingTool.MouseDown(e);
        erasingTool.MouseDown(e);
    }

    private void Form1_MouseMove(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                drawingTool.MouseMove(e, g);
            }
            Invalidate(); // Redraws the form with the updated canvas
        }
        if (e.Button == MouseButtons.Right)
        {
            using (Graphics g = Graphics.FromImage(canvas))
            {
                erasingTool.MouseMove(e, g);
            }
            Invalidate(); // Redraws the form with the updated canvas
        }
    }

    private void Form1_MouseUp(object sender, MouseEventArgs e)
    {
        drawingTool.MouseUp(e);
        erasingTool.MouseUp(e);
    }

    private void UpdateBrushColor()
    {
        // Update the brush color with selected color and opacity
        Color currentBrushColor = Color.FromArgb(customize.brushOpacity, customize.selectedColor);
        drawingTool.Color = currentBrushColor;
    }


    private void btnSave_Click(object sender, EventArgs e)
    {
        using (SaveFileDialog saveDialog = new SaveFileDialog())
        {
            saveDialog.InitialDirectory = Path.GetFullPath(drawingsFolderPath); // Start in the drawings folder
            saveDialog.Filter = "PNG Image|*.png";
            saveDialog.Title = "Save Drawing As";
            saveDialog.FileName = "drawing.png"; // Default file name

            if (saveDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    canvas.Save(saveDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    MessageBox.Show("Drawing saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDrawingList(); // Refresh the list of files
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to save the drawing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

    private void btnLoad_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog openDialog = new OpenFileDialog())
        {
            openDialog.InitialDirectory = Path.GetFullPath(drawingsFolderPath); // Start in the drawings folder
            openDialog.Filter = "PNG Image|*.png";
            openDialog.Title = "Load Drawing";

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Load the image from the selected file
                    Bitmap loadedBitmap = (Bitmap)Image.FromFile(openDialog.FileName);

                    // Dispose of the old canvas and assign the new one
                    canvas?.Dispose();
                    canvas = new Bitmap(loadedBitmap);

                    // Trigger a repaint to display the loaded image
                    Invalidate();
                    MessageBox.Show("Drawing loaded successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to load the drawing: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }


    private void LoadDrawingList()
    {
        if (drawingsListBox == null)
        {
            drawingsListBox = new ListBox
            {
                Location = new Point(10, 550),
                Width = 200,
                Height = 100
            };
            Controls.Add(drawingsListBox);
        }

        drawingsListBox.Items.Clear();
        if (Directory.Exists(drawingsFolderPath))
        {
            string[] files = Directory.GetFiles(drawingsFolderPath, "*.png");
            foreach (string file in files)
            {
                drawingsListBox.Items.Add(Path.GetFileName(file));
            }
        }
    }
}
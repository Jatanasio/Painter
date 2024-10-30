namespace main;

public partial class Form1 : Form
{
    private readonly Bitmap canvas;
    private readonly DrawingTool drawingTool;
    private readonly ErasingTool erasingTool;
    public Form1()
    {
        InitializeComponent();
        canvas = new Bitmap(ClientSize.Width, ClientSize.Height);
        drawingTool = new DrawingTool(Color.Black, 2);
        erasingTool = new ErasingTool(Color.White, 5);
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
    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {

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

}


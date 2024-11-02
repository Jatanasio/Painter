public class DrawingTool
{
    private bool isDrawing = false;
    private Point lastPoint;
    private Pen pen;

    public Color Color
    {
        get { return pen.Color; }
        set
        {
            pen.Color = value; // Update the pen color
        }
    }

    public float Width // Add a property for Width
    {
        get { return pen.Width; }
        set
        {
            pen.Width = value; // Update the pen width
        }
    }
    public DrawingTool(Color color, int width)
    {
        pen = new Pen(color, width);
    }

    public void MouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) // when left mouse button is pressed it will change drawing to true
        {
            isDrawing = true;
            lastPoint = e.Location;
        }
    }

    public void MouseMove(MouseEventArgs e, Graphics g)
    {
        if (isDrawing) // this tracks movement of mouse while drawing
        {
            g.DrawLine(pen, lastPoint, e.Location);
            lastPoint = e.Location;
        }
    }

    public void MouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left) // when left mouse is no longer clicked it will stop drawing
        {
            isDrawing = false;
        }
    }
}

public class ErasingTool
{
    private bool isErasing = false;
    private Pen eraser;
    private Point lastPoint;

    public ErasingTool(Color color, int width)
    {
        eraser = new Pen(color, width);
    }

    public void MouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right) // when right mouse button is clicked it will start erasing
        {
            isErasing = true;
            lastPoint = e.Location;
        }
    }

    public void MouseMove(MouseEventArgs e, Graphics g)
    {
        if (isErasing) // tracks eraser movement
        {
            g.DrawLine(eraser, lastPoint, e.Location);
            lastPoint = e.Location;
        }
    }

    public void MouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Right) // when right button is released eraser will stop
        {
            isErasing = false;
        }
    }
}


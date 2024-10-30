public class DrawingTool
{
    private bool isDrawing = false;
    private Point lastPoint;
    private Pen pen;

    public DrawingTool(Color color, int width)
    {
        pen = new Pen(color, width);
    }

    public void MouseDown(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = true;
            lastPoint = e.Location;
        }
    }

    public void MouseMove(MouseEventArgs e, Graphics g)
    {
        if (isDrawing)
        {
            g.DrawLine(pen, lastPoint, e.Location);
            lastPoint = e.Location;
        }
    }

    public void MouseUp(MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            isDrawing = false;
        }
    }
}

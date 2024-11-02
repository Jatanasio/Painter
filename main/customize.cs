public class Customize
{
    public Color selectedColor { get; set; } = Color.Black;
    public int brushOpacity { get; set; } = 255;
    public float brushSize { get; set; } = 2.0f; // Default brush size

    private ColorDialog colorDialog;
    private TrackBar opacitySlider;
    private TrackBar sizeSlider; // New slider for brush size

    public Customize()
    {
        colorDialog = new ColorDialog();

        // Opacity slider
        opacitySlider = new TrackBar
        {
            Minimum = 0,
            Maximum = 255,
            Value = 255 // Initial opacity
        };
        opacitySlider.ValueChanged += (s, e) =>
        {
            brushOpacity = opacitySlider.Value;
        };

        // Size slider
        sizeSlider = new TrackBar
        {
            Minimum = 1, // Minimum brush size
            Maximum = 20, // Maximum brush size
            Value = (int)brushSize // Initial brush size
        };
        sizeSlider.ValueChanged += (s, e) =>
        {
            brushSize = sizeSlider.Value; // Update brush size
        };
    }

    public void ShowColorPicker()
    {
        if (colorDialog.ShowDialog() == DialogResult.OK)
        {
            selectedColor = colorDialog.Color;
        }
    }

    public TrackBar GetOpacitySlider()
    {
        return opacitySlider; // To add it to the UI in your form
    }

    public TrackBar GetSizeSlider()
    {
        return sizeSlider; // To add size slider to the UI in your form
    }
}

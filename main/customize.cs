public class Customize
{
    public Color selectedColor {get; set;} = Color.Black;
    public int brushOpacity {get; set;} = 255;

    private ColorDialog colorDialog; 
    private TrackBar opacitySlider;

    public Customize()
    {
        colorDialog = new ColorDialog();

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

}
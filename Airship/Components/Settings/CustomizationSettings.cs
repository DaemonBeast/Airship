using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;

namespace Airship.Components.Settings;

public class CustomizationSettings : UserControl
{
    public const string Title = "Customization";

    public CustomizationSettings()
    {
        var themeSetting = new StackPanel
        {
            Children =
            {
                new TextBlock
                {
                    Text = "Theme",
                    // VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(0d, 0d, 0d, 5d)
                },
                new ComboBox
                {
                    ItemsSource = new[]
                    {
                        "Prefer System Theme",
                        "Light",
                        "Dark"
                    },
                    Width = 200d
                }
            },
            Orientation = Orientation.Vertical,
            Margin = new Thickness(0d, 0d, 0d, 40d)
        };

        Content = new StackPanel
        {
            Children =
            {
                themeSetting
            }
        };
    }
}

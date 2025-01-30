using Avalonia.Controls;

namespace Airship.Components;

public class Sidebar : UserControl
{
    public Sidebar()
    {
        var listBox = new ListBox
        {
            ItemsSource = new[]
            {
                "Home",
                "Not Home",
                "Reactor",
                "Lunch Paid",
                "Settings"
            },
            SelectedIndex = 0
        };

        listBox.SelectionChanged += (_, args) =>
        {
            
        };

        Content = listBox;
    }
}

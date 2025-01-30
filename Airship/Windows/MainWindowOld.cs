using System.Reactive.Linq;
using Airship.Components;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using ReactiveUI;

namespace Airship.Windows;

public class MainWindowOld : Window
{
    public ReactiveProperty<int> Count { get; }

    private readonly Button _button;

    public MainWindowOld()
    {
        Title = "Airship";

        Count = new ReactiveProperty<int>();

        var header = new TextBlock
        {
            Text = "Most bestest awesome mod manager for amogus!!!",
            FontSize = 150,
            TextWrapping = TextWrapping.Wrap
        };

        _button = new Button
        {
            Content = "Press Me"
        };

        var textBlock = new TextBlock
        {
            [!TextBlock.TextProperty] = Count.Select(count => count.ToString()).ToBinding()
        };

        var button = new Button()
        {
            Content = "DOWNLOAD NOW \u2193",
            FontSize = 50,
            Background = new SolidColorBrush(Colors.ForestGreen)
        };

        var stackPanel = new StackPanel
        {
            Children =
            {
                // _button,
                // textBlock,
                header,
                new Sidebar(),
                button
            }
        };

        _button.Click += OnClick;

        Content = stackPanel;
    }

    private void OnClick(object? sender, RoutedEventArgs args)
    {
        Count.Value++;
    }
}

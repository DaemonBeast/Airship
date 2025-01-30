using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Styling;
using FluentAvalonia.Interop;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;

namespace Airship.Components.Settings;

public class Settings
{
    public NavigationView Navigation { get; }

    public ContentDialog Dialog { get; }

    public Settings(AppWindow window)
    {
        // Kinda hacky
        var dialogTranslationY = OSVersionHelper.IsWindows() ? window.TitleBar.Height : 0d;

        Navigation = new NavigationView
        {
            MenuItems =
            {
                new NavigationViewItem
                {
                    Content = CustomizationSettings.Title,
                    IconSource = new SymbolIconSource { Symbol = Symbol.ImageEdit }
                },
                new NavigationViewItem
                {
                    Content = "Home",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Home }
                },
                new NavigationViewItem
                {
                    Content = "Not Home",
                    IconSource = new SymbolIconSource { Symbol = Symbol.HomeFilled }
                },
                new NavigationViewItem
                {
                    Content = "Reactor",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Star }
                },
                new NavigationViewItem
                {
                    Content = "Lunch Paid",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Shop }
                },
                new NavigationViewItem
                {
                    Content = "Settings",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Settings }
                }
            },
            IsSettingsVisible = false,
            ExpandedModeThresholdWidth = 800d,
            CompactModeThresholdWidth = 0d,
            Resources =
            {
                ["NavigationViewContentGridCornerRadius"] = new CornerRadius(0d)
            }
        };

        var dialogTitle = new TextBlock
        {
            Text = "Settings",
            Padding = new Thickness(12d, 11d, 0d, 0d)
        };

        Dialog = new ContentDialog
        {
            Title = dialogTitle,
            Content = Navigation,
            CloseButtonText = "Close",
            RenderTransform = new TranslateTransform(0d, dialogTranslationY),
            Resources =
            {
                ["ContentDialogPadding"] = new Thickness(0d)
            }
        };

        var layoutRootStyle = new Style(
            selector => selector.OfType<ContentDialog>().Descendant().Name("LayoutRoot"))
        {
            Setters = { new Setter(Layoutable.MarginProperty, new Thickness(0d, 0d, 0d, dialogTranslationY)) }
        };

        var titleStyle = new Style(selector => selector.OfType<ContentDialog>().Descendant().Name("Title"))
        {
            Setters = { new Setter(Visual.IsVisibleProperty, false) }
        };

        var commandSpaceStyle = new Style(
            selector => selector.OfType<ContentDialog>().Descendant().Name("CommandSpace"))
        {
            Setters = { new Setter(Layoutable.HorizontalAlignmentProperty, HorizontalAlignment.Right) }
        };

        var closeButtonStyle = new Style(
            selector => selector.OfType<ContentDialog>().Descendant().Name("CloseButton"))
        {
            Setters = { new Setter(Layoutable.WidthProperty, 200d) }
        };

        var app = Application.Current!;
        app.Styles.Add(layoutRootStyle);
        app.Styles.Add(titleStyle);
        app.Styles.Add(commandSpaceStyle);
        app.Styles.Add(closeButtonStyle);

        Navigation.Loaded += (_, _) => Navigation.SelectedItem = Navigation.MenuItems[0];

        Navigation.SelectionChanged +=
            (_, args) => OpenPage(args.SelectedItemContainer.Content as string ?? string.Empty);

        UpdateDialogSize(window.Bounds.Width, window.Bounds.Height - dialogTranslationY);

        window.SizeChanged +=
            (_, args) => UpdateDialogSize(args.NewSize.Width, args.NewSize.Height - dialogTranslationY);
    }

    public void OpenPage(string name)
    {
        var page = name switch
        {
            CustomizationSettings.Title => new CustomizationSettings(),
            _ => new ContentControl()
        };

        Navigation.Content = new StackPanel
        {
            Children =
            {
                new TextBlock
                {
                    Text = name,
                    FontSize = 28d,
                    FontWeight = FontWeight.SemiBold,
                    Margin = new Thickness(0d, 0d, 0d, 20d)
                },
                page
            },
            Margin = new Thickness(20d)
        };
    }

    private void UpdateDialogSize(double windowWidth, double windowHeight)
    {
        Dialog.Resources["ContentDialogMinWidth"] = windowWidth - 50d;
        Dialog.Resources["ContentDialogMinHeight"] = windowHeight - 50d;
    }
}

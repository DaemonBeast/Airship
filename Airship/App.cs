using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Airship.Windows;
using Avalonia.Styling;
using FluentAvalonia.Styling;

namespace Airship;

public class App : Application
{
    public override void Initialize()
    {
        var theme = new FluentAvaloniaTheme
        {
            UseSystemFontOnWindows = false,
            TextVerticalAlignmentOverrideBehavior = TextVerticalAlignmentOverride.AlwaysEnabled
        };

        Styles.Add(theme);

        RequestedThemeVariant = ThemeVariant.Default;
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }

        base.OnFrameworkInitializationCompleted();
    }
}

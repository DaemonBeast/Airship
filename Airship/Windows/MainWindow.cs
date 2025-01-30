using System;
using System.Threading.Tasks;
using Airship.Components.Settings;
using FluentAvalonia.UI.Controls;
using FluentAvalonia.UI.Windowing;

namespace Airship.Windows;

public class MainWindow : AppWindow
{
    public MainWindow()
    {
        Title = "Airship";
        MinWidth = 640;
        MinHeight = 480;

        var navigation = new NavigationView
        {
            MenuItems =
            [
                new NavigationViewItem
                {
                    Content = "Home",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Home }
                },
                new NavigationViewItem
                {
                    Content = "Accounts",
                    IconSource = new SymbolIconSource { Symbol = Symbol.Contact }
                }
            ],
            CompactModeThresholdWidth = 0d
        };

        navigation.Loaded += (_, _) =>
        {
            navigation.SettingsItem.SelectsOnInvoked = false;
            navigation.SelectedItem = navigation.MenuItems[0];
        };

        navigation.ItemInvoked += async (_, args) =>
        {
            if (args.IsSettingsInvoked) await OpenSettings();
        };

        navigation.SelectionChanged +=
            (_, args) => OpenPage(args.SelectedItemContainer.Content as string ?? string.Empty);

        Content = navigation;
    }

    public void OpenPage(string name)
    {
        Console.WriteLine($"Open Page {name}");
    }

    public async Task OpenSettings()
    {
        var settings = new Settings(this);
        await settings.Dialog.ShowAsync();
    }
}

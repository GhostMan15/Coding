using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
namespace NrpaNup;

public partial class Uporabnik : Window
{
    public Uporabnik()
    {
        InitializeComponent();
        AvaloniaXamlLoader.Load(this);
    }

    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
       var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
       close.Shutdown();
    }
}
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace NrpaNup;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        
    }
}
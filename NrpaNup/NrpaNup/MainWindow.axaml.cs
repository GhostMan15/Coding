using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
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
        var login = new Login();
        login.Show();
        this.Hide();
    }
}
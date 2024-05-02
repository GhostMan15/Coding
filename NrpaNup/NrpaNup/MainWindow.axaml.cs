using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
namespace NrpaNup;

public partial class MainWindow : Window
{
    private readonly Uporabnik _uporabnik;
    private readonly Zaposlen _zaposlen;
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
    }

    public MainWindow(Uporabnik uporabnik, Zaposlen zaposlen)
    {
        _uporabnik = uporabnik;
        _zaposlen = zaposlen;
    }
    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    { 
        var login = new Login (_zaposlen, _uporabnik);
        login.Show();
        this.Hide();
    }
}
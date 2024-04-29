using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using MySql.Data.MySqlClient;

namespace NrpaNup;

public partial class Zaposlen : Window
{
    private string conn;
    public Zaposlen()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
    }

    private void UstvariUporabnika()
    {
        string username = Username.Text;
        using (MySqlConnection connection = new MySqlConnection(conn))
        {
            connection.Open();
            string sql = "INSERT INTO uporabniki (ime), VALUES(@username)";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.ExecuteNonQuery();
            }
        }
    }
    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
        var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        close.Shutdown();
    }

    private void Kreiraj_OnClick(object? sender, RoutedEventArgs e)
    {
       
        Profil.IsVisible = false;
        Kreiranje.IsVisible = true;
    }

    private void Ustvarjen_OnClick(object? sender, RoutedEventArgs e)
    {
        UstvariUporabnika();
        var register = new Register();
        register.Show();
        Hide();
    }
}
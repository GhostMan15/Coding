using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using System.IO;
using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;
using System.Configuration;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace NrpaNup;

public partial class Login : Window
{
    //private static string kan = "Server=localhost;Database=nrpa;Uid=root;Pwd=root;"; 
    private static string conn;
    public int userId;
    public Login()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
    }


    private void SignIn_OnClick(object? sender, RoutedEventArgs e)
    {
        string username = Username.Text;
        string password = Password.Text;
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Username.Text = "";
            Password.Text = "";
            prazno.IsVisible = true;
            wrong.IsVisible = false;
            return;
            
        }

        using (MySqlConnection connection  = new MySqlConnection(conn) )
        {
            connection.Open();
            try
            {
                string sql = "SELECT id_uporabnika, name,geslo FROM uporabniki WHERE name = @name AND geslo = @geslo";
                using (MySqlCommand command = new MySqlCommand(sql,connection))
                {
                    command.Parameters.AddWithValue("@name", username);
                    command.Parameters.AddWithValue("@geslo", password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if ( reader.Read())
                        {
                            userId = reader.GetInt32("id_uporabnika");
                            Close();
                            var uporabnik = new Uporabnik();
                            uporabnik.Show();
                        }
                        else
                        {
                                Username.Text = "";
                                Password.Text = "";
                                wrong.IsVisible = true;
                                prazno.IsVisible = false;
                        }
                    }
                }
                Console.WriteLine(userId);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
           
        }
    }
}
public class AppSettingsReader
{
    private readonly IConfiguration _configuration;

    public AppSettingsReader(string FilePath)
    {
        var builder = new ConfigurationBuilder()
            .AddJsonFile(FilePath, optional: false, reloadOnChange: false);
        _configuration = builder.Build();
    }

    public string GetStringValue(string key)
    {
        return _configuration[key];
    }


}

using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using System.IO;
using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;
using System.Configuration;
using System.Data;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace NrpaNup;

public partial class Login : Window
{
    //private static string kan = "Server=localhost;Database=nrpa;Uid=root;Pwd=root;"; 
    private readonly string _conn;
    public static int userId;
    public string ime;
    private string vrsta;
    private readonly Uporabnik _uporabnik;
    public Login()
    {
        InitializeComponent();
        _uporabnik = new Uporabnik();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
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

        using (MySqlConnection connection  = new MySqlConnection(_conn) )
        {
            connection.Open();
            try
            {
                //string sql = "SELECT id_uporabnika, ime,geslo FROM uporabniki WHERE ime = @name AND geslo = @geslo";
                using (MySqlCommand command = new MySqlCommand("Vpis",connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ime", username);
                    command.Parameters.AddWithValue("@geslo", password);
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if ( reader.Read())
                        {
                            userId = reader.GetInt32("id_uporabnika");
                            vrsta = reader.GetString(reader.GetOrdinal("vrsta_uporabnika"));
                            Close();
                            switch (vrsta)
                            {
                                case "uporabnik":
                                    var uporabnik = new Uporabnik();
                                    uporabnik.Show();
                                    break;
                                case "zaposlen":
                                    var zaposlen = new Zaposlen();
                                    zaposlen.Show();
                                    break;
                                case "admin":
                                    var admin = new Admin();
                                    admin.Show();
                                    break;
                            }
                   
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

public class Prikaz
{
    public string Ime { get; set; }
    public ObservableCollection<Prikaz> prikazeIme { get; } = new ObservableCollection<Prikaz>();
    public Prikaz(string ime)
    {
        Ime = ime;
    }
    public Prikaz(){}
}
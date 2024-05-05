using System;
using System.Collections.Generic;
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
using System.Text.Json;
using System.Threading.Tasks;
using ConfigurationManager = System.Configuration.ConfigurationManager;

namespace NrpaNup;

public partial class Login : Window
{
    //private static string kan = "Server=localhost;Database=nrpa;Uid=root;Pwd=root;"; 
    private readonly string _conn;
    public static int userId;
    public static string ime;
    public static int id_kartica_uporabnika;
    private string vrsta;
    private readonly Uporabnik _uporabnik;
    private readonly Zaposlen _zaposlen;
  

    public Login()
    {
        InitializeComponent();
       var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
       Console.WriteLine(_conn);
       
    } 

    public Login(Zaposlen zaposlen, Uporabnik uporabnik) : this()
    {
        _zaposlen = zaposlen;
        _uporabnik = uporabnik;
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
                            ime = reader.GetString("ime");
                            vrsta = reader.GetString(reader.GetOrdinal("vrsta_uporabnika"));
                            var login = new Loginanje();
                             login.Login(ime, userId);
                             login.ReadLogFile(userId);
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
               
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
           
        }
    }
    
}


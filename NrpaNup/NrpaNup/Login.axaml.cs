using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.Configuration;
using System.IO;
using MySql.Data.MySqlClient;
using Tmds.DBus.Protocol;

namespace NrpaNup;

public partial class Login : Window
{
    private string conn; 
    private readonly IConfiguration configuration; 
    
    public Login()
    {
        InitializeComponent();
      
        configuration = new ConfigurationBuilder()
  
            .AddJsonFile("conn.json", optional:true)
            .Build();
        conn = configuration.GetConnectionString("MyConnectionString");
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
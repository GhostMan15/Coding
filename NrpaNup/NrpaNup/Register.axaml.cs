using System;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;

namespace NrpaNup;

public partial class Register : Window
{
    private readonly string _conn;
    public Register()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
    }

    private void UstvariKartico()
    {
        
        string username = Username.Text;
        decimal limit = Convert.ToDecimal(Limit.Text);
        decimal stanje = Convert.ToDecimal(Stanje.Text);
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "INSERT INTO kartica(limit,stanje) VALUES (@limit,@stanje)";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                try
                {
                    //command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ime", username);
                    command.Parameters.AddWithValue("@limit", limit);
                    command.Parameters.AddWithValue("@stanje", stanje);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
             
            }
        }
    }
    private void Register_OnClick(object? sender, RoutedEventArgs e)
    {
        UstvariKartico();
        var zaposlen = new Zaposlen();
        zaposlen.Show();
        Close();
    }
}
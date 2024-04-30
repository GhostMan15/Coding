using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using MySql.Data.MySqlClient;

namespace NrpaNup;

public partial class Uporabnik : Window
{
    private ObservableCollection<Kartica> podatkiKartice { get; } = new ObservableCollection<Kartica>();
    private ObservableCollection<Kartica> podatkiKartice2 { get; } = new ObservableCollection<Kartica>();
    private string conn;
    public Uporabnik()
    {
        InitializeComponent();
        DataContext = this;
        var reader = new AppSettingsReader("appsettings.json");
        conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        Kartica1();
        Kartica2();
    }
    
    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
       var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
       close.Shutdown();
    }

    public void Kartica1()
    {
        podatkiKartice.Clear();
        using (MySqlConnection connection = new MySqlConnection(conn))
        {
            connection.Open();
            string sql = "SELECT k.id_kartica, u.id_uporabnika, st_kartice, vrsta, `limit`, status, stanje, veljavnost FROM kartica k JOIN uporabniki u " +
                         "ON k.id_uporabnika = u.id_uporabnika WHERE  u.id_uporabnika = @uporabnik AND  vrsta = 'debitna'";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@uporabnik", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_kartica = reader.GetInt32("id_kartica");
                        string st_kartice = reader.GetString("st_kartice");
                        string vrsta = reader.GetString("vrsta");
                        int limit = reader.GetInt32("limit");
                        string status = reader.GetString("status");
                        double stanje = reader.GetDouble("stanje");
                        string veljavnost = reader.GetString("veljavnost");
                        var kartica = new Kartica(id_kartica, st_kartice, vrsta, limit, status, stanje, veljavnost);
                        podatkiKartice.Add(kartica);
                      
                    }
                }
             
               
            }
        }
        KarticaD.ItemsSource = podatkiKartice;
        Stanje.ItemsSource = podatkiKartice;


    } 
    public void Kartica2()
    {
        podatkiKartice2.Clear();
        using (MySqlConnection connection = new MySqlConnection(conn))
        {
            connection.Open();
            string sql = "SELECT k.id_kartica, u.id_uporabnika, st_kartice, vrsta, `limit`, status, stanje, veljavnost FROM kartica k JOIN uporabniki u " +
                         "ON k.id_uporabnika = u.id_uporabnika WHERE  u.id_uporabnika = @uporabnik AND  vrsta = 'kreditna'";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@uporabnik", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_kartica = reader.GetInt32("id_kartica");
                        string st_kartice = reader.GetString("st_kartice");
                        string vrsta = reader.GetString("vrsta");
                        int limit = reader.GetInt32("limit");
                        string status = reader.GetString("status");
                        double stanje = reader.GetDouble("stanje");
                        string veljavnost = reader.GetString("veljavnost");
                        var kartica = new Kartica(id_kartica, st_kartice, vrsta, limit,  status, stanje, veljavnost);
                        podatkiKartice2.Add(kartica);
                        Console.WriteLine(stanje);
                    }
                }
             
              
            }
        }
        KarticaK.ItemsSource = podatkiKartice2;
    }
}

public class Kartica
{
    public int IDkartica { get; set; }
    public double Stanje { get; set; }
    public string STKratice { get; set; }
    public int Limit { get; set; }
    public string Status { get; set; }
    public string Vrsta { get; set; }
    public string Veljavnost { get; set; }
    public Kartica(){}

    public Kartica(int idDkartica, string stKratice, string vrsta, int limit, string status, double stanje, string veljavnost)
    {
       IDkartica = idDkartica ;
       Stanje = stanje ;
       STKratice = stKratice ;
       Limit = limit;
       Status = status;
       Vrsta = vrsta;
       Veljavnost = veljavnost;
    }
}
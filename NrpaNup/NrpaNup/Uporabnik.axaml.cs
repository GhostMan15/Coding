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
    private ObservableCollection<Narocnine> vseNarocnineUporabnik { get; } = new ObservableCollection<Narocnine>();
    private readonly Zaposlen _zaposlen;
    private string _conn;
    public Uporabnik()
    {
        InitializeComponent();
        DataContext = this;
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        Kartica1();
        Kartica2();
     vseMozneNarocnine();
    }

  
    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
       var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
       close.Shutdown();
    }

    public void Kartica1()
    {
        podatkiKartice.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
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
        using (MySqlConnection connection = new MySqlConnection(_conn))
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
                       
                    }
                }
             
              
            }
        }
        KarticaK.ItemsSource = podatkiKartice2;
    }
    private void vseMozneNarocnine()
    {
        vseNarocnineUporabnik.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "SELECT * FROM narocnine";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_narocnine = reader.GetInt32("id_narocnine");
                        decimal vsota_mesecno = reader.GetDecimal("vsota_mesecno");
                        string ime_narocnine = reader.GetString("ime_narocnine");
                        decimal vsota_letno = reader.GetDecimal("vsota_letno");
                        var narocnine = new Narocnine(id_narocnine,ime_narocnine,vsota_mesecno ,vsota_letno);
                        vseNarocnineUporabnik.Add(narocnine);
                    }
                }
            }
        }

        PrikazUporabnikomNarocnine.ItemsSource = vseNarocnineUporabnik;
    }
   
}


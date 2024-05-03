using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Timers;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using MySql.Data.MySqlClient;

namespace NrpaNup;

public partial class Uporabnik : Window
{
    private ObservableCollection<Kartica> podatkiKartic { get; } = new ObservableCollection<Kartica>();
    private ObservableCollection<Kartica> podatkiKartice1 { get; } = new ObservableCollection<Kartica>();
    private ObservableCollection<Kartica> podatkiKartice2 { get; } = new ObservableCollection<Kartica>();
    private ObservableCollection<Narocnine> vseNarocnineUporabnik { get; } = new ObservableCollection<Narocnine>();
    
    private readonly Zaposlen _zaposlen;
    private string _conn;
    private Timer _timer;
    private int id_kartica;
    //var za narocnine
    private decimal? mesecno;
    private decimal? letno;
    private int id_narocnine;
    private int na_kartico;
    public Uporabnik()
    {
        InitializeComponent();
        DataContext = this;
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        Kartica();
        Kartica1();
        Kartica2();
        vseMozneNarocnine();
        MojeNarocnine();
        MojiKrediti();
     _timer = new Timer();
     _timer.Interval = 10000; 
     _timer.Elapsed += KliciFunkcijo;
     _timer.AutoReset = true;
     _timer.Start();
    }
    
    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
       var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
       close.Shutdown();
    }

    private void KliciFunkcijo(object sender,  ElapsedEventArgs e)
    {
        Kartica1();
        MojeNarocnine();
    }

    public void Kartica()
    {   podatkiKartic.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
           
            string sql = "SELECT k.id_kartica, u.id_uporabnika, st_kartice, vrsta, `limit`, status, stanje, veljavnost FROM kartica k JOIN uporabniki u " +
                         "ON k.id_uporabnika = u.id_uporabnika WHERE  u.id_uporabnika = @uporabnik";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
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
                        podatkiKartic.Add(kartica);

                    }
                }
            }
        }

        KarticeBox.ItemsSource = podatkiKartic;
    }
    public void Kartica1()
    {
        podatkiKartice1.Clear();
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
                        podatkiKartice1.Add(kartica);
                      
                    }
                }
            }
            using (MySqlCommand komanda = new MySqlCommand("UpdateStanje",connection))
            {
                komanda.CommandType = CommandType.StoredProcedure;
                komanda.Parameters.AddWithValue("@id", Login.userId);
                komanda.ExecuteNonQuery();
            }
        }
        KarticaD.ItemsSource = podatkiKartice1;
        Stanje.ItemsSource = podatkiKartice1;


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
            using (MySqlCommand komanda = new MySqlCommand("UpdateStanje",connection))
            {
                komanda.CommandType = CommandType.StoredProcedure;
                komanda.Parameters.AddWithValue("@id", Login.userId);
                komanda.ExecuteNonQuery();
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

    private void Subscribe()
    {
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "INSERT INTO narocnineU (id_uporabnika, id_kartica, id_narocnine, vsota_narocnine,tip_narocnine) VALUES (@id_uporabnika,@id_kartica,@id_narocnine,@vsota_narocnine,@tip_narocnine)";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@id_uporabnika", Login.userId);
                command.Parameters.AddWithValue("@id_kartica", id_kartica);
                command.Parameters.AddWithValue("@id_narocnine", id_narocnine);
                command.Parameters.AddWithValue("@vsota_narocnine", mesecno ?? letno); //Ce je narocnina letna
                command.Parameters.AddWithValue("@tip_narocnine", mesecno != null ? "mesecno" : "letno");
                command.ExecuteNonQuery();
            }
        }
    }
    private void MojeNarocnine()
    {
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = " SELECT * FROM   NarocnineUporabnika WHERE id_uporabnika = @id";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@id", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        
                    }
                }
            }
            using (MySqlCommand komanda = new MySqlCommand("UpdateNarocnine",connection))
            {
                komanda.CommandType = CommandType.StoredProcedure;
                komanda.Parameters.AddWithValue("@id", Login.userId);
                komanda.ExecuteNonQuery();
            }
        }
        
    }

    private void MojiKrediti()
    {
        
    }

    private void Subscribe_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Narocnine narocnine)
        { 
            mesecno = narocnine.VsotaNarocnineMesecno;
            id_narocnine = narocnine.IDNarocnine;
           Console.WriteLine($"Mesecno: {mesecno} ID: {id_narocnine}" );
           
        }

        Narocnine.IsVisible = false;
        NarociSe.IsVisible = true;
    }

    private void SubscribeLetno_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Narocnine narocnine)
        {
            letno = narocnine.VsotaNarocnineLetno;
            id_narocnine = narocnine.IDNarocnine;
            Console.WriteLine($"Letno: {letno}  ID: {id_narocnine}");
        }
        Narocnine.IsVisible = false;
        NarociSe.IsVisible = true;
    }
    

    private void Sub_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Kartica kartica)
        {
            id_kartica = kartica.IDkartica;
            Console.WriteLine(id_kartica);
        }
        else
        {
            Console.WriteLine("nuuh");
        }
        Subscribe();
        Narocnine.IsVisible = true;
        NarociSe.IsVisible = false;
    }
}


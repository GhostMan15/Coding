using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
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
    private ObservableCollection<Narocnine> uporabnikoveNarocnie { get; } = new ObservableCollection<Narocnine>();

    private ObservableCollection<KreditiUporabnika> kreditiUporabnika { get; } = new ObservableCollection<KreditiUporabnika>();
    

private readonly Zaposlen _zaposlen;
    private string _conn;
    private Timer _timer;
    private int id_kartica;
    private string? vrsta;
    private int id_kredita;
    //var za narocnine
    private decimal? mesecno;
    private decimal? letno;
    //private decimal nakazilo;
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
       // vseMozneNarocnine();
       // MojeNarocnine();
        MojiKrediti();
     _timer = new Timer();
     _timer.Interval = 10000; 
     _timer.Elapsed += KliciFunkcijo;
     _timer.AutoReset = true;
     _timer.Start();
     var loginanje = new Loginanje();
     var logi = loginanje.ReadLogFile(Login.userId);
     MojePrijaveBox.ItemsSource = logi;
    }
    
    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
       var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
       var logut = new Loginanje();
       logut.Logout(Login.ime, Login.userId);
       close.Shutdown();
    }

    private void KliciFunkcijo(object sender,  ElapsedEventArgs e)
    {
        Kartica1();
       // MojeNarocnine();
        MojiKrediti();
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
                        decimal stanje = reader.GetDecimal("stanje");
                        string veljavnost = reader.GetString("veljavnost");
                        var kartica = new Kartica(id_kartica, st_kartice, vrsta, limit, status, stanje, veljavnost);
                        podatkiKartic.Add(kartica);

                    }
                }
            }
        }

        //KarticeBox.ItemsSource = podatkiKartic;
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
                        decimal stanje = reader.GetDecimal("stanje");
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
        //Stanje.ItemsSource = podatkiKartic;
    } 
    public void Kartica2()
    {
        podatkiKartice2.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "SELECT k.id_kartica, u.id_uporabnika, st_kartice, vrsta, `limit`, status, stanje, veljavnost FROM kartica k JOIN uporabniki u " +
                         "ON k.id_uporabnika = u.id_uporabnika WHERE  u.id_uporabnika = @uporabnik AND  vrsta = 'kreditna' AND vrsta IS NOT NULL ";
           
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@uporabnik", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_kartica = reader.GetInt32("id_kartica");
                        string st_kartice = reader.GetString("st_kartice");
                        vrsta = reader.GetString("vrsta");
                        int limit = reader.GetInt32("limit");
                        string status = reader.GetString("status");
                        decimal stanje = reader.GetDecimal("stanje");
                        string veljavnost = reader.GetString("veljavnost");
                        var kartica = new Kartica(id_kartica, st_kartice, vrsta, limit,  status, stanje, veljavnost);
                        podatkiKartice2.Add(kartica);
                        Kreditna.IsVisible = true;
                       
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
   /* private void vseMozneNarocnine()
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

        //PrikazUporabnikomNarocnine.ItemsSource = vseNarocnineUporabnik;
    }*/

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
        uporabnikoveNarocnie.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = " SELECT * FROM   NarocnineUporabnika WHERE uporabnikov_id = @id";
            string skl = "SELECT nu.id_uporabnika,nu.vsota_narocnine,nu.id_kartica,k.id_kartica,k.vrsta,n.ime_narocnine,nu.tip_narocnine,nu.id_narocnineU " +
                         "FROM narocnine n JOIN narocnineU nu ON n.id_narocnine = nu.id_narocnine JOIN kartica k ON nu.id_kartica = k.id_kartica " +
                         "WHERE  nu.id_uporabnika = @id AND id_narocnineU IS NOT NULL ;";
            using (MySqlCommand command = new MySqlCommand(skl,connection))
            {
                command.Parameters.AddWithValue("@id", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_narocnineU = reader.GetInt32("id_narocnineU");
                        int id_uporabnika = reader.GetInt32("id_uporabnika");
                       // int id_narocnin = reader.GetInt32("id_narocnine");
                        int id_kartice = reader.GetInt32("id_kartica");
                        string vrsta = reader.GetString("vrsta");
                        int vsota = reader.GetInt32("vsota_narocnine");
                        string ime_narocnine = reader.GetString("ime_narocnine");
                        string tip_narocnine = reader.GetString("tip_narocnine");
                        var narocnineU = new Narocnine(id_narocnineU,id_uporabnika,id_kartice, tip_narocnine, vrsta, ime_narocnine, vsota );
                        uporabnikoveNarocnie.Add(narocnineU);
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

        // MojeNarocnineBox.ItemsSource = vseNarocnineUporabnik;
    }

    private void MojiKrediti()
    {
        kreditiUporabnika.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "SELECT k.st_kartice, ku.id, ku.id_kartica, ku.id_uporabnika, ku.cas, ku.id_krediti, ku.mesecno_odplacilo, " +
                         "ku.st_odplacil, k2.vsota, k2.fixna_obrestna_mera, k2.tip_kredita " +
                         "FROM kreditiU ku JOIN kartica k on k.id_kartica = ku.id_kartica  " +
                         "JOIN krediti k2 on k2.id_krediti = ku.id_krediti WHERE ku.id_uporabnika = @id";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@id", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int id_upo = reader.GetInt32("id_uporabnika");
                        int id_kar = reader.GetInt32("id_kartica");
                        string st_kratice = reader.GetString("st_kartice");
                        int id_kredita = reader.GetInt32("id_krediti");
                        string cas = reader.GetString("cas");
                        decimal odplacilo = reader.GetDecimal("mesecno_odplacilo");
                        int st_odplacil = reader.GetInt32("st_odplacil");
                        int vstoa_kre = reader.GetInt32("vsota");
                        string tip_kredita = reader.GetString("tip_kredita");
                        decimal obrest = reader.GetDecimal("fixna_obrestna_mera");
                        decimal obrest_procent = obrest * 100;
                        var kreditiUpo = new KreditiUporabnika(id,id_upo,id_kredita, id_kar,vstoa_kre, tip_kredita, obrest_procent, odplacilo, cas, st_odplacil, st_kratice);
                        kreditiUporabnika.Add(kreditiUpo);
                    }
                }
            }
        }

        MojiKreditiBox.ItemsSource = kreditiUporabnika;
    }

    private void Nakazila()
    {
       decimal nakazi = Convert.ToDecimal(value.Text);
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "UPDATE kartica  " +
                         "SET stanje = stanje + @nakazilo " +
                         "WHERE id_uporabnika = @id;";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@id", Login.userId);
                command.Parameters.AddWithValue("@nakazilo", nakazi);
                command.ExecuteNonQuery();
            }
        }
    }
    private void Subscribe_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Narocnine narocnine)
        { 
            mesecno = narocnine.VsotaNarocnineMesecno;
            id_narocnine = narocnine.IDNarocnine;
           Console.WriteLine($"Mesecno: {mesecno} ID: {id_narocnine}" );
           
        }

        //Narocnine.IsVisible = false;
        //NarociSe.IsVisible = true;
    }

    private void SubscribeLetno_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is Narocnine narocnine)
        {
            letno = narocnine.VsotaNarocnineLetno;
            id_narocnine = narocnine.IDNarocnine;
            Console.WriteLine($"Letno: {letno}  ID: {id_narocnine}");
        }
        // Narocnine.IsVisible = false;
        //NarociSe.IsVisible = true;
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
        MojeNarocnine();
        //Narocnine.IsVisible = true;
        //  NarociSe.IsVisible = false;
    }

    private async Task UpsenoNakazan()
    {
        UspesnoNakazano.IsVisible = true;
        await Task.Delay(2000);
        UspesnoNakazano.IsVisible = false;
    }

    private void Nakazi_OnClick(object? sender, RoutedEventArgs e)
    {
        UpsenoNakazan();
        Nakazila();
        Profil.IsVisible = true;
        Nakazilo.IsVisible = false;
      
        if (vrsta !=null)
        {
            Kreditna.IsVisible = true;
        }
    }

    private void UstvariNakazilo_OnClick(object? sender, RoutedEventArgs e)
    {
        Profil.IsVisible = false;
        Nakazilo.IsVisible = true;
        Kreditna.IsVisible = false;
    }
}


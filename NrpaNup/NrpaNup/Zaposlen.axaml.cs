using System;
using System.Collections.ObjectModel;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;
using Avalonia.Data.Converters;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.LogicalTree;
using VisualExtensions = Avalonia.VisualTree.VisualExtensions;
using Avalonia.VisualTree;

namespace NrpaNup;

public partial class Zaposlen : Window
{
    private ObservableCollection<UporabniskiPodatki> uporabniskiPodatki { get; } = new ObservableCollection<UporabniskiPodatki>();
    private ObservableCollection<Krediti> vsiMozniKredit { get; } = new ObservableCollection<Krediti>();
    public ObservableCollection<string> uporabnikiImena { get; } = new ObservableCollection<string>();

    private readonly string _conn;
    public int id_kredit;
    public int id_uporabnik;
    public Zaposlen()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        PrikazVsehKreditov();
        UporabnikiZaposlenega();
    }

    
    private void UstvariUporabnika()
    {
        string username = Username.Text;
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "INSERT INTO uporabniki (ime) VALUES(@username)";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@username", username);
                command.ExecuteNonQuery();
            }
        }
    }

    private void PrikazVsehKreditov()
    {
        vsiMozniKredit.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "SELECT * FROM VsiKrediti";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_krediti = reader.GetInt32("id_krediti");
                        int vsota = reader.GetInt32("vsota");
                        string tip_kredita = reader.GetString("tip_kredita");
                        decimal obrestna_mera  = reader.GetDecimal("fixna_obrestna_mera");
                        decimal obrest_procent = obrestna_mera * 100;
                        var krediti = new Krediti(id_krediti, vsota, tip_kredita, obrest_procent);
                        vsiMozniKredit.Add(krediti);
                    }
                }
            }
        }

        Kredit.ItemsSource = vsiMozniKredit;
    }
   
  
    private void UporabnikiZaposlenega()
    {
        uporabniskiPodatki.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            
            using (MySqlCommand command = new MySqlCommand("UporabnikiZaposlenega",connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("id_zaposlen", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Podatki uporabnika
                        int uporabnikId = reader.GetInt32("id_uporabnika");
                        string ime = reader.GetString("ime");
                        string geslo = reader.GetString("geslo");
                        string created = reader.GetString("created");
                        string vrstaUporabnika = reader.GetString("vrsta_uporabnika");
                        //Podatki karice
                        int id_kartica = reader.GetInt32("id_kartica");
                        string st_kartice = reader.GetString("st_kartice");
                        int kartica_limit = reader.GetInt32("limit");
                        string kartica_vrsta = reader.GetString("kartica_vrsta");
                        string kartica_status = reader.GetString("kartica_status");
                        double kartica_stanje = reader.GetDouble("kartica_stanje");
                        string kartica_veljavnost = reader.GetString("kartica_veljavnost");
                        var uporabniki = new UporabniskiPodatki(uporabnikId,ime, geslo, created, vrstaUporabnika, id_kartica,
                            st_kartice, kartica_vrsta, kartica_limit, kartica_status, kartica_stanje, kartica_veljavnost);
                        uporabniskiPodatki.Add(uporabniki);      
                    }
                }
            }
        }
        UporabnikiBox.ItemsSource = uporabniskiPodatki;
    }

    private void Imena()
    {
        uporabnikiImena.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = " SELECT id_zaposlen, z.id_uporabnika, u.id_uporabnika, u.ime FROM uporabniki u JOIN zaposlen z " +
                         "ON u.id_uporabnika = z.id_uporabnika WHERE id_zaposlen = @zaposlen ";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@zaposlen", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string ime = reader.GetString("ime");
                        uporabnikiImena.Add(ime);
                    }
                }
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

    private void Ustvari_OnClick(object? sender, RoutedEventArgs e)
    { 
        UstvariUporabnika();
        UporabnikiZaposlenega();
        UspesnoKreiran();
        Profil.IsVisible = true;
        Kreiranje.IsVisible = false;
    }

    private async Task UspesnoKreiran()
    {
        Uspesno.IsVisible = true;
        await Task.Delay(1000);
        Uspesno.IsVisible = false;
    }

    private void UstvariGenericniKredit_OnClick(object? sender, RoutedEventArgs e)
    {
        var register = new Register();
        register.Show();
        Hide();
    }

    private void Expander_OnExpanded(object? sender, RoutedEventArgs e)
    {
        var expander = (Expander)sender;
        var imenaUporabnika = FindInnerListBox(expander);
        imenaUporabnika.ItemsSource = uporabnikiImena;
        Imena();
        if (sender is Expander expande  && expande.Tag is Krediti krediti)
        {
            id_kredit = krediti.IDkredita;
            Console.WriteLine(id_kredit);
        }
        
    }
    private ListBox? FindInnerListBox(Expander expander)
    {
        foreach (var item in expander.GetLogicalChildren())
        {
            if (item is ListBox listBox)
            {
                return listBox;
            }
        }
        return null;
    }
   

}


using System;
using System.Collections.ObjectModel;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.LogicalTree;
using MySqlConnector;

namespace NrpaNup;

public partial class Admin : Window
{
    private ObservableCollection<UporabniskiPodatki> vsiUporabniskiPodatki { get; } = new ObservableCollection<UporabniskiPodatki>();
    private ObservableCollection<UporabniskiPodatki> zposleni { get; } = new ObservableCollection<UporabniskiPodatki>();
    private readonly string _conn;
    private int id_uporabnika;
    private int id_zaposlenega;
    private string ime_zaposlenega;
    private string geslo_zaposlenega;
    private string izbrani_tip;
    public Admin()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        PrikazUporabniskihPodatkov();
        var loginanje = new Loginanje();
        var logi = loginanje.ReadLogFile(Login.userId);
        MojePrijaveBox.ItemsSource = logi;
    }
    private void PrikazUporabniskihPodatkov()
    {
        vsiUporabniskiPodatki.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "SELECT * FROM PrikazUporabniskihPodatkov ";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
               
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        //PodatkiUporabnika
                        int uporabnikId = reader.GetInt32("uporabnik_id");
                        string ime = reader.GetString("uporabnik_ime");
                        string geslo = reader.GetString("uporabnik_geslo");
                        string created = reader.GetString("uporabnik_created");
                        string vrstaUporabnika = reader.GetString("uporabnik_vrsta");
                        //Podatki Kratice
                        int id_kartica = reader.GetInt32("kartica_id");
                        string st_kartice = reader.GetString("kartica_st");
                        string vrsta = reader.GetString("kartica_vrsta");
                        int limit = reader.GetInt32("kartica_limit");
                        string status = reader.GetString("kartica_status");
                        decimal? stanje = reader.GetDecimal("kartica_stanje");
                        string veljavnost = reader.GetString("kartica_veljavnost");
                        var uporabniki = new UporabniskiPodatki(uporabnikId,ime, geslo, created, vrstaUporabnika, id_kartica,
                            st_kartice, vrsta, limit, status, stanje, veljavnost);
                        vsiUporabniskiPodatki.Add(uporabniki);      
                    }
                }
            }
        }

        AdminPrikazUporabnikov.ItemsSource = vsiUporabniskiPodatki;
    }

    private void PrikazZaposlenih()
    {
        zposleni.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "SELECT  z.id_zaposlen, z.id_uporabnika, z.ime,z.geslo, u.vrsta_uporabnika FROM zaposlen z JOIN  uporabniki u " +
                         " ON z.id_uporabnika = u.id_uporabnika WHERE z.id_uporabnika != @id_uporabnika GROUP BY z.ime ";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id_uporabnika", id_uporabnika);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int zaposlen_id = reader.GetInt32("id_zaposlen");
                        int uporabnik_id = reader.GetInt32("id_uporabnika");
                         string ime = reader.GetString("ime");
                         string geslo = reader.GetString("geslo");
                        var vsiZaposleni = new UporabniskiPodatki(zaposlen_id, uporabnik_id, ime, geslo);
                        zposleni.Add(vsiZaposleni);
                    }
                }
            }
        }
    }

    private void DodajKZaposlenom()
    {
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "INSERT INTO zaposlen (id_zaposlen, id_uporabnika, ime, geslo) VALUES (@id_zaposlen, @id_uporabnika,@ime,@geslo)";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                command.Parameters.AddWithValue("@id_zaposlen", id_zaposlenega);
                command.Parameters.AddWithValue("@id_uporabnika", id_uporabnika);
                command.Parameters.AddWithValue("@ime", ime_zaposlenega);
                command.Parameters.AddWithValue("@geslo", geslo_zaposlenega);
                command.ExecuteNonQuery();
            }
        }
    }

    private void UstvariPRofil()
    {
        string ustvari_ime = AdminAddUsername.Text;
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql = "INSERT INTO uporabniki (ime,vrsta_uporabnika) VALUES (@ime, @vrsta_uporabnika)";
            using (MySqlCommand command = new MySqlCommand(sql,connection))
            {
                    command.Parameters.AddWithValue("@ime", ustvari_ime);
                    command.Parameters.AddWithValue("@vrsta_uporabnika", izbrani_tip);
                    command.ExecuteNonQuery();
            }
        }
    }
    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
        var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        var logut = new Loginanje();
        logut.Logout(Login.ime, Login.userId);
        close.Shutdown();
    }

    private void KreirajProfil_OnClick(object? sender, RoutedEventArgs e)
    {
        ProfilPanel.IsVisible = false;
        KreiranjeProfila.IsVisible = true;
    }
    private void DodajKZaposlenom_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is UporabniskiPodatki uporabniskiPodatki )
        {
            id_zaposlenega = uporabniskiPodatki.ZaposlenID;
            ime_zaposlenega = uporabniskiPodatki.ZaposlenIme;
            geslo_zaposlenega = uporabniskiPodatki.ZaposlenGeslo;
        }
        DodajKZaposlenom();
    }
    private void Expander_OnExpanded(object? sender, RoutedEventArgs e)
    {
        if (sender is Expander expande  && expande.Tag is UporabniskiPodatki krediti)
        {
            id_uporabnika = krediti.UporabnikID;
            Console.WriteLine(id_uporabnika);
        }
        var expander = (Expander)sender;
        var imeZaposlenega = FindInnerListBox(expander);
        imeZaposlenega.ItemsSource = zposleni;
        PrikazZaposlenih();
        
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


    private void Checked_OnChecked(object? sender, RoutedEventArgs e)
    {
        RadioButton izbrani = sender as RadioButton;
        if(izbrani != null && izbrani.IsChecked == true)
        {
            izbrani_tip = izbrani.Tag.ToString();
            Console.WriteLine(izbrani_tip);
        }
   
    }

    private void Ustvari_OnClick(object? sender, RoutedEventArgs e)
    {
        UstvariPRofil();
        ProfilPanel.IsVisible = true;
        KreiranjeProfila.IsVisible = false;
    }
}
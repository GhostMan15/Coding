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
using Avalonia.Controls.Primitives;
using Avalonia.LogicalTree;
using VisualExtensions = Avalonia.VisualTree.VisualExtensions;
using Avalonia.VisualTree;

namespace NrpaNup;

public partial class Zaposlen : Window
{
    private ObservableCollection<UporabniskiPodatki> uporabniskiPodatki { get; } =
        new ObservableCollection<UporabniskiPodatki>();

    private ObservableCollection<Krediti> vsiMozniKredit { get; } = new ObservableCollection<Krediti>();

    private ObservableCollection<UporabniskiPodatki> uporabnikiImena { get; } =
        new ObservableCollection<UporabniskiPodatki>();

    private ObservableCollection<KreditiUporabnika> KreditiUporabnikaZaposlen { get; } =
        new ObservableCollection<KreditiUporabnika>();

    private ObservableCollection<UporabniskiPodatki> vecinInfromacijOUporabniku { get; } =
        new ObservableCollection<UporabniskiPodatki>();

    private readonly string _conn;
    private int id_kredit;
    private int id_uporabnik;
    private int id_kartica;
    private int cas;
    private decimal obrest;
    private int vsota;

    public Zaposlen()
    {

        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        PrikazVsehKreditov();
        UporabnikiZaposlenega();
        //vseMozneNarocnine();
        KreditiUpo();
        var loginanje = new Loginanje();
        var logi = loginanje.ReadLogFile(Login.userId);
        MojePrijaveBox.ItemsSource = logi;
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
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id_krediti = reader.GetInt32("id_krediti");
                        int vsota = reader.GetInt32("vsota");
                        string tip_kredita = reader.GetString("tip_kredita");
                        decimal obrestna_mera = reader.GetDecimal("fixna_obrestna_mera");
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

            using (MySqlCommand command = new MySqlCommand("UporabnikiZaposlenega", connection))
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
                        decimal kartica_stanje = reader.GetDecimal("kartica_stanje");
                        string kartica_veljavnost = reader.GetString("kartica_veljavnost");
                        var uporabniki = new UporabniskiPodatki(uporabnikId, ime, geslo, created, vrstaUporabnika,
                            id_kartica,
                            st_kartice, kartica_vrsta, kartica_limit, kartica_status, kartica_stanje,
                            kartica_veljavnost);
                        uporabniskiPodatki.Add(uporabniki);
                    }
                }
            }
        }

        UporabnikiBox.ItemsSource = uporabniskiPodatki;
    }

    private void VecInformacijUporabnika()
    {
        vecinInfromacijOUporabniku.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            string sql =
                "SELECT u.id_uporabnika, u.ime AS ime, u.geslo, u.created, u.vrsta_uporabnika, k.id_kartica, k.st_kartice, k.`limit`, k.vrsta AS kartica_vrsta,k.status AS kartica_status, k.stanje AS kartica_stanje, k.veljavnost AS kartica_veljavnost  " +
                "FROM uporabniki u  JOIN zaposlen z ON u.id_uporabnika = z.id_uporabnika " +
                "JOIN kartica k ON u.id_uporabnika = k.id_uporabnika WHERE u.id_uporabnika = @id;";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id_uporabnik);
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
                        decimal kartica_stanje = reader.GetDecimal("kartica_stanje");
                        string kartica_veljavnost = reader.GetString("kartica_veljavnost");
                        var uporabnikiPodatki = new UporabniskiPodatki(uporabnikId, ime, geslo, created,
                            vrstaUporabnika,
                            id_kartica,
                            st_kartice, kartica_vrsta, kartica_limit, kartica_status, kartica_stanje,
                            kartica_veljavnost);
                        vecinInfromacijOUporabniku.Add(uporabnikiPodatki);
                    }
                }
            }
        }

        Vecinformacij.ItemsSource = vecinInfromacijOUporabniku;
    }

    private void KreditiUpo()
    {
        KreditiUporabnikaZaposlen.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand("UporabniskiKrediti", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@id", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32("id");
                        int id_upo = reader.GetInt32("id_uporabnika");
                        string ime = reader.GetString("ime");
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
                        var kreditiUpo = new KreditiUporabnika(id, id_upo, id_kredita, id_kar, ime, vstoa_kre,
                            tip_kredita, obrest_procent, odplacilo, cas, st_odplacil, st_kratice);
                        KreditiUporabnikaZaposlen.Add(kreditiUpo);
                    }
                }
            }
        }

        KreditiDisplay.ItemsSource = KreditiUporabnikaZaposlen;
    }

    private void Imena()
    {
        uporabnikiImena.Clear();
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            //Za dodelitev kredita uporabniku
            string sql =
                " SELECT id_zaposlen, z.id_uporabnika, u.id_uporabnika, u.ime, k.id_kartica, k.id_uporabnika FROM uporabniki u " +
                "JOIN zaposlen z ON u.id_uporabnika = z.id_uporabnika " +
                "JOIN kartica k ON  u.id_uporabnika = k.id_uporabnika WHERE id_zaposlen = @zaposlen ";
            using (MySqlCommand command = new MySqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@zaposlen", Login.userId);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int uporabnik_id = reader.GetInt32("id_uporabnika");
                        string ime = reader.GetString("ime");
                        int kartica_id = reader.GetInt32("id_kartica");
                        var ime_uporabnik_zaposlen = new UporabniskiPodatki(uporabnik_id, ime, kartica_id);
                        uporabnikiImena.Add(ime_uporabnik_zaposlen);
                    }
                }
            }
        }

    }

    /*  private void UstvariNarocnine()
      {
          string imeNarocnine = ImeNarocnine.Text;
          string vsotaMesecno = VsotaMesecno.Text;
          decimal vstoaMedecnoD = decimal.Parse(vsotaMesecno);
          using (MySqlConnection connection = new MySqlConnection(_conn))
          {
              connection.Open();
              using (MySqlCommand command = new MySqlCommand("UstvariNarocnino", connection))
              {
                  command.CommandType = CommandType.StoredProcedure;
                  command.Parameters.AddWithValue("@p_ime_narocnine", imeNarocnine);
                  command.Parameters.AddWithValue("@p_vsota_mesecno", vstoaMedecnoD);
                  command.ExecuteNonQuery();
              }
          }
      }

      private void vseMozneNarocnine()
      {
          vseNarocnine.Clear();
          using (MySqlConnection connection = new MySqlConnection(_conn))
          {
              connection.Open();
              string sql = "SELECT * FROM narocnine";
              using (MySqlCommand command = new MySqlCommand(sql, connection))
              {
                  using (MySqlDataReader reader = command.ExecuteReader())
                  {
                      while (reader.Read())
                      {
                          int id_narocnine = reader.GetInt32("id_narocnine");
                          decimal vsota_mesecno = reader.GetDecimal("vsota_mesecno");
                          string ime_narocnine = reader.GetString("ime_narocnine");
                          decimal vsota_letno = reader.GetDecimal("vsota_letno");
                          var narocnine = new Narocnine(id_narocnine, ime_narocnine, vsota_mesecno, vsota_letno);
                          vseNarocnine.Add(narocnine);
                      }
                  }
              }
          }

          VseMozneNarocnine.ItemsSource = vseNarocnine;
      } */

    private void DodeliKredit()
    {
        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            using (MySqlCommand komanda = new MySqlCommand("Izracun", connection))
            {
                komanda.CommandType = CommandType.StoredProcedure;
                komanda.Parameters.AddWithValue("@vsota", vsota);
                komanda.Parameters.AddWithValue("@obrestna_mera", obrest);
                komanda.Parameters.AddWithValue("@cas", cas);
                decimal izracun = Convert.ToDecimal(komanda.ExecuteScalar());
                string sql =
                    "INSERT INTO kreditiU(id_uporabnika,id_kartica,cas,id_krediti, mesecno_odplacilo, st_odplacil) VALUES (@id_uporabnika,@id_kartica,@cas,@id_krediti, @mesecno_odplacilo, @st_odplacil)";
                using (MySqlCommand command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id_uporabnika", id_uporabnik);
                    command.Parameters.AddWithValue("@id_kartica", id_kartica);
                    command.Parameters.AddWithValue("@cas", cas);
                    command.Parameters.AddWithValue("@id_krediti", id_kredit);
                    command.Parameters.AddWithValue("@mesecno_odplacilo", izracun);
                    command.Parameters.AddWithValue("@st_odplacil", cas * 12);
                    command.ExecuteNonQuery();
                }
            }
        }

        KreditiUpo();
    }

    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
        var close = (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        var logut = new Loginanje();
        logut.Logout(Login.ime, Login.userId);
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
        await Task.Delay(2000);
        Uspesno.IsVisible = false;
    }

    /*   private async Task UspesnoKreiranaNarocnina()
       {
           UspesnoUstvarjenaNarocnina.IsVisible = true;
           await Task.Delay(2000);
           UspesnoUstvarjenaNarocnina.IsVisible = false;
       } */

    private async Task KreditDodeljen()
    {
        LabelKredit.IsVisible = true;
        await Task.Delay(2000);
        LabelKredit.IsVisible = false;
    }

    private void UstvariGenericniKredit_OnClick(object? sender, RoutedEventArgs e)
    {
        var register = new Register();
        register.Show();
        Hide();
    }

    /*  private void UstvariNarocnino_OnClick(object? sender, RoutedEventArgs e)
      {
          Profil.IsVisible = false;
          UstvariNarocnino.IsVisible = true;
      }

      private void KreirajNarocnino_OnClick(object? sender, RoutedEventArgs e)
      {
          UstvariNarocnine();
          UspesnoKreiranaNarocnina();
          vseMozneNarocnine();
          Profil.IsVisible = true;
          UstvariNarocnino.IsVisible = false;
      }*/

    private void Expander_OnExpanded(object? sender, RoutedEventArgs e)
    {
        var expander = (Expander)sender;
        var imenaUporabnika = FindInnerListBox(expander);
        imenaUporabnika.ItemsSource = uporabnikiImena;
        Imena();
        if (sender is Expander expande && expande.Tag is Krediti krediti)
        {
            id_kredit = krediti.IDkredita;
            obrest = krediti.ObrestnaMera;

            vsota = krediti.VsotaKredita;
            Console.WriteLine($"ID Kredita: {id_kredit} \n Obrstna Mera: {obrest} \n Vsota Kredita: {vsota}");
        }
    }

    private void DodeliKredit_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is UporabniskiPodatki uporabniskiPodatki)
        {
            id_uporabnik = uporabniskiPodatki.UporabnikID;
            id_kartica = uporabniskiPodatki.IDkartica;
            Console.WriteLine($" ID Uporabnika: {id_uporabnik} \n ID Kartice Uporabnika: {id_kartica}");
        }

        KreditiBorder.IsVisible = false;
        CasOdplacevanja.IsVisible = true;
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

    private void ShraniVBazo_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button)
        {
            if (int.TryParse(button.Tag.ToString(), out cas))
            {
            }
        }

        Console.WriteLine(
            $"ID Kredita: {id_kredit}  ID Uporabnika: {id_uporabnik}  ID Kartice Uporabnika: {id_kartica}");
        CasOdplacevanja.IsVisible = false;
        KreditiBorder.IsVisible = true;
        DodeliKredit();
        KreditDodeljen();

    }


    private void Vecinformacij_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.Tag is UporabniskiPodatki uporabniskiPodatki)
        {
            id_uporabnik = uporabniskiPodatki.UporabnikID;
            Console.WriteLine(id_uporabnik);
            Uporabniki.IsVisible = false;
            VecInformacijUporabnika();
            VecInformacij.IsVisible = true;
        }

    }

    private void Nazaj_OnClick(object? sender, RoutedEventArgs e)
    {
        Uporabniki.IsVisible = true;
        VecInformacij.IsVisible = false;
    }

    private void LogLog_OnClick(object? sender, RoutedEventArgs e)
    {
     
        Profil.IsVisible = false;
        PrijaveBorder.IsVisible = true;
    }

    private void Back_OnClick(object? sender, RoutedEventArgs e)
    {
        Profil.IsVisible = true;
        PrijaveBorder.IsVisible = false;
        
    }
}

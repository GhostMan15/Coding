using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Security;
using Avalonia.Data.Converters;
using System.Globalization;
using System.Threading.Tasks;

namespace NrpaNup;

public partial class Zaposlen : Window
{
    private ObservableCollection<UporabniskiPodatki> uporabniskiPodatki { get; } =
        new ObservableCollection<UporabniskiPodatki>();
    private readonly string _conn;
    //private readonly UporabniskiPodatki _uporabniskiPodatki;
    public Zaposlen()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        PrikazUporabniskihPodatkov();
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

    private void PrikazUporabniskihPodatkov()
    {
        uporabniskiPodatki.Clear();
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
                        int uporabnikId = reader.GetInt32("uporabnik_id");
                        string ime = reader.GetString("uporabnik_ime");
                        string geslo = reader.GetString("uporabnik_geslo");
                        string created = reader.GetString("uporabnik_created");
                        string vrstaUporabnika = reader.GetString("uporabnik_vrsta");
                        int id_kartica = reader.GetInt32("kartica_id");
                        string st_kartice = reader.GetString("kartica_st");
                        string vrsta = reader.GetString("kartica_vrsta");
                        int limit = reader.GetInt32("kartica_limit");
                        string status = reader.GetString("kartica_status");
                        double stanje = reader.GetDouble("kartica_stanje");
                        string veljavnost = reader.GetString("kartica_veljavnost");
                        var uporabniki = new UporabniskiPodatki(uporabnikId,ime, geslo, created, vrstaUporabnika, id_kartica,
                            st_kartice, vrsta, limit, status, stanje, veljavnost);
                        uporabniskiPodatki.Add(uporabniki);
                    }
                }
            }
        }

        UporabnikiBox.ItemsSource = uporabniskiPodatki;
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
        PrikazUporabniskihPodatkov();
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
}

class UporabniskiPodatki : Kartica
{
    public UporabniskiPodatki(){}
    public int UporabnikID { get; set; }
    public string Ime { get; set; }
    public string Geslo { get; set; }
    public string Created { get; set; }
    public string VrstaUporabnika { get; set;}
    
    public UporabniskiPodatki(int uporabnikId,string ime, string geslo, string created, string vrstaUporabnika, int idKartica,
        string stKratice, string vrsta, int limit, string status, double stanje, string veljavnost) : base(idKartica,
        stKratice, vrsta, limit, status, stanje, veljavnost)
    {
        UporabnikID = uporabnikId;
        Ime = ime;
        Geslo = geslo;
        Created = created;
        VrstaUporabnika = vrstaUporabnika;
    }
}

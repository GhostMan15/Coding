using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.ApplicationLifetimes;
using MySqlConnector;

namespace NrpaNup;

public partial class Admin : Window
{
    private ObservableCollection<UporabniskiPodatki> vsiUporabniskiPodatki { get; } =
        new ObservableCollection<UporabniskiPodatki>();
    private readonly string _conn;
    public Admin()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
        PrikazUporabniskihPodatkov();
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
                        vsiUporabniskiPodatki.Add(uporabniki);      
                    }
                }
            }
        }

        //UporabnikiBox.ItemsSource = uporabniskiPodatki;
    }

    private void Logout_OnClick(object? sender, RoutedEventArgs e)
    {
        var close= (IClassicDesktopStyleApplicationLifetime)Application.Current.ApplicationLifetime;
        close.Shutdown();
    }
}
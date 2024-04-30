using System;
using System.Data;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using MySql.Data.MySqlClient;
using Avalonia.Controls.Primitives;
namespace NrpaNup;

public partial class Register : Window
{
    private readonly string _conn;
    private decimal obrest = 0;

    public Register()
    {
        InitializeComponent();
        var reader = new AppSettingsReader("appsettings.json");
        _conn = reader.GetStringValue("ConnectionStrings:MyConnectionString");
    }

    private void UstvariGenericniKredit()
    {
        int vsota = Convert.ToInt32(Vsota.Text.Trim());
        string tip_kredita = TipKredita.Text;
        //decimal fixna = Convert.ToDecimal(Fixna.Text);

        using (MySqlConnection connection = new MySqlConnection(_conn))
        {
            connection.Open();
            using (MySqlCommand command = new MySqlCommand("Ustvari_Genericni_Kredit", connection))
            {
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@vsota", vsota);
                command.Parameters.AddWithValue("@tip_kredita", tip_kredita);
                command.Parameters.AddWithValue("@fixna_obrestna_mera", obrest);
                command.ExecuteNonQuery();
            }
        }

    }

    private void Register_OnClick(object? sender, RoutedEventArgs e)
    {
        UstvariGenericniKredit();
        var zaposlen = new Zaposlen();
        zaposlen.Show();
        Close();
    }

    private void Checked_OnChecked(object? sender, RoutedEventArgs e)
    {
        /*ToggleButton izbran = sender as ToggleButton;
        foreach (var child in ObrestStackPanel.Children)
        {
            if (child is CheckBox toggleButton && toggleButton != sender)
            {
                toggleButton.IsChecked = false;
            }
        }
        if (izbran != null && izbran.IsChecked == true)
        {
            switch (true)
            {
                case true when Obrest2_5.IsChecked == true:
                    obrest = Convert.ToDecimal(2.5);
                    break;
                case true when Obrest5.IsChecked == true:
                    obrest = 5;
                    break;
                case true when Obrest7_5.IsChecked == true:
                    obrest = Convert.ToDecimal(7.5);
                    break;
            }

        }*/
        RadioButton selectedRadioButton = sender as RadioButton;
        if (selectedRadioButton != null && selectedRadioButton.IsChecked == true)
        {
         
            obrest = Convert.ToDecimal(selectedRadioButton.Tag); //mogoc nared da zgledao kt checkboxi
        }


    }

  
}
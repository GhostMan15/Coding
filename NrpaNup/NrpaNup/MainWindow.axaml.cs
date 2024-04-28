using Avalonia.Controls;
using Avalonia.Interactivity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
namespace NrpaNup;

public partial class MainWindow : Window
{
    private string conn; 
    private readonly IConfiguration configuration; 
    public MainWindow()
    {
        InitializeComponent();
        DataContext = this;
        configuration = new ConfigurationBuilder()
  
            .AddJsonFile("conn.json", optional:true)
            .Build();
        conn = configuration.GetConnectionString("MyConnectionString");
    }

    private void LoginButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var login = new Login(configuration);
       //var login = new Login();
        login.Show();
    }
}
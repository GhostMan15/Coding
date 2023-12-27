namespace ProjektFormsNRPA
{

    public class Oseba
    {
        string Ime { get; set; }
        int Pin { get; set; }

        string Id { get; set; }

        public Oseba(string ime, int pin, string id)
        {
            this.Id = id;
            this.Ime = ime;
            this.Pin = pin;
        }



    }

    class Uporabnik
    {

    }

    class Zaposleni
    {

    }

    class ZapisOseb
    {
        private List<Oseba> osebe = new List<Oseba>()
        {
            new Oseba("Duško", 1234, "SI78965DD"),


        };


        public void PreveriPin()
        {

        }
    }

    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}
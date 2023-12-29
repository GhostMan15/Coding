namespace ProjektFormsNRPA
{

    public class Oseba
    {
        public string Ime { get; set; }
        public int Pin { get; set; }

        public string Id { get; set; }


        private static HashSet<int> obstojecPin = new HashSet<int>();

        private bool UnikatenPin(int pin)
        {
            return obstojecPin.Add(pin);
        }


        public Oseba(string ime, int pin, string id)
        {

            if (!UnikatenPin(pin))
            {
                throw new Exception("Pin mora biti unikaten");
            }

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
            new Oseba("Milan",5674,"SI64356DD"),
            new Oseba("Dragan",6547,"SI56239DD"),
        };


        public void PreveriPin(int vnesenPin)
        {
            int i = 0;
            bool pravilenPin = false;
            foreach (Oseba oseba in osebe)
            {
                if (oseba.Pin == vnesenPin)
                {
                    pravilenPin = true;
                    break;
                }
                i++;
            }
            if (pravilenPin)
            {
                MessageBox.Show("Vstop odobren");
                Form2 form2 = new Form2(osebe[i]);
                form2.Show();

            }
            else
            {
                MessageBox.Show("Nepravilen pin \n Vstop zavrnjen");
            }
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
namespace ProjektFormsNRPA
{

    public class Oseba
    {
        public string Ime { get; set; }
        public int Pin { get; set; }
        public string Id { get; set; }
        public bool Zaposlen { get; set; }

        private static HashSet<int> obstojecPin = new HashSet<int>();

        public static bool UnikatenPin(int pin)
        {
            return !obstojecPin.Contains(pin);
        }

        public void ZapisOsebe()
        {
            if (obstojecPin == null)
            {
                obstojecPin = new HashSet<int>();
            }
        }

        public Oseba(string ime, int pin, string id, bool zaposlen)
        {

            if (!UnikatenPin(pin))
            {
                throw new Exception("Pin mora biti unikaten");
                
            }

            this.Id = GeneririId();
            this.Ime = ime;
            this.Pin = pin;
            this.Zaposlen = zaposlen;
            obstojecPin.Add(pin); 
        }

        private string GeneririId()
        {
            Random random = new Random();
            string randomNumbers = random.Next(10000, 99999).ToString();
            return $"SI{randomNumbers}DD";
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
        private string filePath = "C:\\Users\\faruk\\source\\repos\\Coding\\ProjektFormsNRPA\\ProjektFormsNRPA\\bin\\Debug\\net7.0-windows\\Osebe.txt";

        public static List<Oseba> osebe = new List<Oseba>()
        {
            new Oseba("Duško", 1234, "SI78965DD", false),
            new Oseba("Milan",5674,"SI64356DD", false),
            new Oseba("Dragan",6547,"SI56239DD", false),
            new Oseba("Janko",5555,"SI73856DD",true),
        };

        public void ShraniOsebeVFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.filePath))
                {
                    foreach (Oseba oseba in osebe)
                    {
                        writer.WriteLine($"{oseba.Ime},{oseba.Pin},{oseba.Id},{oseba.Zaposlen}");
                    }
                }
                MessageBox.Show("Oseba uspešno sharnjena");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka pri shranjevanju: {ex.Message}");
            }
        }
        public void PreberiOsebeIzFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    osebe.Clear();
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                           string nov = reader.ReadLine();
                           if (nov != null)
                            {
                                string[] razdeli = nov.Split(',');
                                string ime = razdeli[0];
                                int pin = int.Parse(razdeli[1]);
                                string id = razdeli[2];
                                bool zaposlen = bool.Parse(razdeli[3]);

                                Oseba novaOseba = new Oseba(ime, pin, id, zaposlen);
                                osebe.Add(novaOseba);
                            }
                        }
                    }
                    MessageBox.Show("Uspešno naložena oseba");
                }
                else
                {
                    MessageBox.Show("File ne obstaja.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napraka pri nalaganju osebe: {ex.Message}");
            }
        }

        public void DodajOsebo(string ime, int pin, string id, bool zaposlen, Form3 form3)
        {
            if (!Oseba.UnikatenPin(pin))
            {
                MessageBox.Show("Pin že obstaja.\n Prosim vnesite unikaten pin");
                return;
            }
            osebe.Add(new Oseba(ime, pin, id, zaposlen));
            form3.Close();
        }
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
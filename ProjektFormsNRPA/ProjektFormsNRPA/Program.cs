using System.ComponentModel;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProjektFormsNRPA
{

    public class Oseba
    {
        public string Ime { get; set; }
        public int Pin { get; set; }
        public string Id { get; set; }
        public bool Zaposlen { get; set; }
        public float Stanje { get; set; }
        
        public BindingList<Transakcija> Transakcije { get; set; }

        public static HashSet<int> obstojecPin = new HashSet<int>();



        public static bool UnikatenPin(int pin)
        {
            return !obstojecPin.Contains(pin);
        }


        public Oseba(string ime, int pin, string id, bool zaposlen, float stanje)
        {

            if (!UnikatenPin(pin))
            {
                throw new Exception("Pin mora biti unikaten");

            }

            this.Id = id;
            this.Ime = ime;
            this.Pin = pin;
            this.Zaposlen = zaposlen;
            this.Stanje = stanje;
            obstojecPin.Add(pin);
        }

    
    }

    class Uporabnik : Oseba
    {
        public Uporabnik(string ime, int pin, string id, float stanje) : base(ime, pin, id, zaposlen: false, stanje)
        {

        }

        public virtual void PredstaviSe()
        {
           
        }


        public void IzvediTransakcijo(float znesek, Uporabnik prejemnik = null)
        {
            if (znesek <= 0)
            {

            }
        }
    }

    class Zaposleni : Oseba
    {
        public Zaposleni(string ime, int pin, string id, float stanje) : base(ime, pin, id,zaposlen: true, stanje)
        {

        }
    }

    class ZapisOseb
    {
        private string filePath = "Osebe.txt";

        public static List<Oseba> osebe = new List<Oseba>();
        public ZapisOseb()
        {
            PreberiOsebeIzFile();
            if (osebe.Count == 0)
            {
                osebe.Add(new Oseba("Janko", 5555, "SI73856DD", true, 300));
                
            }
        }

        public static Oseba PoisciOsebo(int pin)
        {
            Oseba poisciOsebo = osebe.FirstOrDefault(o => o.Pin == pin);
            if (poisciOsebo == null)
            {
                throw new Exception("Oseban ni najdena");
            }
            return poisciOsebo;
        }

        public void ShraniOsebeVFile()
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(this.filePath))
                {
                    foreach (Oseba oseba in osebe)
                    {
                        writer.WriteLine($"{oseba.Ime},{oseba.Pin},{oseba.Id},{oseba.Zaposlen},{oseba.Stanje}");
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
                Oseba.obstojecPin.Clear();
                if (File.Exists(filePath))
                {
                    

                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                           string nov = reader.ReadLine();
                           if (!string.IsNullOrEmpty(nov))
                            {
                                string[] razdeli = nov.Split(',');
                                string ime = razdeli[0];
                                int pin = int.Parse(razdeli[1]);
                                string id = razdeli[2];
                                bool zaposlen = bool.Parse(razdeli[3]);
                                float stanje = float.Parse(razdeli[4]);

                                Oseba novaOseba = new Oseba(ime, pin, id, zaposlen, stanje);
                                osebe.Add(novaOseba);
                                Oseba.obstojecPin.Add(pin);
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

        public void DodajOsebo(string ime, int pin, string id, bool zaposlen, Form3 form3, float stanje)
        {
            if (!Oseba.UnikatenPin(pin))
            {
                MessageBox.Show("Pin že obstaja.\n Prosim vnesite unikaten pin");
                return;
            }
            osebe.Add(new Oseba(ime, pin, id, zaposlen, stanje));
            form3.Close();
        }
        public void PreveriPin(int vnesenPin)
        {

            PreberiOsebeIzFile();
            if (osebe.Count == 0)
            {
                MessageBox.Show(".");
                return;
            }
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
            if (pravilenPin && i< osebe.Count)
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

    class Transakcija
    {
        public DateTime Datum { get; set; }
        public string Opis { get; set; }
        public float Znesek { get; set; }
        public Oseba _oseba { get; set; }

        public Transakcija(DateTime datum, string opis, float znesek, Oseba _oseba)
        {
            this.Datum = datum;
            this.Opis = opis;
            this.Znesek = znesek;
            this._oseba = _oseba;
        }
        public void IzvediTransakcijo(Oseba oseba)
        {
            if (Znesek <= 0)
            {
                MessageBox.Show("Znesek mora biti pozitiven.");
                return;
            }
            if (Opis.StartsWith("Nakaži"))
            {
                _oseba.Stanje += Znesek;
            }
            else if (Opis.StartsWith("Dvig"))
            {
                if (_oseba.Stanje >= Math.Abs(Znesek))
                {
                    _oseba.Stanje -= Math.Abs(Znesek);
                }
                else
                {
                    MessageBox.Show("Nimate dovolj sredstev za dvig");
                    return;
                }
            }
        }
        public void PrikaziPodatke(Transakcija transakcija,ListBox transakcije)
        {
            transakcije.Items.Add($"Datum: {transakcija.Datum}, Opis: {transakcija.Opis}, Znesek: {transakcija.Znesek} €");
        }
        public override string ToString()
        {
            return $"{Datum}: {Opis}";
        }


        private static string filePath = "Transakcije.txt";


        public static void ShraniTransakcijoVFile(List<Transakcija> transakcije, string filePath)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    foreach (var transakcija in transakcije)
                    {
                        writer.WriteLine(transakcija.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka pri shranjevanju transakcije: {ex.Message}");
            }
        }

        public static List<Transakcija> NaloziIzFile(string filePath)
        {
            List<Transakcija> transakcije = new List<Transakcija>();
            try
            {
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        while (!reader.EndOfStream)
                        {
                            string line = reader.ReadLine();
                            if (!string.IsNullOrEmpty(line))
                            {
                                string[] deli = line.Split(',');
                                if (deli.Length == 3 && DateTime.TryParse(deli[0], out DateTime datum) && float.TryParse(deli[1], out float zensek) && !string.IsNullOrEmpty(deli[2]))
                                {
                                    if (deli.Length >= 4 && int.TryParse(deli[3], out int pin))
                                    {
                                        Oseba poisciOsebo = ZapisOseb.PoisciOsebo(pin);
                                        transakcije.Add(new Transakcija(datum, deli[2], zensek, poisciOsebo));
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show($"Problem pri nalaganju transakcije: {e.Message}");
            }
            return transakcije;
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
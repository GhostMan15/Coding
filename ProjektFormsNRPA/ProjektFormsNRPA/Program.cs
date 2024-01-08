using Microsoft.VisualBasic.ApplicationServices;
using System.ComponentModel;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing.Text;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.AxHost;
using Banka;

namespace ProjektFormsNRPA
{

    public class Oseba
    {
        public string Ime { get; set; }
        public int Pin { get; set; }
        public string Id { get; set; }
        public  bool Zaposlen { get; set; }
        public float Stanje { get; set; }
        
       // public BindingList<Transakcija> Transakcije { get; set; }

        public static readonly HashSet<int> obstojecPin = new HashSet<int>();
     
        


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
        public virtual void PredstaviSe()
        {
            MessageBox.Show($"Ime: {Ime}, ID: {Id}, Zaposlen: {Zaposlen}, Stanje: {Stanje} �");
        }
        ~Oseba()
        {
            MessageBox.Show($"Oseba z id: {Id} je bila uni�ena");

        }
      
    }

    class Uporabnik : Oseba
    {
        public Uporabnik(string ime, int pin, string id, float stanje) : base(ime, pin, id, zaposlen: false, stanje)
        {

        }

        public override void PredstaviSe()
        {
            base.PredstaviSe();
            MessageBox.Show("Uporabnik");
        }
    }

    class Zaposleni : Oseba
    {
        public Zaposleni(string ime, int pin, string id, float stanje) : base(ime, pin, id, zaposlen: true, stanje)
        {

        }
        public override void PredstaviSe()
        {
            base.PredstaviSe();
            MessageBox.Show("Zaposleni");
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
                osebe.Add(new Zaposleni("On", 5346, "SI41345DD",  300));
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
                MessageBox.Show("Oseba uspe�no sharnjena");
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

                                if (zaposlen)
                                {
                                    Zaposleni zaposleni = new Zaposleni(ime, pin, id, stanje);
                                    osebe.Add(zaposleni);
                                }
                                else
                                {
                                    Oseba novaOseba = new Oseba(ime, pin, id, zaposlen, stanje);
                                    osebe.Add(novaOseba);
                                }
                                Oseba.obstojecPin.Add(pin);
                            }
                        }
                    }
                    
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
        public static void UstvariTransakcijskiFile(string id)
        {
            string filePath = $"{id}_Transakcije.txt";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }
        }
     
        public void DodajOsebo(string ime, int pin, string id, bool zaposlen, Form3 form3, float stanje)
        {
            if (!Oseba.UnikatenPin(pin))
            {
                MessageBox.Show("Pin �e obstaja.\n Prosim vnesite unikaten pin");
                return;
            }
            osebe.Add(new Oseba(ime, pin, id, zaposlen, stanje));
            UstvariTransakcijskiFile(id);
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
                Form2 form2 = new(osebe[i]);
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
        public Oseba _Oseba { get; set; }

        public Transakcija(DateTime datum, string opis, float znesek, Oseba oseba)
        {
            this.Datum = datum;
            this.Opis = opis;
            this.Znesek = znesek;
            this._Oseba = oseba;

        }
        public virtual bool IzvediTransakcijo(Oseba oseba, Oseba user, Transakcija transakcija, List<Transakcija> transakcijež)
        {
            MessageBox.Show($"{Opis}");
            ShraniTransakcijoVFile(oseba, transakcija, new List<Transakcija>());
            return true;
        }

        //public void IzvediTransakcijo(Oseba oseba, Oseba user, Transakcija transakcija, List<Transakcija> transakcije�)
        //{
        //    if (Znesek <= 0)
        //    {
        //        MessageBox.Show("Znesek mora biti pozitiven.");
        //        return;
        //    }
        //    if (Opis.StartsWith("Naka�i"))
        //    {
        //        oseba.Stanje += Znesek;
        //    }
        //    else if (Opis.StartsWith("Dvig"))
        //    {
        //        if (oseba.Stanje >= Math.Abs(Znesek))
        //        {
        //            oseba.Stanje -= Math.Abs(Znesek);
        //        }
        //        else
        //        {
        //            MessageBox.Show("Nimate dovolj sredstev za dvig");
        //            return;
        //        }
        //    }
        //    ShraniTransakcijoVFile(user, transakcija, transakcije�);
        //}
        //public void PrikaziPodatke(Transakcija transakcija, ListBox transakcije)
        //{
        //    transakcije.Items.Add($"Datum: {transakcija.Datum}, Opis: {transakcija.Opis}, Znesek: {transakcija.Znesek} �");
        //}
        public virtual string PrikaziPodatke(ListBox transakcije)
        {
            return $"{Datum}: {Opis}, Znesek: {Znesek} �";
           
        }

        public static void ShraniTransakcijoVFile(Oseba user, Transakcija transakcija, List<Transakcija> transakcijež)
        {
            string filePath = $"{user.Id}_Transakcije.txt";
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath, true))
                {
                    foreach (Transakcija t in transakcijež)
                    {
                        writer.WriteLine($"{transakcija.Datum},{transakcija.Opis},{transakcija.Znesek}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Napaka pri shranjevanju transakcije: {ex.Message}");
            }
        }

        public static List<Transakcija> NaloziIzFile(string filePath, int pin)
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
                                if (deli.Length == 3 && DateTime.TryParse(deli[0], out DateTime datum ))     
                                {
                                        float zensek = float.Parse(deli[2]);      
                                        Oseba poisciOsebo = ZapisOseb.PoisciOsebo(pin);
                                        transakcije.Add(new Transakcija(datum, deli[1], zensek, poisciOsebo));
                                        
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
        public class Nakazilo : Transakcija
        {
            public Nakazilo(DateTime datum, float znesek, Oseba oseba) : base(datum, $"Nakaži: {znesek} €", znesek, oseba)
            {
            }

            public override bool IzvediTransakcijo(Oseba oseba, Oseba user, Transakcija transakcija, List<Transakcija> transakcijež)
            {
                base.IzvediTransakcijo(oseba,user,transakcija,transakcijež);
                _Oseba.Stanje += Znesek;
                return true;
            }

            public override string PrikaziPodatke(ListBox transakcije)
            {
                return base.PrikaziPodatke(transakcije) + " (Nakazilo)";
            }
        }

        public class Dvig : Transakcija
        {
            public Dvig(DateTime datum, float znesek, Oseba oseba) : base(datum, $"Dvig: {znesek} €", znesek, oseba)
            {
            }

            public override bool IzvediTransakcijo(Oseba oseba, Oseba user, Transakcija transakcija, List<Transakcija> transakcijež)
            {
                base.IzvediTransakcijo(oseba, user, transakcija, transakcijež);
                if (_Oseba.Stanje >= Math.Abs(Znesek))
                {
                    _Oseba.Stanje -= Math.Abs(Znesek);
                    return true;

                }
                else
                {
                    MessageBox.Show("Nimate dovolj sredstev za dvig");
                    return false;   
                }
            }

            public override string PrikaziPodatke(ListBox transakcije)
            {
                return base.PrikaziPodatke(transakcije) + " (Dvig)";
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFormsNRPA
{
    internal class ZapisOseb
    {
        private string filePath = "Osebe.txt";

        public static List<Oseba> osebe = new List<Oseba>();
        public ZapisOseb()
        {
            PreberiOsebeIzFile();
            if (osebe.Count == 0)
            {
                osebe.Add(new Oseba("fds", 5634, "SI73856DD", true, 300));
                osebe.Add(new Zaposleni("On", 5346, "SI41345DD", 300));
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
                MessageBox.Show("Pin ne obstaja.\n Prosim vnesite unikaten pin");
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
            if (pravilenPin && i < osebe.Count)
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

}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFormsNRPA
{
    internal class Transakcija
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
            return $"{Datum}: {Opis}, Znesek: {Znesek} €";

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
                                if (deli.Length == 3 && DateTime.TryParse(deli[0], out DateTime datum))
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
                base.IzvediTransakcijo(oseba, user, transakcija, transakcijež);
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
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektFormsNRPA
{
    class Oseba
    {

        public string Ime { get; set; }
        public int Pin { get; set; }
        public string Id { get; set; }
        public bool Zaposlen { get; set; }
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
            MessageBox.Show($"Oseba z id: {Id} je bila uničena");

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
}

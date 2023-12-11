using System;
using System.Buffers;

namespace Projekt
{
    class Oseba
    {
        public string Ime { get; set; }
        public int Starost { get; set; }



        ~Oseba()
        {
            Console.WriteLine($"Oseba {Ime} je zbrisana");
        }
    }



    class Uporabnik : Oseba
    {
        public void DodajTransakcijo(Transakcija transakcija)
        {

        }

        public virtual void PrikaziPodatke()
        {
            
        }
    }

    class Racun : Transakcija
    {
        public void IzracunajStanje()
        {

        }
        private void PrikaziStanje()
        {

        }
    }

    class Transakcija 
    {
        private void PrikaziPodatke()
        {

        }
    }
}


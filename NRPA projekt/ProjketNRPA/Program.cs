using System;
using System.Buffers;

namespace Projekt
{
    class Oseba                      
    {
        public string Ime { get; set; } 	//Lahko je Zaposleni ali Uporabnik
        public int Starost { get; set; }


          public  Oseba + (Oseba oseba 1, Oseba, oseba 2)
	  {
		  return Oseba ($"Uporabik: {oseba1.Ime} in {oseba2.Ime};
	  }
        ~Oseba()
        {
            Console.WriteLine($"Oseba {Ime} je zbrisana");   
        }
    }



    class Uporabnik : Oseba  
    {
	    
        public void DodajTransakcijo(Transakcija transakcija) //Dodajanje transakcij uporabniku
        {

       }

        public virtual void PrikaziPodatke()
        {
            
        }
    }

    class Racun 
    {
        public void IzracunajStanje() //Izracun in prikaz trenutnega stanja
        {

        }
        private void PrikaziStanje()
        {

        }
    }

    class Transakcija 
    {
        private void PrikaziPodatke() //Prikaz posamezne transakcije
        {
		float znesek;
		DateTime datum;
        }
    }
}


using System;
using System.Buffers;
using Projekt;

namespace Projekt
{
    class Oseba                      
    {
        public string Ime { get; set; } 	//Lahko je Zaposleni ali Uporabnik
        public int Starost { get; set; }


          public  Oseba + (Oseba oseba 1, Oseba, oseba 2) //Preoblikovanje operatorjev (Ugibam?)
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
        public virtual void PredstaviSe()
        {
            Console.WriteLine($"Ime:{Ime}/n Starost:{Starost}/n Tip porabnika:Navadni uporabnik ");
        }
	   
        public void DodajTransakcijo(Transakcija transakcija) //Dodajanje transakcij uporabniku
        {

        }

        public virtual void PrikaziPodatke()
        {
            
        }
    }

    class Zaposleni : Oseba  //Ima pogled v uporabnike in njihove transakcije
    {
        public override void PredstaviSe()
        {
            Console.WriteLine($"Ime:{Ime}/n Starost:{Starost}/n Tip porabnika: Zaposleni ");
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


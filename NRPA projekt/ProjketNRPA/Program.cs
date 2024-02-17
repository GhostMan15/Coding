using System;
using System.Buffers;


namespace Projekt
{
    class Oseba                      
    {
        public string Ime { get; set; } 	//Lahko je Zaposleni ali Uporabnik
        public int Starost { get; set; }


          public  static Oseba operator +(Oseba oseba 1, Oseba oseba 2) =>  new Oseba ($"Uporabik: {oseba1.Ime} * {oseba2.Ime}; //Preoblikovanje operatorjev (Ugibam?)
	    
        ~Oseba()
        {
            Console.WriteLine($"Oseba {Ime} je zbrisana");
        }

	
    }



    class Uporabnik : Oseba  
    {
        public void PredstaviSe()
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



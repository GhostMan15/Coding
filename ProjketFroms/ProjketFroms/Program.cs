using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ProjketFroms
{

     class Oseba
    {
        string ime { get; set; }
        int pin { get; set; }

        string id { get; set; }

        public Oseba(string ime, int pin, string id)
        {
            this.id = id;
            this.ime = ime;
            this.pin = pin;
        }

       

    }

    class Uporabnik 
    {

    }

    class Zaposleni
    {

    }


    


  
        
    internal  class Program
    {
      
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
            

            private List<Oseba> osebe = new List<Oseba>()
            {
                new Oseba ("Duško",1234,"SI2465"),
            };
       }
    }
}
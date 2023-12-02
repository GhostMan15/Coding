using System;
using System.ComponentModel.Design;
using System.Globalization;

namespace Yolo
{


    class Planet
    {
        string name;
        double gravity;
        public Planet(string n, double g)
        {
            name = n;
            gravity = g;
        }
        public string Name
        {
            get { return name; }
        }
        public double Gravity
        {
            get { return gravity; }
        }
    }
    class Astronaut
    {
        double fitness;
        Planet curruentPlanet;
        public Astronaut(double f)
        {
            fitness = f;
        }
        public Planet CurrentPlanet
        {
            get { return curruentPlanet; }
            set { curruentPlanet = value; }
        }
        public void Jump()
        {
            if (curruentPlanet == null)
            {
                Console.WriteLine("Bye");
            }
            else
            {
                double distance = fitness / curruentPlanet.Gravity;
                Console.WriteLine("Jumped {0} meters on {1}", distance, curruentPlanet.Name);
            }
        }
    }
    class Test
    {
        static void Main()
        {
            Planet erath = new Planet("earth", 9.8);
            Planet moon = new Planet("moon", 1.6);
            Astronaut forestGump = new Astronaut(20);
            forestGump.CurrentPlanet = erath;
            forestGump.Jump();
            forestGump.CurrentPlanet = moon;
            forestGump.Jump();
            Console.ReadLine(); 
        }
    }
}

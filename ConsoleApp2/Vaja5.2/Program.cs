using System;

namespace ConsoleApp2
{
    public class Krog
    { 
        public double Premer { get; set; }
        public Krog(double premer)
        {
            Premer = premer;
        }
        public double Povrsina()
        {
            double polmer = Premer / 2;
            double povrsina = Math.PI * Math.Pow(polmer, 2);
            return polmer;
        }

    }
    class Krogla : Krog
    {
        public Krogla(double premer) : base(premer) 
        {

        }
        public new double Povrsina()
        {
            double polmer = Premer / 2;
            double povrsina = 4*Math.PI * Math.Pow(polmer, 2);
            return povrsina;
        }
        public new double Prostornina()
        {
            double polmer = Premer / 2;
            double prostornina = (4.0 / 3.0) * Math.PI * Math.Pow(polmer, 3);
            return prostornina;
        }
    }
}

namespace Vaja5
{
    class Pes
    {
        private string ime;
        private int teza;

        public void Izpis(string i, int t)
        {
            this.ime = i;
            this.teza = t;
        }

    }
    class Dalmatinec
    {
        private int stPik;
        public void Nastavi(int stPik, int t, string i)
        {
            base.Nastavi(i, t);

            i = new string("Roni");
            t = new int();
            
            stPik = 500;

        }
    }
    class Glavni
    {
        static void Main(string[] args)
        {

        }
    }

}
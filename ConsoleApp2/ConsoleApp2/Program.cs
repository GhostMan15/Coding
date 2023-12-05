namespace ConsoleApp2
{
    class Pes
    {
        private string ime;
        private int teza;

        public void Nastavi(string i, int t)
        {
            this.ime = i;
            this.teza = t;
           
        }
        public void Izpis()
        {
            Console.Write($"Ime : {ime}, Teža: {teza} kg, " );
        }

    }
    class Dalmatinec : Pes
    {

        public int stPik;
      
        public void Nastavi(int stPik, int t, string i)
        {
            base.Nastavi(i, t);
            this.stPik = stPik;


        }
        public void Izpisi()
        {
            base.Izpis();
            Console.Write($"Število pik {stPik}");
        }
    }
    class Glavni
    {
        static void Main(string[] args)
        {
            Dalmatinec dalmatinec = new Dalmatinec();
            dalmatinec.Nastavi (1001,25,"Roni");
            dalmatinec.Izpisi();

            Console.ReadLine();
        }
    }

}
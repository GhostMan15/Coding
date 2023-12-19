namespace ProjketFroms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
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

        class Uporabnik : Oseba
        {

        }

        class Zaposleni
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        
            static void Main(string[] args)
            {

            List<Oseba> osebe = new List<Oseba>()
                 {
                   new Oseba("Duško", 1234, "SI78965DD"),
                   
                   
                 };
        }
    }
}

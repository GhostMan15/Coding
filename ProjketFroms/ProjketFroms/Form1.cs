namespace ProjketFroms
{
    public partial class Form1 : Form
    {
        private List<Oseba> osebe;
        public Form1()
        {
             
            InitializeComponent();

           osebe = new List<Oseba>()
            {
                   new Oseba("Duško", 1234, "SI78965DD"),


            };
        
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                 
        }
        
       
    }
}
    
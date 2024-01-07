using Banka;


namespace ProjektFormsNRPA
{
    public partial class Form1 : Form
    {
        private ZapisOseb _zapisOseb;
        public Form1()
        {
            InitializeComponent();
            _zapisOseb = new ZapisOseb();
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CekiriPin();
            this.Hide();
        }

        private void CekiriPin()
        {
            string vnesenPin = textBox1.Text;
            if (int.TryParse(vnesenPin, out int pin)) //converta pin v int 
            {
                _zapisOseb.PreveriPin(pin);
                MessageBox.Show($"Dobrodošli v {Konstane.ImeBanke}");
               
            }
            else
            {
                MessageBox.Show("Nepravilen pin \n Vstop zavrnjen");
            }
        }

        
    }
}

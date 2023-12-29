namespace ProjektFormsNRPA
{
    public partial class Form1 : Form
    {
        private ZapisOseb zapisOseb;
        public Form1()
        {
            InitializeComponent();
            zapisOseb = new ZapisOseb();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CekiriPin();
            this.Hide();
        }

        private void CekiriPin()
        {
            string vnesenPin = textBox1.Text;
            if (int.TryParse(vnesenPin, out int pin))
            {
                zapisOseb.PreveriPin(pin);
              
            }
            else
            {
                MessageBox.Show("Nepravilen pin \n Vstop zavrnjen");
            }
        }

        
    }
}

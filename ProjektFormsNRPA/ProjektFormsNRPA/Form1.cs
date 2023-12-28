namespace ProjektFormsNRPA
{
    public partial class Form1 : Form
    {
        private ZapisOseb zapisOseb;
        public Form1()
        {
            InitializeComponent();
            zapisOseb = new ZapisOseb();
            button1.Click += button1_Click;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CekiriPin();
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

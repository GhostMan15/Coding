namespace ProjektFormsNRPA
{
    public partial class Form1 : Form
    {
        private ZapisOseb _zapisOseb;
        public Form1()
        {
            InitializeComponent();
            _zapisOseb = new ZapisOseb();
            List<Oseba> osebeList = _zapisOseb.VrniVseOsebe();
            foreach (var oseba in osebeList)
            {
                oseba.PredstaviSe(); // This will call the overridden method in SpecialOseba if the object is of that type
            }

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
              
            }
            else
            {
                MessageBox.Show("Nepravilen pin \n Vstop zavrnjen");
            }
        }

        
    }
}

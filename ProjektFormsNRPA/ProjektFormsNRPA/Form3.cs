using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjektFormsNRPA
{
    public partial class Form3 : Form
    {
        private ZapisOseb _zapisOseb;
        public Form3()
        {
            InitializeComponent();
            _zapisOseb = new ZapisOseb();
        }

        private void Dodaj_Click(object sender, EventArgs e)
        {
            string ime = Ime.Text;
            string pinText = nPin.Text;

            if (int.TryParse(pinText, out int pin))
            {
                _zapisOseb.DodajOsebo(ime, pin, string.Empty, checkBox1.Checked, this);
                _zapisOseb.ShraniOsebeVFile();
                _zapisOseb.PreberiOsebeIzFile();
            }

        }
    }
}

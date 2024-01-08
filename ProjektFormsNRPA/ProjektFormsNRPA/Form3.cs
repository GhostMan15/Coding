using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Banka;

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
            string id = Id.Text;
            string stanje = Stanje.Text;

            if (int.TryParse(pinText, out int pin))
            {
                _zapisOseb.DodajOsebo(ime, pin, id, checkBox1.Checked, this, float.Parse(stanje));
                _zapisOseb.ShraniOsebeVFile();
                _zapisOseb.PreberiOsebeIzFile();

                Ime.Text = "";
                nPin.Text = "";
                Id.Text = "";
                Stanje.Text = "";
                checkBox1.Checked = false;
            }
            else
            {
                MessageBox.Show("Vnesite obvezne podatke");
            }

        }
    }
}

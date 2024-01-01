using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ProjektFormsNRPA
{
    public partial class Form2 : Form
    {
        private Oseba _oseba;
        public Form2(Oseba osebe)
        {
            InitializeComponent();
            _oseba = osebe;
            ime.Text += osebe.Ime;
            id.Text += osebe.Id;

            Zaposleni.Visible = _oseba.Zaposlen;
        }

        private void Zaposleni_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }
    }
}

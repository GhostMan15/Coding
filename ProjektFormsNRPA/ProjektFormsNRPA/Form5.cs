using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ProjektFormsNRPA.Transakcija;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.DataFormats;

namespace ProjektFormsNRPA
{
    public partial class Form5 : Form
    {
        public string Znesek { get; private set; }
        private readonly Oseba _oseba;
        public ListBox transakcije;
        private static string GetTransactionFilePath(Oseba oseba)
        {
            return $"{oseba.Id}_Transakcije.txt";
        }

        public Form5(Oseba oseba, ListBox transakcije)
        {
            InitializeComponent();
            _oseba = oseba;
            this.transakcije = transakcije;

        }


        private void dvigP_Click(object sender, EventArgs e)
        {

            if (_oseba != null)
            {
                if (float.TryParse(dvig.Text, out float znesek))
                {
                    Dvig transakcija = new(DateTime.Now, znesek, _oseba);
                    string filePath = GetTransactionFilePath(_oseba);

                    if (transakcija.IzvediTransakcijo(_oseba, _oseba, transakcija, new List<Transakcija>()))
                    {


                        List<Transakcija> transactions = new List<Transakcija> { transakcija };
                        Transakcija.ShraniTransakcijoVFile(_oseba, transakcija, transactions);
                        Znesek = znesek.ToString();
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Transakcijo ni bilo mogoče sporcesuirati.");
                    }
                }
                else
                {
                    MessageBox.Show("Vnesite validen znesek");
                }
            }
            else
            {
                MessageBox.Show("_oseba is null.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

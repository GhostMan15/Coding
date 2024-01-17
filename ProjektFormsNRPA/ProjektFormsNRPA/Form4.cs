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
    public partial class Form4 : Form
    {
        public string Znesek { get; private set; }
        private readonly Oseba _oseba;
        public ListBox transakcije;
        private static string GetTransactionFilePath(Oseba oseba)
        {
            return $"{oseba.Id}_Transakcije.txt";
        }
        internal Form4(Oseba _oseba, ListBox transakcije)
        {
            InitializeComponent();
            this._oseba = _oseba;
            this.transakcije = transakcije;
        }

        private void nakaziP_Click(object sender, EventArgs e)
        {
            if (_oseba != null)
            {
                if (float.TryParse(nakazi.Text, out float znesek))
                {
                    Transakcija.Nakazilo transakcija = new(DateTime.Now,znesek, _oseba);
                    string filePath = GetTransactionFilePath(_oseba);

                    if (transakcija != null)
                    {
                        
                        transakcija.IzvediTransakcijo(_oseba,_oseba,transakcija,new List<Transakcija>());

                        List<Transakcija> transactions = new List<Transakcija> { transakcija };
                        Transakcija.ShraniTransakcijoVFile(_oseba, transakcija, transactions);
                        Znesek = znesek.ToString();
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Transakcijo ni bilo mogoče sprocesuirati .");
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
       

       
    }
}

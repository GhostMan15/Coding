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
    public partial class Form4 : Form
    {
        public string Znesek { get; private set; }
        private Oseba _oseba;
        public ListBox transakcije;
        public event EventHandler UpdateTransakcije;
        private static string GetTransactionFilePath(Oseba oseba)
        {
            return $"{oseba.Id}_Transakcije.txt";
        }
        public Form4(Oseba _oseba, ListBox transakcije)
        {
            InitializeComponent();
            // transakcije = transakcije ?? new ListBox();
            this._oseba = _oseba;
            this.transakcije = transakcije;
        }

        private void nakaziP_Click(object sender, EventArgs e)
        {
            if (_oseba != null)
            {
                if (float.TryParse(nakazi.Text, out float znesek))
                {
                    Transakcija transakcija = new Transakcija(DateTime.Now, $"Nakaži: {znesek} €", znesek, _oseba);
                    string filePath = GetTransactionFilePath(_oseba);

                    if (transakcija != null)
                    {
                        transakcija.IzvediTransakcijo(_oseba, _oseba, transakcija, new List<Transakcija>());

                        List<Transakcija> transactions = new List<Transakcija> { transakcija };
                        Transakcija.ShraniTransakcijoVFile(_oseba, transakcija, transactions);

                        Znesek = znesek.ToString();
                        DialogResult = DialogResult.OK;
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Transakcija is null.");
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

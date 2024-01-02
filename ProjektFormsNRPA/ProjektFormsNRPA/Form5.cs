using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;

namespace ProjektFormsNRPA
{
    public partial class Form5 : Form
    {
        public string Znesek { get; private set; }
        private Oseba _oseba;
        public ListBox transakcije;
        public event EventHandler UpdateTransakcije;
        private static string filePath = "Transakcije.txt";
        public Form5(Oseba oseba, ListBox transakcije)
        {
            InitializeComponent();
            _oseba = oseba;
            this.transakcije = transakcije;
        }

        private void dvigP_Click(object sender, EventArgs e)
        {
            if (float.TryParse(dvig.Text, out float znesek))
            {
                Transakcija transakcija = new Transakcija(DateTime.Now, $"Dvig: {znesek} €", znesek, _oseba);
                transakcija.IzvediTransakcijo(_oseba);
                transakcija.PrikaziPodatke(transakcija,transakcije);

                Transakcija.ShraniTransakcijoVFile(new List<Transakcija> { transakcija }, filePath);
                UpdateTransakcije?.Invoke(this, EventArgs.Empty);
                Znesek = znesek.ToString();
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                MessageBox.Show("Vnesite validen znesek");
            }
        }
    }
}

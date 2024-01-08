using Banka;
using Microsoft.VisualBasic.ApplicationServices;

namespace ProjektFormsNRPA
{
    public partial class Form2 : Form
    {
        private Oseba _oseba;
        public string Znesek { get; private set; }
        public EventHandler UpdateTransakcije;
        private Transakcija transakcija { get; set; }
        private static string GetTransactionFilePath(Oseba oseba)
        {
            return $"{oseba.Id}_Transakcije.txt";
        }
        public Form2(Oseba osebe)
        {
            InitializeComponent();
            _oseba = osebe;
            ime.Text += osebe.Ime;
            id.Text += osebe.Id;
            stanje.Text += osebe.Stanje;
            transakcije.Text += osebe;
            Zaposleni.Visible = _oseba.Zaposlen;
            PrikaziTransakcije();
           
          
        }

        private void Zaposleni_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void transakcije_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void nakazi_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4(_oseba, transakcije);
            form4.ShowDialog();
            _oseba.Stanje += float.Parse(form4.Znesek);
            stanje.Text = $"Stanje: {_oseba.Stanje} €";
               
            
            //doda transakcijo v file
            //refresh/add v listbox

        }
        private void dvig_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(_oseba, transakcije);
            form5.ShowDialog();
            _oseba.Stanje -= float.Parse(form5.Znesek);
            stanje.Text = $"Stanje: {_oseba.Stanje} €";


        }

        private void PrikaziTransakcije() 
        {
            transakcije.Items.Clear();
            var objekt = Transakcija.NaloziIzFile(GetTransactionFilePath(_oseba), _oseba.Pin);
            foreach (var item in objekt)
            {
                transakcije.Items.Add(item.PrikaziPodatke(transakcija,transakcije));
            }
            
        }
    }

}

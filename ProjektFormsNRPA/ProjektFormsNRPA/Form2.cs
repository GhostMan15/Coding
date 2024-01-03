namespace ProjektFormsNRPA
{
    public partial class Form2 : Form
    {
        private Oseba _oseba;
        public string Znesek { get; private set; }
        public Form2(Oseba osebe)
        {
            InitializeComponent();
            _oseba = osebe;
            ime.Text += osebe.Ime;
            id.Text += osebe.Id;
            stanje.Text += osebe.Stanje;

            Zaposleni.Visible = _oseba.Zaposlen;
            Transakcija.NaloziIzFile("Transakcije.txt");
            var objekt =  Transakcija.NaloziIzFile("Transakcije.txt");
            foreach( var item in objekt )
            {
                transakcije.Items.Add( item );
            }
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
            Form4 form4 = new Form4();
            if (form4.ShowDialog() == DialogResult.OK)
            {
                float znesek;
                if (float.TryParse(form4.Znesek, out znesek))
                {
                    Transakcija transakcija = new Transakcija(DateTime.Now, $"Nakaži: {znesek} €", znesek, _oseba);
                    transakcija.IzvediTransakcijo(_oseba);
                    transakcija.PrikaziPodatke(transakcija, transakcije);
                    stanje.Text = $"Stanje: {_oseba.Stanje} €";
                }
                else
                {
                    MessageBox.Show("Vnos ni veljaven");
                }

            }

            //doda transakcijo v file
            //refresh/add v listbox

        }
        private void dvig_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5(_oseba, transakcije);
            form5.Show();
           // form5.UpdateTransakcije += Form5_UpdateTransakcije;
            //if (form5.ShowDialog() == DialogResult.OK)
            //{
            //    float znesek;
            //    if (float.TryParse(form5.Znesek, out znesek))
            //    {
            //        Transakcija transakcija = new Transakcija(DateTime.Now, $"Dvig: {znesek} €", znesek, _oseba);
            //        transakcija.IzvediTransakcijo(_oseba);
            //        transakcija.PrikaziPodatke(transakcija, transakcije);
            //        stanje.Text = $"Stanje: {_oseba.Stanje} €";
            //    }
            //    else
            //    {
            //        MessageBox.Show("Vnos ni veljaven");
            //    }
            //}

        }
        private void PrikaziTransakcijoT(string tipTransakcije)
        {
            string input = Microsoft.VisualBasic.Interaction.InputBox("Vpište znesek:", "Znsek", "0", -1, -1);

            if (float.TryParse(input, out float znesek))
            {
                Transakcija transakcija = new Transakcija(DateTime.Now, $"{tipTransakcije}: {znesek} €", znesek, _oseba);
                transakcija.IzvediTransakcijo(_oseba);

                transakcije.Items.Add(transakcija.ToString());
                stanje.Text = $"Stanje: {_oseba.Stanje} €";
            }
            else
            {
                MessageBox.Show("Vnos ni validen.");
            }
        }
        //private void form5_updatetransakcije(object sender, eventargs e)
        //{
        //    transakcije.items.clear();
        //    list<transakcija> loadedtransakcije = transakcija.naloziizfile("transakcije.txt");
        //    foreach (transakcija t in loadedtransakcije)
        //    {
        //        t.prikazipodatke(transakcije);
        //    }
        //}
    }

}

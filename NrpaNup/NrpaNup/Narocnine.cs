namespace NrpaNup;

public class Narocnine : Kartica
{
    //Table Narocnine
    public  int IDNarocnine { get; set; }
    public string ImeNarocnine { get; set; }
    public decimal VsotaNarocnineMesecno { get; set; }
    public decimal VsotaNarocnineLetno { get; set; }
   
    //Table narocnineU
    public int IDNarocnineU { get; set; }
    public int IDUporabnika { get; set; }
    public int IDKartica { get; set; }
    public int VsotaNarocnine { get; set; }
    public string TipNarocnine { get; set; }
    public Narocnine() {}

    public Narocnine(int idNarocnine, string imeNarocnine ,decimal vsotaNarocnineMesecno, decimal vsotaNarocnineLetno)
    {
        IDNarocnine = idNarocnine;
        ImeNarocnine = imeNarocnine;
        VsotaNarocnineMesecno = vsotaNarocnineMesecno;
        VsotaNarocnineLetno = vsotaNarocnineLetno;
    }
    
    public Narocnine(int idNarocnineU, int idUporabnika, int idKartica,  string tipNarocnine, string vrsta, string imeNarocnine, int vsotaNarocnine) : base (vrsta)
    {
        IDNarocnineU = idNarocnineU;
        IDUporabnika = idUporabnika;
        IDKartica = idKartica;
        TipNarocnine = tipNarocnine;
        Vrsta = vrsta;
        ImeNarocnine = imeNarocnine;
        VsotaNarocnine = vsotaNarocnine;
    }
}
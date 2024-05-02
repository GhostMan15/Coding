namespace NrpaNup;

public class Narocnine
{
    //Table Narocnine
    public  int IDNarocnine { get; set; }
    public string ImeNarocnine { get; set; }
    public decimal VsotaNarocnineMesecno { get; set; }
    public decimal VsotaNarocnineLetno { get; set; }
    public string VrstaNarocnine { get; set; }
    //Table narocnineU
    public int IDNarocnineU { get; set; }
    public int IDUporabnika { get; set; }
    public int IDKartica { get; set; }
    public Narocnine() {}

    public Narocnine(int idNarocnine, string imeNarocnine ,decimal vsotaNarocnineMesecno, decimal vsotaNarocnineLetno)
    {
        IDNarocnine = idNarocnine;
        ImeNarocnine = imeNarocnine;
        VsotaNarocnineMesecno = vsotaNarocnineMesecno;
        VsotaNarocnineLetno = vsotaNarocnineLetno;
    }
    
    public Narocnine(int idNarocnineU, int idUporabnika, int idKartica, int idNarocnine, string vrstaNarocnine)
    {
        IDNarocnineU = idNarocnineU;
        IDUporabnika = idUporabnika;
        IDKartica = idKartica;
        IDNarocnine = idNarocnine;
        VrstaNarocnine = vrstaNarocnine;
    }
}
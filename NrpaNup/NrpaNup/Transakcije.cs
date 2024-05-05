namespace NrpaNup;

public class Transakcije
{
    //vosta
    // z kartice
    //koliko
    //kdaj
    //od uporabnika
    public int IDTranskacije { get; set; }
    public int IDUporabnika { get; set; }
    public decimal VsotaTransakcije { get; set; }
    public int IDkartica { get; set; }
    public int IDkredita { get; set; }
    public int IDZaposlenTabela { get; set; }
    public string Ime { get; set; }
    public int STkratice { get; set; }
    public string Cas { get; set; }
}
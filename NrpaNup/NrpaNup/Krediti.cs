namespace NrpaNup;

public class Krediti : Kartica
{
    public int IDkredita { get; set; }
    public int VsotaKredita { get; set; }
    public string TipKredita { get; set; }
    public decimal ObrestnaMera { get; set; }
    public decimal MesecnoOdplacilo { get; set; }
    public string Cas { get; set; }
    public int StOdplacil { get; set; }
    public Krediti() {}

    public Krediti(int idKredita, int vsotaKredita, string tipKredita, decimal obrestnaMera)
    {
        IDkredita = idKredita;
        VsotaKredita = vsotaKredita;
        TipKredita = tipKredita;
        ObrestnaMera = obrestnaMera;
    }

    protected Krediti(int idKredita, int vsotaKredita, string tipKredita, decimal obrestnaMera, decimal mesecnoOdplacilo, string cas, int stOdplacil, string stKartice): base(stKartice)
    {
        IDkredita = idKredita;
        VsotaKredita = vsotaKredita;
        TipKredita = tipKredita;
        ObrestnaMera = obrestnaMera;
        MesecnoOdplacilo = mesecnoOdplacilo;
        Cas = cas;
        StOdplacil = stOdplacil;
        STKratice = stKartice;
    }
}
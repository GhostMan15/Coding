namespace NrpaNup;

public class KreditiUporabnika : Krediti
{
    public int IDKreditaU { get; set; }
    public int UporabnikID { get; set; }
    public int IDKArtica { get; set; }
    public string Ime { get; set; }
    public KreditiUporabnika(int idKreditaU, int uporabnikId,int idKredita, int idkArtica, int vsotaKredita, string tipKredita, decimal obrestnaMera, decimal mesecnoOdplacilo, string cas, int stOdplacil, string stKartice) : base(idKredita,
        vsotaKredita, tipKredita, obrestnaMera, mesecnoOdplacilo,cas, stOdplacil, stKartice)
    {
        IDKreditaU = idKreditaU;
        UporabnikID = uporabnikId;
        IDKArtica = idkArtica;
    }
    public KreditiUporabnika(int idKreditaU, int uporabnikId,int idKredita, int idkArtica,string ime,  int vsotaKredita, string tipKredita, decimal obrestnaMera, decimal mesecnoOdplacilo, string cas, int stOdplacil, string stKartice) : base(idKredita,
        vsotaKredita, tipKredita, obrestnaMera, mesecnoOdplacilo,cas, stOdplacil, stKartice)
    {
        IDKreditaU = idKreditaU;
        UporabnikID = uporabnikId;
        IDKArtica = idkArtica;
        Ime = ime;
    }
}
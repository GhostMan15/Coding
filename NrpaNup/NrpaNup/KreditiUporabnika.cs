namespace NrpaNup;

public class KreditiUporabnika : Krediti
{
    public int UporabnikID { get; set; }
    
    public KreditiUporabnika(int idKredita, int vsotaKredita, string tipKredita, decimal obrestnaMera, decimal mesecnoOdplacilo) : base(idKredita,
        vsotaKredita, tipKredita, obrestnaMera, mesecnoOdplacilo)
    {
        
    }
}
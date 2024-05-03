namespace NrpaNup;

public class UporabniskiPodatki : Kartica
{
    public int UporabnikID { get; set; }
    public string Ime { get; set; }
    public string Geslo { get; set; }
    public string Created { get; set; }
    public string VrstaUporabnika { get; set;}
    public int ZaposlenID { get; set; }
    public string ZaposlenIme { get; set; }
    public string ZaposlenGeslo { get; set; }
    public UporabniskiPodatki(int uporabnikId,string ime, string geslo, string created, string vrstaUporabnika, int idKartica,
        string stKratice, string vrsta, int limit, string status, double stanje, string veljavnost) : base(idKartica,
        stKratice, vrsta, limit, status, stanje, veljavnost)
    {
        UporabnikID = uporabnikId;
        Ime = ime;
        Geslo = geslo;
        Created = created;
        VrstaUporabnika = vrstaUporabnika;
    }

    public UporabniskiPodatki(int zaposlenId, int uporabnikId, string zaposlenIme, string zaposlenGeslo)
    {
        ZaposlenID = zaposlenId;
        UporabnikID = uporabnikId;
        ZaposlenIme = zaposlenIme;
        ZaposlenGeslo = zaposlenGeslo;
    }

    public UporabniskiPodatki(int uporabnikId, string ime, int karticaID)
    {
        UporabnikID = uporabnikId;
        Ime = ime;
        IDkartica = karticaID;
    }
}
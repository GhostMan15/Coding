namespace NrpaNup;

public class Kartica
{
    public int IDkartica { get; set; }
    public decimal? Stanje { get; set; }
    public string STKratice { get; set; }
    public int Limit { get; set; }
    public string Status { get; set; }
    public string Vrsta { get; set; }
    public string Veljavnost { get; set; }
    public Kartica(){}

    public Kartica(int idDkartica, string stKratice, string vrsta, int limit, string status, decimal? stanje, string veljavnost)
    {
        IDkartica = idDkartica;
        Stanje = stanje;
        STKratice = stKratice;
        Limit = limit;
        Status = status;
        Vrsta = vrsta;
        Veljavnost = veljavnost;
    }

    public Kartica(string vrsta)
    {
        Vrsta = vrsta;
    }
    
}
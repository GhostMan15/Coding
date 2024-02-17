

namespace PredtestNRPA;


abstract class Tiskovina : IDDV
{
    protected string naslov;
    protected int steviloStrani;
    protected double cena;
    public abstract int Oglasi(int stOglasov);
    public static bool operator > (Tiskovina t1, Tiskovina t2)
    {
        if (t1.cena > t2.cena) return true;
        else return false;
    }

    public static bool operator < (Tiskovina t1, Tiskovina t2)
    {
        if (t1.cena < t2.cena) return false;
        else return true;
    }

    public double ZnesekDDv(double procentDDV)
    {
        return cena * procentDDV / 100;
    }

}

class Revija : Tiskovina
{
    protected string izdajatelj;

    public Revija()
    {
        naslov = "";
        izdajatelj = ""; 
        steviloStrani = 1;
        cena = 0;
    }
    public Revija(string naslov,int steviloStrani, int cena,string izdajatelj)
    {
        if (steviloStrani < 0)
        {
            this.steviloStrani = 1;
        }
    }

    public override int Oglasi(int stOglasov)
    {
        return stOglasov * steviloStrani;
    }

    public virtual void Izpis()
    {
        Console.WriteLine(naslov);
        Console.WriteLine(izdajatelj);
        Console.WriteLine(steviloStrani);
        Console.WriteLine(cena);
    }
    
}

class ERevija : Revija
{
    private string URL;
    public ERevija(){}
    public ERevija(string naslov, int steviloStrani, int cena, string izdajatelj, string url) 
        : base(naslov,steviloStrani, cena, izdajatelj)
    {
        this.URL = url;
    }

    public bool JeSlovenska
    {
        get{
            if(URL.EndsWith("si"))return true;
            else return false; 
        }
    }

    public string this[int indeks]
    {
        get
        {
            switch ( indeks)
            {
                case 0:
                    return naslov;
                case 1:
                    return steviloStrani.ToString();
                case 2:
                    return cena.ToString();
                case 3:
                    return izdajatelj;
                case 4:
                    return URL;
                default:
                    throw new Exception(nameof(indeks));
                  
            }
            
        }
        set
        {
            switch (indeks)
            {
                case 1:
                    naslov = value;
                    break;
                case 2:
                    steviloStrani = int.Parse(value);
                    break;
                case 3:
                    cena = double.Parse(value);
                    break;
                case 4:
                    izdajatelj = value;
                    break;
                case 5:
                    URL = value;
                    break;
                default:
                    throw new Exception(nameof(indeks));
            }
        }
    }
}

interface IDDV
{
  public double ZnesekDDv(double procentDDV);
}
class Program
{
    static void Main(string[] args)
    {
        ERevija r1 = new ERevija();
        ERevija r2 = new ERevija("njau",32,543,"njhn","asdasd.si");
        IDDV vmesnik;
        if (r1 > r2)
        {
            r1.Izpis();
            if (r1.JeSlovenska) Console.WriteLine("je slo");
            else Console.WriteLine("ni");
            vmesnik = r1;
            Console.WriteLine(vmesnik.ZnesekDDv(22.5));
            Console.WriteLine(r1[2]);
        }
        else
        {
            r2.Izpis();
            if (r2.JeSlovenska) Console.WriteLine("je slo");
            else Console.WriteLine("ni");
            vmesnik = r2;
            Console.WriteLine(vmesnik.ZnesekDDv(17.5));
        }
        

    }
}
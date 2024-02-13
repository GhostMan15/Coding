namespace Vaja06Nrpa;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
    }
}

abstract class Liki
{
    public abstract double Ploscina(int x);
    public abstract double Obseg(int y);


}

class  Krog : Liki
{
    public override double Ploscina(int x)
    {
        x = 22;
        return x ;
    }

   public override double Obseg(int y)
    {
        return y.ToString();
    }
}
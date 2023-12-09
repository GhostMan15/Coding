using System;

// Prvi razred s kapsulacijo, konstruktorji/destruktorji, lastnostmi, statičnimi in objektnimi metodami
public class Oseba
{
    // Privatni podatki
    private string ime;
    private int starost;

    // Konstruktor
    public Oseba(string ime, int starost)
    {
        this.ime = ime;
        this.starost = starost;
    }

    // Destruktor
    ~Oseba()
    {
        Console.WriteLine($"Oseba {ime} je bila uničena.");
    }

    // Lastnosti
    public string Ime
    {
        get { return ime; }
        set { ime = value; }
    }

    public int Starost
    {
        get { return starost; }
        set { starost = value; }
    }

    // Objektna metoda
    public virtual void PredstaviSe()
    {
        Console.WriteLine($"Ime: {ime}, Starost: {starost} let");
    }

    // Statična metoda
    public static void Pozdravi()
    {
        Console.WriteLine("Pozdravljeni iz statične metode!");
    }

    // Preoblaganje operatorja
    public static Oseba operator +(Oseba oseba1, Oseba oseba2)
    {
        return new Oseba($"Skupaj: {oseba1.ime} in {oseba2.ime}", oseba1.starost + oseba2.starost);
    }
}

// Drugi razred s konstanto, readonly podatkom, statično metodo in dedovanjem
public class Student : Oseba
{
    // Konstanta
    public const string Status = "Študent";

    // Readonly podatek
    public readonly int Indeks;

    // Konstruktor
    public Student(string ime, int starost, int indeks) : base(ime, starost)
    {
        Indeks = indeks;
    }

    // Statična metoda
    public static void Studiraj()
    {
        Console.WriteLine("Študiram!");
    }
}

// Tretji razred s polimorfno metodo
public class Zaposleni : Oseba
{
    // Konstruktor
    public Zaposleni(string ime, int starost) : base(ime, starost)
    {
    }

    // Polimorfna metoda
    public override void PredstaviSe()
    {
        Console.WriteLine($"Ime: {Ime}, Starost: {Starost} let, Zaposleni");
    }
}

class Program
{
    static void Main()
    {
        // Uporaba lastnih knjižnic
        Oseba oseba1 = new Oseba("Janez", 30);
        Oseba oseba2 = new Oseba("Ana", 25);
        oseba1.PredstaviSe();
        oseba2.PredstaviSe();

        Oseba.Pozdravi();

        Oseba skupaj = oseba1 + oseba2;
        skupaj.PredstaviSe();

        Student student = new Student("Marko", 22, 12345);
        student.PredstaviSe();
        Console.WriteLine($"Status: {Student.Status}, Indeks: {student.Indeks}");
        Student.Studiraj();

        Zaposleni zaposleni = new Zaposleni("Katarina", 35);
        zaposleni.PredstaviSe();

        Console.ReadKey();
    }
}


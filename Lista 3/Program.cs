
public delegate void NowyObserwatorDelegate(string nazwa, double x, double y);

public delegate WypiszDelegate();

public class Obserwator
{
    private string nazwa;
    private double x;
    private double y;

    private List<(string Nazwa, double X, double Y, double Odleglosc)> sasiedzi;
    public Obserwator(string nazwa, double x, double y)
    {
        this.nazwa = nazwa;
        this.x = x;
        this.y = y;
        this.sasiedzi = new List<(string, double, double, double)>();
    }

    
}

public class Tworca {
    
    
}

public class Program
{
    public static void Main(string[] args)
    {
        // Tworzenie instancji klasy Tworca
        Tworca tworca = new Tworca();

        // Tworzenie instancji klasy Obserwator
        Obserwator obserwator = new Obserwator("minecraft", 0.5, 0.5);

        // Możesz dodać logikę do interakcji między twórcą a obserwatorem tutaj
        Console.WriteLine("Hello, World!");
    }
}
// See https://aka.ms/new-console-template for more information



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

    public void NowyObserwator(string nowaNazwa, double noweX, double noweY)
    {
        if (nowaNazwa != this.nazwa)
        {
            double odleglosc = Math.Sqrt(Math.Pow(noweX - x, 2) + Math.Pow(noweY - y, 2));
            sasiedzi.Add((nowaNazwa, noweX, noweY, odleglosc));
            sasiedzi.Sort((a, b) => a.Odleglosc.CompareTo.(b.Odleglosc));
            if (sasiedzi.Count > 2) {
                sasiedzi.Take(2).ToList();
            }
        }
    }

    public void Wypisz() {
        Console.Write($"Jestem {0} - lista sąsiadów:", nazwa);
        if (sasiedzi.Count <= 0)
        {
            Console.Write("Brak sąsiadów");
        }
        else
        {
            foreach (var sasiad in sasiedzi)
            {
                Console.Write($"{0}   x = {1}   y = {2}   odleglosc = {3}", sasiad.Nazwa, sasiad.X, sasiad.Y, sasiad.Odleglosc);
            }
        }
    }

    
}

public class Tworca {
    
    
}

public class Program
{
    public static void Main(string[] args)
    {
        // Tworzenie instancji klasy Tworca
        //Tworca tworca = new Tworca();

        // Tworzenie instancji klasy Obserwator
        //Obserwator obserwator = new Obserwator("minecraft", 0.5, 0.5);

        // Możesz dodać logikę do interakcji między twórcą a obserwatorem tutaj
        Console.WriteLine("Hello, World!");
    }
}
// See https://aka.ms/new-console-template for more information


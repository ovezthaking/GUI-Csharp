
public delegate void NowyObserwatorDelegate(string nazwa, double x, double y);

public delegate void WypiszDelegate();

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
            sasiedzi.Sort((a, b) => a.Odleglosc.CompareTo(b.Odleglosc));
            if (sasiedzi.Count > 2) {
                sasiedzi.Take(2).ToList();
            }
        }
    }

    public void Wypisz() {
        Console.WriteLine("Jestem {0} - lista sąsiadów:", nazwa);
        if (sasiedzi.Count <= 0)
        {
            Console.WriteLine("Brak sąsiadów");
        }
        else
        {
            foreach (var sasiad in sasiedzi)
            {
                Console.WriteLine("{0}   x = {1:F3}   y = {2:F3}   odleglosc = {3:F3}", sasiad.Nazwa, sasiad.X, sasiad.Y, sasiad.Odleglosc);
            }
        }
    }

    
}

public class Tworca {

    private List<Obserwator> obserwatorzy = new List<Obserwator>();

    private Random random = new Random();

    public event NowyObserwatorDelegate Nowy;

    public event WypiszDelegate Wypisz;

    public void StworzObserwatora(string nazwa)
    {
        double x = random.NextDouble();
        double y = random.NextDouble();

        Obserwator nowyObserwator = new Obserwator(nazwa, x, y);
        obserwatorzy.Add(nowyObserwator);

        Nowy += nowyObserwator.NowyObserwator;
        Wypisz += nowyObserwator.Wypisz;


        Nowy?.Invoke(nazwa, x, y);

    }

    public void WypiszWszystkich()
    {
        Wypisz?.Invoke();
    }
    
}

public class Program
{
    public static void Main(string[] args)
    {
        // Tworzenie instancji klasy Tworca
        Tworca tworca = new Tworca();
        int krok = 0;

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine("-------------krok {0} -------------", i);
            if (i > 0)
            {
                tworca.WypiszWszystkich();
            }
            tworca.StworzObserwatora($"Obs {i}");
            krok++;
        }
        Console.WriteLine("-------------krok {0}-------------", krok);
        tworca.WypiszWszystkich();

        // Tworzenie instancji klasy Obserwator
        //Obserwator obserwator = new Obserwator("minecraft", 0.5, 0.5);

        
        
    }
}
// See https://aka.ms/new-console-template for more information


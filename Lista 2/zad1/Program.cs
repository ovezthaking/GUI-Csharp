using System;
using System.Collections.Generic;
using System.Linq;

public class Lista
{
    protected List<int> numbers;

    public Lista(int length)
    {
        numbers = new List<int>();
        Random rand = new Random();
        for (int i = 0; i < length; i++)
        {
            numbers.Add(rand.Next(1, 101));
        }
    }

    public Lista()
    {
        Random rand = new Random();
        int length = rand.Next(1, 6);
        numbers = new List<int>();
        for (int i = 0; i < length; i++)
        {
            numbers.Add(rand.Next(1, 101));
        }
    }

    public override string ToString()
    {
        return "{" + string.Join(",", numbers) + "}";
    }
}

public class Lista1 : Lista, IComparable<Lista1>
{
    public Lista1(int length) : base(length) { }
    public Lista1() : base() { }

    public int CompareTo(Lista1 other)
    {
        if (other == null) return 1;
        int minLength = Math.Min(numbers.Count, other.numbers.Count);
        for (int i = 0; i < minLength; i++)
        {
            if (numbers[i] != other.numbers[i])
                return numbers[i].CompareTo(other.numbers[i]);
        }
        return numbers.Count.CompareTo(other.numbers.Count);
    }
}

public class Lista2 : Lista, IComparable<Lista2>
{
    public Lista2(int length) : base(length) { }
    public Lista2() : base() { }

    public int CompareTo(Lista2 other)
    {
        if (other == null) return 1;
        if (numbers.Count != other.numbers.Count)
            return numbers.Count.CompareTo(other.numbers.Count);
        
        for (int i = 0; i < numbers.Count; i++)
        {
            if (numbers[i] != other.numbers[i])
                return numbers[i].CompareTo(other.numbers[i]);
        }
        return 0;
    }
}

class Program
{
    static void Main()
    {
        Random rand = new Random();
        
        // Tworzenie kolekcji dla Lista1
        List<Lista1> lista1Collection = new List<Lista1>();
        for (int i = 0; i < 5; i++)
        {
            lista1Collection.Add(new Lista1(rand.Next(0, 6)));
        }

        // Wypisanie przed sortowaniem
        Console.WriteLine("Lista1 przed sortowaniem:");
        foreach (var item in lista1Collection)
        {
            Console.WriteLine(item);
        }

        // Sortowanie
        lista1Collection.Sort();

        // Wypisanie po sortowaniu
        Console.WriteLine("\nLista1 po sortowaniu:");
        foreach (var item in lista1Collection)
        {
            Console.WriteLine(item);
        }

        // Tworzenie kolekcji dla Lista2
        List<Lista2> lista2Collection = new List<Lista2>();
        for (int i = 0; i < 5; i++)
        {
            lista2Collection.Add(new Lista2(rand.Next(0, 6)));
        }

        // Wypisanie przed sortowaniem
        Console.WriteLine("\nLista2 przed sortowaniem:");
        foreach (var item in lista2Collection)
        {
            Console.WriteLine(item);
        }

        // Sortowanie
        lista2Collection.Sort();

        // Wypisanie po sortowaniu
        Console.WriteLine("\nLista2 po sortowaniu:");
        foreach (var item in lista2Collection)
        {
            Console.WriteLine(item);
        }
    }
}
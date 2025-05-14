using System;

public class PaidInteger
{
    private int value; // Przechowywana wartość
    private int freeOperationLimit; // Limit bezpłatnych operacji
    private int getCount; // Licznik operacji pobierania
    private int setCount; // Licznik operacji nadawania

    // Konstruktor
    public PaidInteger(int initialValue, int freeOperationLimit)
    {
        this.value = initialValue;
        this.freeOperationLimit = freeOperationLimit;
        this.getCount = 0;
        this.setCount = 0;
    }

    // Właściwość z akcesorami get i set
    public int Value
    {
        get
        {
            if (getCount < freeOperationLimit)
            {
                getCount++;
                return value;
            }
            else
            {
                Console.WriteLine("Nieautoryzowana próba pobrania wartości! Limit operacji przekroczony.");
                return -1;
            }
        }
        set
        {
            if (setCount < freeOperationLimit)
            {
                setCount++;
                this.value = value;
            }
            else
            {
                Console.WriteLine("Nieautoryzowana próba nadania wartości! Limit operacji przekroczony.");
            }
        }
    }

    // Metoda zerująca liczniki operacji
    public void Ureguluj()
    {
        getCount = 0;
        setCount = 0;
        Console.WriteLine("Liczniki operacji zostały zresetowane.");
    }

    // Metoda wyświetlająca stan liczników
    public void WypiszStan()
    {
        Console.WriteLine($"Stan liczników: Pobrania = {getCount}, Nadania = {setCount}, Limit = {freeOperationLimit}");
    }
}

class Program
{
    static void Main()
    {
        // Tworzenie obiektu z początkową wartością 10 i limitem 3 operacji
        PaidInteger number = new PaidInteger(10, 3);

        // Test 1: Wyświetlenie stanu początkowego
        Console.WriteLine("--- Test 1: Stan początkowy ---");
        number.WypiszStan();

        // Test 2: Pobieranie wartości w granicach limitu
        Console.WriteLine("\n--- Test 2: Pobieranie wartości ---");
        Console.WriteLine($"Pobrana wartość: {number.Value}"); // 1. pobranie
        Console.WriteLine($"Pobrana wartość: {number.Value}"); // 2. pobranie
        Console.WriteLine($"Pobrana wartość: {number.Value}"); // 3. pobranie
        number.WypiszStan();

        // Test 3: Próba pobrania po przekroczeniu limitu
        Console.WriteLine("\n--- Test 3: Pobieranie po przekroczeniu limitu ---");
        Console.WriteLine($"Pobrana wartość: {number.Value}"); // Przekroczenie limitu
        number.WypiszStan();

        // Test 4: Nadawanie wartości w granicach limitu
        Console.WriteLine("\n--- Test 4: Nadawanie wartości ---");
        number.Value = 20; // 1. nadanie
        number.Value = 30; // 2. nadanie
        Console.WriteLine($"Pobrana wartość: {number.Value}"); // Pobranie (w granicach limitu)
        number.WypiszStan();

        // Test 5: Próba nadania po przekroczeniu limitu
        Console.WriteLine("\n--- Test 5: Nadawanie po przekroczeniu limitu ---");
        number.Value = 40; // Przekroczenie limitu
        Console.WriteLine($"Pobrana wartość: {number.Value}"); // Pobranie aktualnej wartości
        number.WypiszStan();

        // Test 6: Uregulowanie liczników i dalsze operacje
        Console.WriteLine("\n--- Test 6: Uregulowanie liczników ---");
        number.Ureguluj();
        number.WypiszStan();
        number.Value = 50; // Nowe nadanie po uregulowaniu
        Console.WriteLine($"Pobrana wartość: {number.Value}"); // Nowe pobranie
        number.WypiszStan();
    }
}
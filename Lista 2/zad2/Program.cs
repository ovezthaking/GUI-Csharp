using System;

public class StudentTestResults
{
    private int[][] testResults; // Tablica nieregularna przechowująca wyniki testów
    private string[] classNames; // Nazwy klas

    // Konstruktor inicjalizujący tablicę nieregularną
    public StudentTestResults(int numberOfClasses)
    {
        if (numberOfClasses <= 0)
            throw new ArgumentException("Liczba klas musi być większa od zera.");

        testResults = new int[numberOfClasses][];
        classNames = new string[numberOfClasses];

        // Inicjalizacja tablicy dla każdej klasy z losową liczbą uczniów (1-10)
        Random rand = new Random();
        for (int i = 0; i < numberOfClasses; i++)
        {
            int numberOfStudents = rand.Next(1, 11); // Losowa liczba uczniów
            testResults[i] = new int[numberOfStudents];
            classNames[i] = $"Klasa {i + 1}";

            // Wypełnianie wyników testów losowymi wartościami (0-100)
            for (int j = 0; j < numberOfStudents; j++)
            {
                testResults[i][j] = rand.Next(0, 101); // Losowy wynik testu
            }
        }
    }

    // Metoda do wyświetlania wyników
    public void DisplayResults()
    {
        for (int i = 0; i < testResults.Length; i++)
        {
            Console.WriteLine($"{classNames[i]} (Liczba uczniów: {testResults[i].Length}):");
            for (int j = 0; j < testResults[i].Length; j++)
            {
                Console.WriteLine($"  Uczeń {j + 1}: {testResults[i][j]} punktów");
            }
            Console.WriteLine();
        }
    }

    // Metoda do pobierania wyniku konkretnego ucznia w klasie
    public int GetStudentResult(int classIndex, int studentIndex)
    {
        if (classIndex < 0 || classIndex >= testResults.Length)
            throw new ArgumentException("Nieprawidłowy indeks klasy.");
        if (studentIndex < 0 || studentIndex >= testResults[classIndex].Length)
            throw new ArgumentException("Nieprawidłowy indeks ucznia.");

        return testResults[classIndex][studentIndex];
    }

    // Metoda do ustawiania wyniku konkretnego ucznia
    public void SetStudentResult(int classIndex, int studentIndex, int score)
    {
        if (classIndex < 0 || classIndex >= testResults.Length)
            throw new ArgumentException("Nieprawidłowy indeks klasy.");
        if (studentIndex < 0 || studentIndex >= testResults[classIndex].Length)
            throw new ArgumentException("Nieprawidłowy indeks ucznia.");
        if (score < 0 || score > 100)
            throw new ArgumentException("Wynik musi być w zakresie 0-100.");

        testResults[classIndex][studentIndex] = score;
    }

    // Metoda testująca działanie klasy
    public static void Test()
    {
        try
        {
            // Tworzenie instancji klasy z 3 klasami
            StudentTestResults results = new StudentTestResults(3);

            // Wyświetlanie początkowych wyników
            Console.WriteLine("Początkowe wyniki testów:");
            results.DisplayResults();

            // Testowanie pobierania wyniku
            Console.WriteLine("\nPobieranie wyniku ucznia 1 z klasy 1:");
            int score = results.GetStudentResult(0, 0);
            Console.WriteLine($"Wynik: {score} punktów");

            // Testowanie ustawiania wyniku
            Console.WriteLine("\nUstawianie nowego wyniku dla ucznia 1 z klasy 1:");
            results.SetStudentResult(0, 0, 95);
            Console.WriteLine("Nowy wynik ustawiony. Wyniki po zmianie:");
            results.DisplayResults();

            // Testowanie błędnego indeksu
            Console.WriteLine("\nPróba dostępu do nieprawidłowego ucznia:");
            results.GetStudentResult(0, 100); // Powinno rzucić wyjątek
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd: {ex.Message}");
        }
    }
}

// Główna metoda do uruchomienia testu
public class Program
{
    public static void Main()
    {
        StudentTestResults.Test();
    }
}
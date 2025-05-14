using System;

class Program
{
    static void Main()
    {
        Console.Write("Podaj liczbę naturalną: ");
        string input = Console.ReadLine();
        int N = int.Parse(input);
        int maxProduct = N * N;
        int maxDigits = maxProduct.ToString().Length;
        int fieldWidth = maxDigits + 1;
        
        for (int i = 1; i <= N; i++)
        {
            for (int j = 1; j <= N; j++)
            {
                int product = i * j;
                Console.Write("{0," + fieldWidth + "}", product);
            }
            Console.WriteLine();
        }
    }
}
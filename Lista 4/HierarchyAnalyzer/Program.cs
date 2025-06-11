using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using AttributeLibrary;

namespace HierarchyAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ANALIZA HIERARCHII DZIEDZICZENIA KLAS ===\n");

            // Ścieżka do analizowanego zestawu
            string assemblyPath = "SampleClasses.dll";
            
            

            try
            {
                AnalyzeAssembly(assemblyPath);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Nie można znaleźć pliku: {assemblyPath}");
                Console.WriteLine("Upewnij się, że plik SampleClasses.dll znajduje się w tym samym katalogu co program.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas analizy: {ex.Message}");
            }

            Console.WriteLine("\nNaciśnij dowolny klawisz aby zakończyć...");
            Console.ReadKey();
        }

        static void AnalyzeAssembly(string assemblyPath)
        {
            // Ładowanie zestawu
            Assembly assembly = Assembly.LoadFrom(assemblyPath);
            
            Console.WriteLine($"Analizowany zestaw: {assembly.GetName().Name}");
            Console.WriteLine($"Lokalizacja: {assembly.Location}\n");

            // Pobieranie wszystkich typów z zestawu
            Type[] allTypes = assembly.GetTypes();
            
            // Filtrowanie typów - tylko klasy i interfejsy bez atrybutu Hidden
            var visibleTypes = allTypes
                .Where(t => (t.IsClass || t.IsInterface) && !HasHiddenAttribute(t))
                .ToList();

            Console.WriteLine($"Znaleziono {allTypes.Length} typów, z czego {visibleTypes.Count} jest widocznych (bez atrybutu Hidden)\n");

            // Znajdowanie klas bazowych (tych, które nie mają rodzica lub ich rodzic to Object)
            var rootTypes = visibleTypes
                .Where(t => t.BaseType == null || t.BaseType == typeof(object) || HasHiddenAttribute(t.BaseType))
                .OrderBy(t => t.Name)
                .ToList();

            Console.WriteLine("HIERARCHIA DZIEDZICZENIA:\n");

            // Wyświetlanie hierarchii dla każdej klasy bazowej
            foreach (var rootType in rootTypes)
            {
                DisplayTypeHierarchy(rootType, visibleTypes, "", new HashSet<Type>());
                Console.WriteLine();
            }

            // Wyświetlanie statystyk
            DisplayStatistics(visibleTypes, allTypes);
        }

        static bool HasHiddenAttribute(Type type)
        {
            return type.GetCustomAttribute<HiddenAttribute>() != null;
        }

        static void DisplayTypeHierarchy(Type currentType, List<Type> allVisibleTypes, string indent, HashSet<Type> processedTypes)
        {
            // Sprawdzenie czy typ już został przetworzony (zabezpieczenie przed cyklami)
            if (processedTypes.Contains(currentType))
                return;

            processedTypes.Add(currentType);

            // Wyświetlanie informacji o aktualnym typie
            string typeInfo = GetTypeInfo(currentType);
            Console.WriteLine($"{indent}{typeInfo}");

            // Znajdowanie klas pochodnych
            var derivedTypes = allVisibleTypes
                .Where(t => t.BaseType == currentType)
                .OrderBy(t => t.Name)
                .ToList();

            // Rekurencyjne wyświetlanie klas pochodnych
            for (int i = 0; i < derivedTypes.Count; i++)
            {
                bool isLast = i == derivedTypes.Count - 1;
                string newIndent = indent + (isLast ? "└── " : "├── ");
                string childIndent = indent + (isLast ? "    " : "│   ");

                Console.Write(newIndent);
                DisplayTypeHierarchy(derivedTypes[i], allVisibleTypes, childIndent, new HashSet<Type>(processedTypes));
            }
        }

        static string GetTypeInfo(Type type)
        {
            string typeKind = type.IsInterface ? "interface" : 
                             type.IsAbstract ? "abstract class" : 
                             type.IsSealed ? "sealed class" : "class";

            string info = $"{type.Name} ({typeKind})";

            // Dodawanie informacji o implementowanych interfejsach
            var interfaces = type.GetInterfaces()
                .Where(i => i != typeof(object))
                .Select(i => i.Name)
                .ToList();

            if (interfaces.Any())
            {
                info += $" implements: {string.Join(", ", interfaces)}";
            }

            // Dodawanie informacji o modyfikatorach dostępu
            if (type.IsPublic)
                info += " [public]";
            else if (type.IsNotPublic)
                info += " [internal]";

            return info;
        }

        static void DisplayStatistics(List<Type> visibleTypes, Type[] allTypes)
        {
            Console.WriteLine("=== STATYSTYKI ===");
            
            var hiddenTypes = allTypes.Where(t => HasHiddenAttribute(t)).ToList();
            
            Console.WriteLine($"Wszystkich typów: {allTypes.Length}");
            Console.WriteLine($"Widocznych typów: {visibleTypes.Count}");
            Console.WriteLine($"Ukrytych typów (z atrybutem Hidden): {hiddenTypes.Count}");
            
            if (hiddenTypes.Any())
            {
                Console.WriteLine("\nUkryte typy:");
                foreach (var hiddenType in hiddenTypes.OrderBy(t => t.Name))
                {
                    var attr = hiddenType.GetCustomAttribute<HiddenAttribute>();
                    string reason = !string.IsNullOrEmpty(attr?.Reason) ? $" - {attr.Reason}" : "";
                    Console.WriteLine($"  • {hiddenType.Name}{reason}");
                }
            }

            // Statystyki typów
            var classes = visibleTypes.Where(t => t.IsClass).Count();
            var interfaces = visibleTypes.Where(t => t.IsInterface).Count();
            var abstractClasses = visibleTypes.Where(t => t.IsClass && t.IsAbstract).Count();
            var sealedClasses = visibleTypes.Where(t => t.IsClass && t.IsSealed).Count();

            Console.WriteLine($"\nRozkład typów widocznych:");
            Console.WriteLine($"  • Klasy: {classes}");
            Console.WriteLine($"  • Interfejsy: {interfaces}");
            Console.WriteLine($"  • Klasy abstrakcyjne: {abstractClasses}");
            Console.WriteLine($"  • Klasy sealed: {sealedClasses}");
        }
    }
}

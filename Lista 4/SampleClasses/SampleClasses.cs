using System;
using AttributeLibrary;

namespace SampleClasses
{
    // Klasa bazowa bez atrybutu - będzie widoczna
    public class Animal
    {
        public string Name { get; set; }
        public virtual void MakeSound() { }
    }

    // Klasa pochodna bez atrybutu - będzie widoczna
    public class Mammal : Animal
    {
        public int NumberOfLegs { get; set; }
        public override void MakeSound()
        {
            Console.WriteLine("Mammal sound");
        }
    }

    // Klasa z atrybutem Hidden - będzie ukryta
    [Hidden("Internal implementation class")]
    public class InternalAnimal : Animal
    {
        public string InternalId { get; set; }
    }

    // Klasa pochodna bez atrybutu - będzie widoczna
    public class Dog : Mammal
    {
        public string Breed { get; set; }
        public override void MakeSound()
        {
            Console.WriteLine("Woof!");
        }
    }

    public class Cat : Mammal
    {
        public bool IsIndoor { get; set; }
        public override void MakeSound()
        {
            Console.WriteLine("Meow!");
        }
    }

    // Klasa z atrybutem Hidden - będzie ukryta
    [Hidden]
    public class SecretCat : Cat
    {
        public string SecretMission { get; set; }
    }

    // Kolejna hierarchia klas
    public class Vehicle
    {
        public string Brand { get; set; }
        public int Year { get; set; }
    }

    public class Car : Vehicle
    {
        public int NumberOfDoors { get; set; }
    }

    // Klasa z atrybutem Hidden - będzie ukryta
    [Hidden("Legacy code")]
    public class OldCar : Car
    {
        public bool IsAntique { get; set; }
    }

    public class SportsCar : Car
    {
        public int MaxSpeed { get; set; }
    }

    // Klasa bez dziedziczenia
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    // Klasa z atrybutem Hidden i bez dziedziczenia
    [Hidden("Confidential")]
    public class Employee : Person
    {
        public decimal Salary { get; set; }
    }

    // Interfejs - również będzie analizowany
    public interface IFlyable
    {
        void Fly();
    }

    // Klasa implementująca interfejs
    public class Bird : Animal, IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("Flying...");
        }
    }

    // Klasa z atrybutem Hidden implementująca interfejs
    [Hidden]
    public class Airplane : Vehicle, IFlyable
    {
        public void Fly()
        {
            Console.WriteLine("Airplane flying...");
        }
    }
}
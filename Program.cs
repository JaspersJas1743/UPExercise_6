using System;
using System.Collections.Generic;
using System.Linq;

namespace Задание__6
{
    internal class Car
    {
        public delegate void Error(object sender, CarEventArgs e);
        public event Error? NotifyError;
        public int MaxSpeed { get { return 60; } }
        private int speed = 0;

        public Car() => this.speed = 0;

        public Car(int value) => this.speed = value;

        public int Speed
        {
            get { return this.speed; }
            set
            {
                if (value < 0 && value > MaxSpeed) {  NotifyError?.Invoke(this, new CarEventArgs("Скорость не может превышать 60 км./ч.", value)); }
                else { speed = value; }
            }
        }

        public void increaseSpeed(int value)
        {
            if ((this.Speed + value) > MaxSpeed) { NotifyError?.Invoke(this, new CarEventArgs("Значение превышает максимальную скорость!", value)); }
            else
            {
                this.Speed += value;
                Console.WriteLine((this.Speed == value ? "Вы тронулись." : "Вы прибавили скорость!") + $" Текущая скорость: {this.Speed}");
            }
        }

        public void slowDownSpeed(int value)
        {
            if ((this.Speed - value) < 0) { NotifyError?.Invoke(this, new CarEventArgs("Значение скорости не может быть отрицательным!", value)); }
            else
            {
                this.Speed -= value;
                Console.WriteLine(this.Speed > 0 ? $"Вы cбавили скорость! Текущая скорость: {this.Speed}" : "Вы остановились");
            }
        }
    }

    internal class CarEventArgs
    {
        public string Message { get; }
        public int Value { get; }

        public CarEventArgs(string msg, int val)
        {
            Message = msg;
            Value = val;
        }
    }

    internal class Program
    {
        public static void PrintErrorMessage(object sender, CarEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Некорректное значение: {e.Value}");
            Console.WriteLine(e.Message);
            Console.ResetColor();
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("1. Создаем объект класса Car");
            var myCar = new Car();
            Console.WriteLine("2. Подписываемся на событие Car.NotifyError, которое вызывается при попытке установки некорректных значений скорости");
            myCar.NotifyError += PrintErrorMessage;
            Console.WriteLine("Метод Car.increaseSpeed:\npublic void increaseSpeed(int value)\n{\n\tif ((this.Speed + value) > MaxSpeed) { NotifyError?.Invoke(this, new CarEventArgs(\"Значение превышает максимальную скорость!\", value)); }\n\telse\n\t{\n\t\tthis.Speed += value;\n\t\tConsole.WriteLine((this.Speed == value ? \"Вы тронулись.\" : \"Вы прибавили скорость!\") + $\" Текущая скорость: {this.Speed}\");\n\t}\n}");
            Console.WriteLine("Метод Car.slowDownSpeed:\npublic void slowDownSpeed(int value)\n{\n\tif ((this.Speed - value) < 0) { NotifyError?.Invoke(this, new CarEventArgs(\"Значение скорости не может быть отрицательным!\", value)); }\n\telse\n\t{\n\t\tthis.Speed -= value;\n\t\tConsole.WriteLine(this.Speed > 0 ? $\"Вы cбавили скорость! Текущая скорость: {this.Speed}\" : \"Вы остановились\");\n\t}\n}");
            Console.WriteLine("\nВызов myCar.increaseSpeed(20);");
            myCar.increaseSpeed(20);
            Console.WriteLine("\nВызов myCar.increaseSpeed(50);");
            myCar.increaseSpeed(50);
            Console.WriteLine("\nВызов myCar.increaseSpeed(40);");
            myCar.increaseSpeed(40);
            Console.WriteLine("\nВызов myCar.slowDownSpeed(10);");
            myCar.slowDownSpeed(10);
            Console.WriteLine("\nВызов myCar.slowDownSpeed(50);");
            myCar.slowDownSpeed(50);
            Console.WriteLine("\nВызов myCar.increaseSpeed(10);");
            myCar.increaseSpeed(10);
            Console.WriteLine("\nВызов myCar.slowDownSpeed(30);");
            myCar.slowDownSpeed(30);
            Console.WriteLine("3. В конце кода отписываемся от события Car.NotifyError");
            myCar.NotifyError -= PrintErrorMessage;
        }
    }
}
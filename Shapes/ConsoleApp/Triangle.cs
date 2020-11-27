using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Triangle : Shape
    {
        private double[] _sides = new double[3];

        public override double Perimeter 
            => _sides[0] + _sides[1] + _sides[2];

        public override double Square
            => Math.Sqrt((Perimeter / 2) * ((Perimeter / 2) - _sides[0]) 
                * ((Perimeter / 2) - _sides[1]) 
                * ((Perimeter / 2) - _sides[1]));

        public Triangle()
        {
            Name = "Triangle";
            Console.WriteLine("Введите длины 3-х сторон треугольника (положительные вещественные числа).");
            _sides[0] = GetDoubleInput(0, 100000000);
            _sides[1] = GetDoubleInput(0, 100000000);
            _sides[2] = GetDoubleInput(0, 100000000);
        }

        public Triangle(double a, double b, double c)
        {
            Name = "Triangle";
            _sides[0] = a;
            _sides[1] = b;
            _sides[2] = c;
        }

        public static Triangle Generate()
        {
            return new Triangle(GetRandomNumber(Double.Epsilon, 100000000),
                GetRandomNumber(Double.Epsilon, 100000000),
                GetRandomNumber(Double.Epsilon, 100000000));
        }
    }
}

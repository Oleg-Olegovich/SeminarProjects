using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Circle : Shape
    {
        private double _radius;

        public override double Perimeter
            => 2 * Math.PI * _radius;

        public override double Square
            => Math.PI * Math.PI * _radius;

        public Circle()
        {
            Name = "Circle";
            Console.WriteLine("Введите длину радиуса круга.");
            _radius = GetDoubleInput(0, 1000000);
        }

        public Circle(double r)
        {
            Name = "Circle";
            _radius = r;
        }

        public static Circle Generate()
        {
            return new Circle(GetRandomNumber(Double.Epsilon, 1000000));
        }
    }
}

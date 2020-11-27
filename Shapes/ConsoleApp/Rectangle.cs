using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Rectangle : Shape
    {
        // 0 - 2 и 1 - 3 параллельные. 
        private double[] _sides = new double[4];

        public override double Perimeter
            => 2 * (_sides[0] + _sides[1]);

        public override double Square
            => _sides[0] * _sides[1];

        public Rectangle()
        {
            Name = "Rectangle";
            Console.WriteLine("Введите длины 2-х смежных сторон прямоугольника (положительные вещественные числа).");
            _sides[0] = _sides[2] = GetDoubleInput(0, 100000000);
            _sides[1] = _sides[3] = GetDoubleInput(0, 100000000);
        }

        public Rectangle(double a, double b)
        {
            Name = "Rectangle";
            _sides[0] = _sides[2] = a;
            _sides[1] = _sides[3] = b;
        }

        public static Rectangle Generate()
        {
            return new Rectangle(GetRandomNumber(Double.Epsilon, 100000000), 
                GetRandomNumber(Double.Epsilon, 100000000));
        }
    }
}

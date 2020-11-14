using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    class Parallelogram : Shape
    {
        // 0 - 2 и 1 - 3 параллельные. 
        private double[] _sides = new double[4];

        private double _angle;

        public override double Perimeter
            => 2 * (_sides[0] + _sides[1]);

        public override double Square
            => _sides[0] * _sides[1] * Math.Sin(_angle);

        public Parallelogram()
        {
            Name = "Paraallelogram";
            Console.WriteLine("Введите радиус круга.");
            Console.WriteLine("Введите длины 2-х смежных сторон параллелограмма (положительные вещественные числа).");
            _sides[0] = _sides[2] = GetDoubleInput(0, 100000000);
            _sides[1] = _sides[3] = GetDoubleInput(0, 100000000);
            Console.WriteLine("Введите значение острого угла");
            _angle = GetDoubleInput(0, 1000000);
        }

        public Parallelogram(double a, double b, double alpha)
        {
            Name = "Paraallelogram";
            _sides[0] = _sides[2] = a;
            _sides[1] = _sides[3] = b;
            _angle = alpha;
        }

        public static Parallelogram Generate()
        {
            return new Parallelogram(GetRandomNumber(Double.Epsilon, 100000000),
                GetRandomNumber(Double.Epsilon, 100000000),
                GetRandomNumber(Double.Epsilon, 1000000));
        }
    }
}

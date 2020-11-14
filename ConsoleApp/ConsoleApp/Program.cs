using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace ConsoleApp
{
    public class Program
    {
        private static List<Shape> _shapes;

        static void Input<T>() where T : Shape, new()
        {
            T shape;
            Console.WriteLine("Выберите способ ввода.");
            Console.WriteLine("Нажмите \"0\" для ручного ввода или любую другую клавишу для генерации.");
            if (Console.ReadKey().KeyChar == '0')
            {
                shape = new T();
            }
            else
            {
                shape = (T)Shape.Generate<T>();
            }
            _shapes.Add(shape);
        }

        static void ShapesInitialization()
        {
            _shapes = new List<Shape>();
            do
            {
                Console.WriteLine(Environment.NewLine + "Выберите тип фигуры:");
                Console.WriteLine("\"0\" - треугольник;");
                Console.WriteLine("\"1\" - прямоугольник;");
                Console.WriteLine("\"2\" - круг;");
                Console.WriteLine("\"3\" - параллелограм.");
                char type = Console.ReadKey().KeyChar;
                while (type < '0' || type > '3')
                {
                    Console.WriteLine("Некорректный ввод, попробуйте ещё раз:");
                    type = Console.ReadKey().KeyChar;
                }
                Console.WriteLine(Environment.NewLine + "Нажмите \"0\", если хотите сгенерировать множество фигур.");
                Console.WriteLine("Иначе нажмите любую клавишу.");
                if (Console.ReadKey().KeyChar == '0')
                {
                    Console.WriteLine();
                    int count = Shape.GetIntInput(1, 100);
                    for (int i = 0; i < count; ++i)
                    {
                        _shapes.Add(type switch
                        {
                            '0' => Triangle.Generate(),
                            '1' => Rectangle.Generate(),
                            '2' => Circle.Generate(),
                            _ => Parallelogram.Generate()
                        });
                    }
                }
                else
                {
                    Console.WriteLine();
                    switch (type)
                    {
                        case '0':
                            Input<Triangle>();
                            break;
                        case '1':
                            Input<Rectangle>();
                            break;
                        case '2':
                            Input<Circle>();
                            break;
                        default:
                            Input<Parallelogram>();
                            break;
                    }
                }
                Console.WriteLine(Environment.NewLine + "Нажмите \"Escape\", чтобы прекратить инициализацию фигур.");
                Console.WriteLine("Иначе нажмите любую клавишу.");
            }
            while (Console.ReadKey().Key != ConsoleKey.Escape);
        }

        static void Main(string[] args)
        {
            ShapesInitialization();
            string file = "shapes.json";
            string output = "";
            foreach (var shape in _shapes)
            {
                output += JsonConvert.SerializeObject(shape) + Environment.NewLine;
            }
            File.WriteAllText(file, output);
        }
    }
}
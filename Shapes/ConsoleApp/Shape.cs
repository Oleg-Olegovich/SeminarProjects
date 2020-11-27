using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp
{
    abstract class Shape
    {
        public string Name { get; set; }

        public virtual double Perimeter { get; }

        public virtual double Square { get; }

        /// <summary>
        /// This method correctly processes the input data (for any type and validator).
        /// </summary>
        private static T GetData<T>(string helpMessage, Predicate<T> dataValidator = null)
        {
            Console.WriteLine(helpMessage);
            bool correctInput = false;
            T result = default;
            while (correctInput == false)
            {
                try
                {
                    // This is an attempt to convert the entered string to the desired type.
                    result = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                    /* If there are no additional data requirements or these requirements 
                     * are met, correctInput is set to true. Else program print error message.*/
                    correctInput = dataValidator?.Invoke(result) ?? true;
                    if (correctInput == false)
                    {
                        Console.WriteLine("Ввод не соответствует всем условиям!");
                    }
                    Console.WriteLine();
                }
                catch
                {
                    // Program catch an exception if the conversion fails.
                    Console.WriteLine("Некорректный ввод!");
                }
            }
            return result;
        }

        /// <summary>
        /// This method correctly handles the input of a single 
        /// number in range [leftBorder; rightBorder].
        /// </summary>
        public static int GetIntInput(int leftBorder, int rightBorder)
            => GetData<int>($"Введите целое числов в диапазоне [{leftBorder}; {rightBorder}]. ",
                x => leftBorder <= x && x <= rightBorder);

        /// <summary>
        /// This method correctly handles the input of a single 
        /// real number in range [leftBorder; rightBorder].
        /// </summary>
        public double GetDoubleInput(int leftBorder, int rightBorder)
            => GetData<double>($"Введите число в интервале ({leftBorder}; {rightBorder}). ",
                x => leftBorder < x && x < rightBorder);

        public static double GetRandomNumber(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }

        public static Shape Generate<T>()
        {
            return typeof(T).ToString() switch
            {
                "ConsoleApp.Triangle" => Triangle.Generate(),
                "ConsoleApp.Rectangle" => Rectangle.Generate(),
                "ConsoleApp.Circle" => Circle.Generate(),
                _ => Parallelogram.Generate()
            };
        }
    }
}

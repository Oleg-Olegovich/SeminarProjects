using System;
using System.Collections.Generic;
using System.Text;

namespace SomeSystem
{
    public class Man : Human
    {
        public Man(Man father, Woman mother)
        {
            Born(father, mother);
            Console.WriteLine(Name + " родился =(");
        }

        public void PrintInfo(int currentYear)
        {
            Console.WriteLine("Пол: мужской");
            base.PrintInfo(currentYear);
        }

        public void AddSpouse(Woman bride)
        {
            Console.WriteLine(Name + " женился на " + bride.Name + " =(");
            AddSpouse(bride);
        }

        public void AddChild(Woman mother)
        {
            mother.AddChild(this);
        }
    }
}

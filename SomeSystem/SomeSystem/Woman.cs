using System;
using System.Collections.Generic;
using System.Text;

namespace SomeSystem
{
    public class Woman : Human
    {
        public Woman (Man father, Woman mother)
        {
            Born(father, mother);
            Console.WriteLine(Name + " родилась! =)");
        }

        public void PrintInfo(int currentYear)
        {
            Console.WriteLine("Пол: женский");
            base.PrintInfo(currentYear);
        }

        public void AddChild(Man father)
        {
            AddChild(father, this);
        }

        public void AddSpouse(Man groom)
        {
            Console.WriteLine(Name + " вышла замуж за " + groom.Name + " =)");
            AddSpouse(groom);
        }
    }
}

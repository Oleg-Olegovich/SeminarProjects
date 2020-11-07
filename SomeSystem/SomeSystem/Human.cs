using System;
using System.Collections.Generic;
using System.Net.Cache;
using System.Text;

namespace SomeSystem
{
    public abstract class Human
    {
        public List<Human> Children;
        public List<Human> Spouses;
        public Human Father;
        public Human Mother;
        public bool isAlive = true;

        public string Name { get; set; }

        public int YearOfBirth { get; }

        public void PrintInfo(int currentYear)
        {
            Console.WriteLine("Имя: " + Name);
            Console.WriteLine("Возраст: " + (currentYear >= YearOfBirth
                ? (currentYear - YearOfBirth).ToString()
                : "задан год до рождения"));
            Console.WriteLine("Отец: " + Father.Name);
            Console.WriteLine("Мать: " + Mother.Name);
            Console.WriteLine("Жив: " + (isAlive ? "Да" : "Нет"));
        }

        public new void ToString(int currentYear)
        {
            string result = "Имя: " + Name + Environment.NewLine
                + "Возраст: " + (currentYear >= YearOfBirth
                ? (currentYear - YearOfBirth).ToString()
                : "задан год до рождения") + Environment.NewLine
                + "Отец: " + Father.Name + Environment.NewLine
                + "Мать: " + Mother.Name + Environment.NewLine
                + "Жив: " + (isAlive ? "Да" : "Нет") + Environment.NewLine
                + "Список супругов:" + Environment.NewLine;
            for (int i = 0; i < Spouses.Count; ++i)
            {
                result += Spouses[i].Name + Environment.NewLine;
            }
            result += "Список детей:" + Environment.NewLine;
            for (int i = 0; i < Children.Count; ++i)
            {
                result += Children[i].Name + Environment.NewLine;
            }
        }

        public void AddSpouse(Human spouse)
        {
            Spouses.Add(spouse);
        }

        public void AddChild(Man father, Woman mother)
        {
            Console.WriteLine("Введите пол нового человека (\"male\" или иное):");
            var sex = Console.ReadLine();
            if (sex == "male")
            {
                Children.Add(new Man(father, mother));
            }
            else
            {
                Children.Add(new Woman(father, mother));
            }
        }

        public void Born(Man father, Woman mother)
        {
            Father = father;
            Mother = mother;
            Console.WriteLine("Введите имя нового человека:");
            Name = Console.ReadLine();
            Console.WriteLine("Введите год рождения нового человека:");
            int year = 0;
            
            while (year < Math.Max(GetYearOfBirth(father), GetYearOfBirth(mother))
                || !int.TryParse(Console.ReadLine(), out year))
            {
                Console.WriteLine("Ввод некорректный, попробуйте ещё раз:");
            }
        }

        public void Die()
        {
            isAlive = false;
        }

        public static int GetYearOfBirth(Human human)
        {
            return human == null ? 0 : human.YearOfBirth;
        }
    }
}

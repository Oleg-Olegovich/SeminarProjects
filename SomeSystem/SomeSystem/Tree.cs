using System;
using System.Collections.Generic;
using System.IO;

namespace SomeSystem
{
    public static class HumanExtension
    {
        public static int CountOfGrandchildren(this Human human)
        {
            int count = 0;
            for (int i = 0; i < human.Children.Count; ++i)
            {
                count += human.Children[i].Children.Count;
            }
            return count;
        }
    }

    class Tree
    {
        private List<Human> _people = new List<Human>();

        private Action[] commands;

        public Human this[int index]
        {
            get
            {
                return _people[index];
            }
            set
            {
                _people[index] = value;
            }
        }

        public Tree()
        {
            commands = new Action[]
            {
                AddHuman,
                Wedding,
                AddChild,
                OutputData,
                Exit
            };
        }

        private static void PrintError(string errorMessage)
            => Console.WriteLine(errorMessage + "Попробуйте ещё раз.");

        private static T GetData<T>(string helpMessage, Predicate<T> dataValidator = null)
        {
            Console.WriteLine(helpMessage);
            bool correctInput = false;
            T result = default;
            while (correctInput == false)
            {
                try
                {
                    result = (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
                    correctInput = dataValidator?.Invoke(result) ?? true;
                    if (correctInput == false)
                    {
                        PrintError("Ввод не соответствует всем условиям. ");
                    }
                }
                catch
                {
                    PrintError("Некорректный ввод. ");
                }
            }
            return result;
        }

        private int GetIntInput(int leftBorder, int rightBorder)
            => GetData<int>($"Введите число в диапазоне: [{leftBorder}; {rightBorder}]. ",
                x => leftBorder <= x && x <= rightBorder);

        // Добавляется родоначальник.
        public void AddHuman()
        {
            Console.WriteLine("Введите пол нового человека:");
            var sex = Console.ReadLine();
            if (sex == "male")
            {
                _people.Add(new Man(null, null));
            }
            else
            {
                _people.Add(new Woman(null, null));
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Menu();
        }

        public void PrintListOfPeople(bool onlyAlive)
        {
            Console.Clear();
            Console.WriteLine("Список людей:");
            for (int i = 0; i < _people.Count; ++i)
            {
                if (onlyAlive && this[i].isAlive == false)
                {
                    continue;
                }
                Console.WriteLine(i.ToString() + " " + this[i].Name);
            }
        }

        public Human SelectHuman(bool onlyAlive)
        {
            int number = GetIntInput(0, _people.Count);
            while (onlyAlive && this[number].isAlive == false)
            {
                Console.WriteLine("Данный человек мёртв, выберите другого!");
                number = GetIntInput(0, _people.Count);
            }
            return this[number];
        }

        public void Wedding()
        {
            PrintListOfPeople(true);
            Console.WriteLine("Выберите 2 номера из списка выше, введите в разных строках.");
            dynamic groom = SelectHuman(true);
            dynamic bride = SelectHuman(true);
            while (groom.GetType() == bride.GetType()) 
            {
                Console.WriteLine("Желательно, чтобы жених и невеста были разных полов, повторите выбор.");
                groom = SelectHuman(true);
                bride = SelectHuman(true);
            }
            groom.AddSpoose(bride);
            bride.AddSpoose(groom);
            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Menu();
        }

        public void AddChild()
        {
            PrintListOfPeople(true);
            Console.WriteLine("Выберите 2 номера из списка выше, введите в разных строках.");
            dynamic father = SelectHuman(true);
            dynamic mother = SelectHuman(true);
            while (father.GetType() == mother.GetType())
            {
                Console.WriteLine("Желательно, чтобы отец и мать были разных полов, повторите выбор.");
                father = SelectHuman(true);
                mother = SelectHuman(true);
            }
            if (father.GetType() == typeof(Woman))
            {
                dynamic c = father;
                father = mother;
                mother = c;
            }
            father.AddChild(mother);
            Human grandfather = father.Father;
            Console.WriteLine($"Тепер у {grandfather} {grandfather.CountOfGrandchildren()} внуков.");
            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Menu();
        }

        public void OutputData()
        {
            try
            {
                string output = "";
                for (int i = 0; i < _people.Count; ++i)
                {
                    output += this[i].ToString() + Environment.NewLine;
                }
                File.WriteAllText("data.txt", output);
                Console.WriteLine("Данные успешно выведены в файл!");
            }
            catch
            {
                Console.WriteLine("Что-то пошло не так =(");
            }
            Console.WriteLine("Нажмите любую клавишу для продолжения.");
            Console.ReadKey();
            Menu();
        }

        public void Menu()
        {
            Console.Clear();
            Console.WriteLine("Выберите команду:" + Environment.NewLine
                + "0 - добавить родоначальника;" + Environment.NewLine
                + "1 - устроить свадьбу;" + Environment.NewLine
                + "2 - добавить ребёнка;" + Environment.NewLine
                + "3 - вывести данные в файл." + Environment.NewLine
                + "4 - завершить программу.");
            var command = GetIntInput(0, 4);
            commands[command]();
        }

        public void Exit()
        {
            Console.WriteLine("Конец программы.");
        }
    }
}

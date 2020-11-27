using System;

namespace LinkedList
{
    class Program
    {
        /// <summary>
        /// Необходимо реализовать коллекцию LinkedList, тогда этот метод должен успешно сработать.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            ICustomCollection<int> myList = new LinkedList<int>();
            Console.WriteLine(myList);
            for(int i = 0; i < 10; i++)
            {
                myList.Add(i);
            }
            Console.WriteLine(myList);
            if(myList.Length != 10)
            {
                Console.WriteLine($"ОШИБКА, {myList.Length} не равно 10!");
                return;
            }
            myList.Remove(0);
            if (myList.Length != 9)
            {
                Console.WriteLine($"ОШИБКА, {myList.Length} не равно 9!");
                return;
            }
            for(int i = 0; i < 9; i++)
            {
                if(myList[i] != i + 1)
                {
                    Console.WriteLine($"ОШИБКА, {myList[i]} не равно {i + 1}!");
                    return;
                }
            }
            myList[3] = 100;
            while (!myList.IsEmpty())
            {
                myList.Remove(0);
            }
            Console.WriteLine(myList);
            if (!myList.IsEmpty())
            {
                Console.WriteLine($"ОШИБКА, лист должен был быть пуст!");
                return;
            }
            Console.WriteLine("Поздравляем, ваш лист прошел несколько простых тестов! Возможно он упадет на других, но пока все ок!");
            Console.Read();
        }
    }
}

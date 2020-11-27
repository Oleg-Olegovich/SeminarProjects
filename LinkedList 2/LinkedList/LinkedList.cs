using System;
using System.Data.Common;

namespace LinkedList
{
    /// <summary>
    /// Необходимо реализовать унаследованный классом LinkedList интерфейс 
    /// (реализовать = написать все объявленные в нем методы)
    /// throw new NotImplementedException(); подлежат удалению и замене на реализацию каждой ветви кода, где такие бросания сейчас находятся.
    /// Кроме того, я не выписал некоторые члены интерфейса, 
    /// потому программа поначалу не будет компилироваться - просьба разобраться и выписать их самим - гугл и студия в помощь.
    /// 
    /// Сразу поясняю T - не равно Node!
    /// Другие члены - поля, свойства, методы и пр. - можно добавлять на ваше усмотрение!
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class LinkedList<T> : ICustomCollection<T>
    {
        public T this[int index]
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public int Length 
            => throw new NotImplementedException();

        public void Add(T item)
        {
            throw new NotImplementedException();
        }

        public bool Contains(T item)
        { 
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string stringOfItems = "";
            //Нужно написать логику формирования stringOfItems

            return $"{Length} items: [{stringOfItems}]";
            
        }
    }
  
    /// <summary>
    /// Это класс, представляющий из себя коробочки, из которых должен состоять односвязный список - 
    /// какие в нем должны быть члены - 
    /// можно догадаться, 
    /// посмотреть в справочном материале в гугле/слаке, 
    /// вспомнить, о чем говорилось в конце занятия в субботу
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Node<T>
    {

    }
}

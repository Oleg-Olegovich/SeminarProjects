using System;
using System.Data.Common;

namespace LinkedList
{
    /// <summary>
    /// Необходимо реализовать унаследованный классом LinkedList интерфейс 
    /// (реализовать = написать все объявленные в нем методы)
    /// 
    /// Сразу поясняю T - не равно Node!
    /// Другие члены - поля, свойства, методы и пр. - можно добавлять на ваше усмотрение!
    /// </summary>
    class LinkedList<T> : ICustomCollection<T>
    {
        private Node<T> _head;

        private Node<T> _tail;

        public T this[int index]
        {
            get => GetNode(index).Item;
            set => GetNode(index).Item = value;
        }

        public int Length { get; set; }

        private Node<T> GetNode(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException();
            }
            var node = _head;
            for (int i = 0; i < index; ++i)
            {
                node = node.Next;
            }
            return node;
        }

        public void Add(T item)
        {
            var node = new Node<T>(item);
            if (_head == null)
            {
                _head = node; 
            }
            else
            {
                node.Previous = _tail;
                _tail.Next = node;
            }
            _tail = node;
            ++Length;
        }

        public void AddAsTheFirst(T data)
        {
            var node = new Node<T>(data);
            if (Length == 0)
            {
                _head = _tail = node;
            }
            else
            {
                _head.Previous = node;
                node.Next = _head;
                _head = node;
            }
            ++Length;
        }

        public bool Contains(T item)
        {
            var node = _head;
            while (node != null)
            {
                if (node.Item.Equals(item))
                {
                    return true;
                }
                node = node.Next;
            }
            return false;
        }

        public bool IsEmpty()
            => Length > 0;

        public void Remove(int index)
        {
            var node = GetNode(index);
            if (node.Previous == null)
            {
                node.Next.Previous = null;
                _head = node.Next;
            }
            else
            {
                node.Previous.Next = node.Next;
                node.Next.Previous = node.Previous;
            }
            --Length;
        }

        public override string ToString()
        {
            string stringOfItems = "";
            var node = _head;
            while (node != null)
            {
                stringOfItems += $"{node.Item.ToString()}; ";
                node = node.Next;
            }
            return $"{Length} items: [{stringOfItems}]";
        }
    }
  

    class Node<T>
    {
        public T Item { get; set; }

        public Node<T> Previous { get; set; }

        public Node<T> Next { get; set; }

        public Node(T item)
        {
            Item = item;
        }
    }
}

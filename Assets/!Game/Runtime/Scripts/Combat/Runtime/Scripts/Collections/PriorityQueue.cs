using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultNamespace
{
    public class PriorityQueue<T> : IPriorityQueue<T>
    {
        private T[] _items;

        private int _size;
        private int _capacity;

        private IComparer<T> _comparer;
        
        public PriorityQueue(int capacity, IComparer<T> comparer)
        {
            _size = 0;

            _capacity = capacity;
            _items = new T[_capacity];

            _comparer = comparer;
        }
        
        public PriorityQueue(T[] items, IComparer<T> comparer)
        {
            _size = items.Length;

            _capacity = _size;
            _items = new T[_capacity];

            for (int i = 0; i < _size; i++)
            {
                _items[i] = items[i];
            }
            _comparer = comparer;
            Heapify();
        }

        public bool Push(T item)
        {
            if (_size == _capacity)
            {
                return false;
            }
            _items[_size] = item;
            
            int i = _size++;
            int parent = Parent(i);
            
            while (i > 0 && Compare(i, parent) < 0)
            {
                Swap(i, parent);

                i = parent;
                parent = Parent(i);
            }
            return true;
        }

        public T Peek()
        {
            if (_size == 0)
            {
                return default(T);
            }
            return _items[0];
        }

        public T Pop()
        {
            if (_size == 0)
            {
                return default(T);
            }
            if (_size == 1)
            {
                return _items[0];
            }
            T root = _items[0];
            _items[0] = _items[--_size];

            int i = 0;
            while (i < _size)
            {
                int smallest = BubbleDown(i);
                if (smallest == i)
                {
                    break;
                }
                Swap(i, smallest);
                i = smallest;
            }
            return root;
        }

        private int Parent(int i)
        {
            return (i - 1) / 2;
        }

        private int Left(int i)
        {
            return 2 * i + 1;
        }

        private int Right(int i)
        {
            return 2 * i + 2;
        }

        private int Compare(int i, int j)
        {
            return _comparer.Compare(_items[i], _items[j]);
        }

        private void Swap(int i, int j)
        {
            T temp = _items[i];
            _items[i] = _items[j];
            _items[j] = temp;
        }

        private int BubbleDown(int i)
        {
            int left = Left(i);
            int right = Right(i);
            int smallest = i;

            if (left < _size && Compare(left, smallest) < 0)
            {
                smallest = left;
            }
            if (right < _size && Compare(right, smallest) < 0)
            {
                smallest = right;
            }
            return smallest;
        }
        
        private void Heapify()
        {
            for (int i = _size / 2; i >= 0; i--)
            {
                int j = i;
                int smallest = BubbleDown(j);
                while (j < _size && j != smallest)
                {
                    Swap(j, smallest);
                    j = smallest;
                    smallest = BubbleDown(j);
                }
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            foreach (T item in _items)
            {
                sb.AppendFormat("{0}, ", item);
            }
            return sb.Append("]").ToString();
        }

        private int GetNearestPow2(int x)
        {
            if (x < 0)
            {
                return 0;
            }
            --x;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }
    }
}

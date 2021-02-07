using System;

namespace DefaultNamespace
{
    public interface IPriorityQueue<T>
    {
        bool Push(T item);
        T Peek();
        T Pop();
    }
}

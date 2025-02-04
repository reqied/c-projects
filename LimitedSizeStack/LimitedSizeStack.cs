using System;
using System.Collections.Generic;

namespace LimitedSizeStack;

public class LimitedSizeStack<T>
{
        private LinkedList<T> stack;
        private int capacity;

        public LimitedSizeStack(int undoLimit)
        {
            capacity = undoLimit;
            stack = new LinkedList<T>();
        }

        public void Push(T item)
        {
            if (capacity == 0)
                return;
            if (stack.Count == capacity) stack.RemoveFirst();
            stack.AddLast(item);
        }

        public T Pop()
        {
            if (stack.Count == 0)
                throw new InvalidOperationException("Stack is empty");
            var item = stack.Last.Value;
            stack.Remove(stack.Last);
            return item;
        }

        public int Count => stack.Count;
}
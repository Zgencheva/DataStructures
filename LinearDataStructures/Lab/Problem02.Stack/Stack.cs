namespace Problem02.Stack
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Stack<T> : IAbstractStack<T>
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }

            public T Value { get; set; }
            public Node Next { get; set; }
        }

        private Node top;

        public int Count { get; private set; }

        public void Push(T item)
        {
            var newNode = new Node(item);
            newNode.Next = this.top;
            this.top = newNode;
            this.Count++;
        }

        public T Pop()
        {
            if (this.top == null)
            {
                throw new InvalidOperationException();
            }
            var result = this.top.Value;
            this.top = this.top.Next;
            this.Count--;
            return result;

        }

        public T Peek()
        {
            if (this.top == null)
            {
                throw new InvalidOperationException();
            }
            return this.top.Value;
        }

        public bool Contains(T item)
        {
            foreach (var value in this)
            {
                if (value.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.top;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }

        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
namespace Problem03.Queue
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Queue<T> : IAbstractQueue<T>
    {
        private class Node
        {
            public T Value { get; set; }

            public Node Next { get; set; }

            public Node(T value)
            {
                this.Value = value;
            }
        }
        public Queue()
        {
            
        }
        private Node head;

        private Node tail;

        public int Count { get; set; }

        public void Enqueue(T item)
        {
            if (this.head == null)
            {
                this.head = new Node(item);
            }
            else if (this.tail == null)
            {
                this.tail = new Node(item);
                this.head.Next = this.tail;
            }
            else
            {
                var lastNode = new Node(item);
                this.tail.Next = lastNode;
                this.tail = lastNode;
            }
            this.Count++;
            
        }

        public T Dequeue()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            var result = this.head.Value;
            this.head = this.head.Next;
            Count--;
            return result;
        }

        public T Peek()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            return this.head.Value;
        }

        public bool Contains(T item)
        {
            foreach (var element in this)
            {
                if (element.Equals(item))
                {
                    return true;
                }
            }
            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var currentNode = this.head;
            while (currentNode != null)
            {
                yield return currentNode.Value;
                currentNode = currentNode.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
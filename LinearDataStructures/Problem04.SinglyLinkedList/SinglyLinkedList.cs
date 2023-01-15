namespace Problem04.SinglyLinkedList
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class SinglyLinkedList<T> : IAbstractLinkedList<T>
    {
        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
            }
            public T Value { get; set; }

            public Node Next { get; set; }
            public Node Previous { get; set; }
        }
        public int Count { get; set; }
        private Node head { get; set; }
        private Node tail { get; set; }
        public void AddFirst(T item)
        {
            var newNode = new Node(item);
            newNode.Next = this.head;
            this.head = newNode;
            this.Count++;
        }

        public void AddLast(T item)
        {
            if (this.head == null)
            {
                this.head = new Node(item);
            }
            else if (this.tail == null)
            {
                this.tail = new Node(item);
                this.head.Next = this.tail;
                this.tail.Previous = this.head;
            }
            else
            {
                var lastNode = new Node(item);
                this.tail.Next = lastNode;
                lastNode.Previous = this.tail;
                this.tail = lastNode;
            }
            this.Count++;
        }

        public T GetFirst()
        {
            if (this.head == null)
            {
                throw new InvalidOperationException();
            }
            return this.head.Value;
        }

        public T GetLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            else if (this.Count == 1)
            {
                return this.head.Value;
            }
            else
            {
                return this.tail.Value;
            }
            
        }

        public T RemoveFirst()
        { 
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            var result = this.head.Value;
            if (this.Count == 1)
            {
                this.head = null;
                
            }
            else
            {
                this.head = this.head.Next;
            }
            this.Count--;
            return result;
        }

        public T RemoveLast()
        {
            if (this.Count == 0)
            {
                throw new InvalidOperationException();
            }
            if (this.Count == 1)
            {
                var result = this.head.Value;
                this.head = null;
                this.Count--;
                return result;

            }
            else
            {
                var result = this.tail.Value;
                this.tail = this.tail.Previous;
                this.tail.Next = null;
                this.Count--;
                return result;
            }
            
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
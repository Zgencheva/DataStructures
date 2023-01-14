namespace Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private class Node
        {
            public Node(T element)
            {
                this.Value = element;
                this.Children = new HashSet<Node>();
            }

            public T Value { get; set; }

            public Node Parent { get; set; }
            public HashSet<Node> Children { get; set; }
        }
        public Hierarchy(T value)
        {
            this.root = new Node(value);
            this.nodesByValue = new Dictionary<T, Node>();
            nodesByValue.Add(value, this.root);
        }

        private Node root;

        private Dictionary<T, Node> nodesByValue { get; set; }

        public int Count => this.nodesByValue.Count;

        public void Add(T element, T child)
        {
            if (this.Contains(child))
            {
                throw new ArgumentException("Duplicate element");
            }
            if (!this.Contains(element))
            {
                throw new ArgumentException("Node does not exist");
            }

            var parentNode = this.nodesByValue[element];
            Node currentNode = new Node(child)
            {
                Parent = parentNode,
            };
            parentNode.Children.Add(currentNode);
            this.nodesByValue.Add(child, currentNode);
        }

        //private Node Add(Node node, T element, T value)
        //{
        //    if (node.Value.Equals(element))
        //    {
        //         node = AddValueOrThrowException(node, value);
        //        this.Count++;
        //    }
        //    else
        //    {
        //        foreach (var child in node.Children)
        //        {
        //            child.Value = this.Add(child, element, value).Value;
        //        }
        //    }
        //    return node;
        //}

        //private static Node AddValueOrThrowException(Node node, T value)
        //{
        //    foreach (var child in node.Children)
        //    {
        //        if (child.Value.Equals(value))
        //        {
        //            throw new ArgumentException("Duplicate child");
        //        }

        //    }
        //    node.Children.Add(new Node(value));
        //    return node;
        //}

        public bool Contains(T element)
        {
            return this.nodesByValue.ContainsKey(element);
        }

        public IEnumerable<T> GetChildren(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }
            var currentNode = this.nodesByValue[element];
            foreach (var child in currentNode.Children)
            {
                yield return child.Value;
            }
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            return this.Intersect(other);
        }
        public T GetParent(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }
            if (this.nodesByValue[element].Parent == null)
            {
                return default;
            }
            return this.nodesByValue[element].Parent.Value;
        }

        public void Remove(T element)
        {
            if (!this.nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }
            if (this.root.Value.Equals(element))
            {
                throw new InvalidOperationException();
            }
            var currentNode = this.nodesByValue[element];
            var currentParrent = currentNode.Parent;
            if (currentNode.Children.Count ==0)
            {
                currentParrent.Children.Remove(currentNode);
                this.nodesByValue.Remove(element);
            }
            else
            {
                foreach (var child in currentNode.Children)
                {
                    currentParrent.Children.Add(child);
                    child.Parent = currentParrent;
                }
                currentParrent.Children.Remove(currentNode);
                this.nodesByValue.Remove(element);
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
           var queue = new Queue<Node>();

           queue.Enqueue(this.root);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                yield return current.Value;
                foreach (var child in current.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
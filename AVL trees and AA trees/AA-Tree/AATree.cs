namespace AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {
        private class Node
        {
            public Node(T element)
            {
                this.Value = element;
                this.Level = 1;
            }

            public T Value { get; set; }
            public Node Right { get; set; }
            public Node Left { get; set; }
            public int Level { get; set; }
        }

        private Node root;

        public int Count()
        {
            return this.Count(this.root);
        }

        private int Count(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return 1 + this.Count(node.Left) + this.Count(node.Right);
        }

        public void Insert(T element)
        {
            this.root = this.Insert(this.root, element);
        }

        private Node Insert(Node node, T value)
        {
            if (node == null)
            {
                return new Node(value);
            }
            if (IsLesser(value, node.Value))
            {
                node.Left = this.Insert(node.Left, value);
            }
            else
            {
                node.Right = this.Insert(node.Right, value);
            }
            if (node.Left != null && node.Left.Level == node.Level)
            {
                node = this.Skew(node);
            }
            if (node.Right != null && node.Right.Right != null && node.Level == node.Right.Right.Level)
            {
                node = this.Split(node);
            }

            return node;
        }

        private Node Split(Node node)
        {
            Node current = node.Right;
            node.Right = current.Left;
            current.Left = node;
            current.Level++;
            return current;
        }

        private Node Skew(Node node)
        {
            var current = node.Left;
            node.Left = current.Right;
            current.Right = node;            
            return current;
        }

        public bool Contains(T element)
        {
            var current = this.root;
            while (current != null)
            {
                if (AreEqual(current.Value, element))
                {
                    return true;
                }
                else if (IsLesser(element, current.Value))
                {
                    current = current.Left;
                }
                else
                {
                    current = current.Right;
                }
            }

            return false;
        }

        public void InOrder(Action<T> action)
        {
            this.InOrder(this.root, action);
        }

        private void InOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            this.InOrder(node.Left, action);
            action(node.Value);
            this.InOrder(node.Right, action);
        }

        public void PreOrder(Action<T> action)
        {
            this.PreOrder(this.root, action);
        }

        private void PreOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            action(node.Value);
            this.PreOrder(node.Left, action);
            this.PreOrder(node.Right, action);
        }

        public void PostOrder(Action<T> action)
        {
            this.PostOrder(this.root, action);
        }

        private void PostOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;
            }
            this.PostOrder(node.Left, action);
            this.PostOrder(node.Right, action);
            action(node.Value);
        }

        private bool AreEqual(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }

        private bool IsLesser(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }
    }
}
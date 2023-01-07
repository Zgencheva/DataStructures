namespace AVLTree
{
    using System;

    public class AVL<T> where T : IComparable<T>
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.Height = 1;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int Height { get; set; }
        }

        public Node Root { get; private set; }

        public bool Contains(T element)
        {
            Node current = this.Root;
            while (current != null)
            {
                if (AreEqual(element, current.Value))
                {
                    return true;
                }
                if (IsLesser(element, current.Value))
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

        public void Delete(T element)
        {
            throw new InvalidOperationException();
        }

        public void DeleteMin()
        {
            throw new InvalidOperationException();
        }

        public void Insert(T element)
        {
            this.Root = this.Insert(this.Root, element);
        }

        private Node Insert(Node node, T element)
        {
            if (node == null)
            {
                return new Node(element);
            }
            if (IsLesser(element, node.Value))
            {
                node.Left = this.Insert(node.Left, element);
            }
            else
            {
                node.Right = this.Insert(node.Right, element);
            }
            node = this.Balance(node);
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return node;
        }

        private Node Balance(Node node)
        {
            var balanceFactor = BalanceFactor(node); //[-1,0,1]
            if (balanceFactor > 1)
            {
                if (BalanceFactor(node.Left) < 0)
                {
                    node = DoubleRightRotation(node);
                }
                else
                {
                    node = RotateRight(node);
                }
            }
            else if (balanceFactor < -1)
            {
                if (BalanceFactor(node.Right) > 0)
                {
                    node = DoubleLeftRotation(node);
                }
                else
                {
                   node = RotateLeft(node);
                }
            }
            return node;
        }

        private Node RotateLeft(Node node)
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return temp;
        }

        private Node RotateRight(Node node)
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            node.Height = Math.Max(Height(node.Left), Height(node.Right)) + 1;
            return temp;
        }

        private Node DoubleRightRotation(Node node)
        {
            node.Left = RotateLeft(node.Left);
            node = RotateRight(node);
            return node;
        }
        private Node DoubleLeftRotation(Node node)
        {
            node.Right = RotateRight(node.Right);
            node = RotateLeft(node);
            return node;
        }

        private int BalanceFactor(Node node)
        {
            return Height(node.Left) - Height(node.Right);
        }

        private int Height(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            return node.Height;
        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(this.Root, action);
        }

        private void EachInOrder(Node node, Action<T> action)
        {
            if (node == null)
            {
                return;            
            }
            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }
        private bool AreEqual(T element, T value)
        {
            return element.CompareTo(value) == 0;
        }
        private bool IsLesser(T element, T value)
        {
            return element.CompareTo(value) < 0;
        }
    }
}

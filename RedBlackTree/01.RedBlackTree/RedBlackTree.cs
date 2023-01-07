namespace _01.RedBlackTree
{
    using System;

    public class RedBlackTree<T> where T : IComparable
    {
        public class Node
        {
            public Node(T value)
            {
                this.Value = value;
                this.IsRed = true;
            }

            public T Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }

            public bool IsRed;

        }

        public Node root;
        public RedBlackTree(Node node)
        {
            this.PreOrderCopy(node);
        }

        private void PreOrderCopy(Node node)
        {
            if (node == null)
            {
                return;
            }
            this.Insert(node.Value);
            this.PreOrderCopy(node.Left);
            this.PreOrderCopy(node.Right);
        }

        public RedBlackTree()
        {

        }

        public void EachInOrder(Action<T> action)
        {
            this.EachInOrder(root, action);
        }
        private void EachInOrder(Node node, Action<T> action)
        {
            if (node== null)
            {
                return;
            }

            this.EachInOrder(node.Left, action);
            action(node.Value);
            this.EachInOrder(node.Right, action);
        }

        public RedBlackTree<T> Search(T element)
        {
            Node current = this.FindNode(element);

            return new RedBlackTree<T>(current);
        }
        public bool Contains(T element)
        {
            Node current = this.root;
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
        private Node FindNode(T element)
        {
            Node current = this.root;

            while (current != null)
            {
                if (IsLesser(element, current.Value))
                {
                    current = current.Left;
                }
                else if (IsLesser(current.Value, element))
                {
                    current = current.Right;
                }
                else
                {
                    break;
                }
            }
            
            return current;
        }

        public void Insert(T value)
        {
            this.root = this.Insert(root, value);
            this.root.IsRed = false;
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
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColors(node);
            }
                return node;
        }

        public void Delete(T key)
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = this.Delete(key, root);
            if (this.root != null)
            {
                this.root.IsRed = false;
            }
        }

        private Node Delete(T element, Node node)
        {
            if (IsLesser(element, node.Value))
            {
                if (!IsRed(node.Left) && !IsRed(node.Left.Left))
                {
                    node = MoveRedLeft(node);
                }
                node.Left = Delete(element, node.Left);
            }
            else
            {
                if (IsRed(node.Left))
                {
                    node = RotateRight(node);
                }
                if (AreEqual(element, node.Value) && node.Right == null)
                {
                    return null;
                }
                if (!IsRed(node.Right) && !IsRed(node.Right.Left))
                {
                    node = MoveRedRight(node);
                }
                if (AreEqual(element, node.Value))
                {
                    node.Value = FindMinValueInSubtree(node.Right);
                    node.Right = DeleteMin(node.Right);
                }
                else
                {
                    node.Right = Delete(element, node.Right);
                }
            }

            return FixUp(node);
        }

        private T FindMinValueInSubtree(Node node)
        {
            if (node.Left == null)
            {
                return node.Value;
            }
            return FindMinValueInSubtree(node.Left);
        }

        public void DeleteMin()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = this.DeleteMin(this.root);
            //check if we had tree with 1 node and we deleted it!
            if (this.root != null)
            {
                this.root.IsRed = false;
            }
        }

        private Node DeleteMin(Node node)
        {
            if (node.Left == null)
            {
                return null;
            }
            if (!IsRed(node.Left) && !IsRed(node.Left.Left))
            {
                node = MoveRedLeft(node);
            }
            node.Left = this.DeleteMin(node.Left);

            return FixUp(node);
        }

        public void DeleteMax()
        {
            if (this.root == null)
            {
                throw new InvalidOperationException();
            }
            this.root = DeleteMax(this.root);
        }

        private Node DeleteMax(Node node)
        {
            if (IsRed(node.Left))
            {
                node = RotateRight(node);
            }
            if (node.Right == null)
            {
                return null;
            }
            if (!IsRed(node.Right) && !IsRed(node.Right.Left))
            {
                node = MoveRedRight(node);
            }

            node.Right = DeleteMax(node.Right);
            return FixUp(node);
        }

        public int Count()
        {
            return this.Count(this.root);
        }

        private int Count(Node root)
        {
            if (root == null)
            {
                return 0;
            }
            return 1 + this.Count(root.Left) + this.Count(root.Right);
        }

        private Node RotateLeft(Node node) 
        {
            Node temp = node.Right;
            node.Right = temp.Left;
            temp.Left = node;
            temp.IsRed = node.IsRed;
            temp.Left.IsRed = true;

            return temp;
        }
        private Node FixUp(Node node)
        {
            if (IsRed(node.Right))
            {
                node = RotateLeft(node);
            }
            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }
            if (IsRed(node.Left) && IsRed(node.Right))
            {
                FlipColors(node);
            }
            return node;
        }

        private Node MoveRedLeft(Node node)
        {
            FlipColors(node);
            if (IsRed(node.Right.Left))
            {
                node.Right = RotateRight(node.Right);
                node = RotateLeft(node);
                FlipColors(node);
            }

            return node;
        }

        private Node MoveRedRight(Node node)
        {
            FlipColors(node);
            if (IsRed(node.Left.Left))
            {
                node = RotateRight(node);
                FlipColors(node);
            }

            return node;
        }
        private Node RotateRight(Node node) 
        {
            Node temp = node.Left;
            node.Left = temp.Right;
            temp.Right = node;
            temp.IsRed = temp.Right.IsRed;
            temp.Right.IsRed = true;

            return temp;
        }
        private void FlipColors(Node node)
        {
            node.IsRed = !node.IsRed;
            node.Right.IsRed = !node.Right.IsRed;
            node.Left.IsRed = !node.Left.IsRed;
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }
            return node.IsRed == true;
        }
        private bool IsLesser(T a, T b)
        {
            return a.CompareTo(b) < 0;
        }

        private bool AreEqual(T a, T b)
        {
            return a.CompareTo(b) == 0;
        }
    }
}
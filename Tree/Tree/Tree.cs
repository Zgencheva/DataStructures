namespace Tree
{
    using System;
    using System.Collections.Generic;

    public class Tree<T> : IAbstractTree<T>
    {

        public Tree(T value)
        {
            Value = value;
            this.Children = new List<Tree<T>>();
        }
        public Tree(T value, params Tree<T>[] children)
            : this(value)
        {
            foreach (var child in children)
            {
                child.Parent = this;
                this.Children.Add(child);
            }
        }

        public T Value { get; }
        public Tree<T> Parent { get; set; }

        public List<Tree<T>> Children { get; set; }

        public void AddChild(T parentKey, Tree<T> child)
        {
            var parrentNode = this.FindNode(parentKey);
            if (parrentNode == null)
            {
                throw new ArgumentNullException();
            }
            parrentNode.Children.Add(child);
            child.Parent = parrentNode;

        }

        private Tree<T> FindNode(T parentKey)
        {
            var queue = new Queue<Tree<T>>();
            var currentNode = this;
            queue.Enqueue(currentNode);
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue();
                if (currentNode.Value.Equals(parentKey))
                {
                    return currentNode;
                }
                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return null;
        }

        public IEnumerable<T> OrderBfs()
        {
            var result = new List<T>();
            var queue = new Queue<Tree<T>>();
            var currentNode = this;
            queue.Enqueue(currentNode);
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue();
                result.Add(currentNode.Value);
                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return result;
        }

        public IEnumerable<T> OrderDfs()
        {
            var result = new List<T>();
            return this.Dfs(this, result);
        }
        private List<T> Dfs(Tree<T> tree, List<T> result)
        {
            foreach (var child in tree.Children)
            {
                Dfs(child, result);
            }
            result.Add(tree.Value);
            return result;
        }
        public IEnumerable<T> OrderDfsWithStack()
        {
            var result = new Stack<T>();
            var stack = new Stack<Tree<T>>();
            stack.Push(this);
            while (stack.Count > 0)
            {
                var node = stack.Pop();
                foreach (var child in node.Children)
                {
                    stack.Push(child);
                }
                result.Push(node.Value);
            }

            return result;
        }
        public void RemoveNode(T nodeKey)
        {
            var nodeToRemove = this.FindNode(nodeKey);
            if (nodeToRemove == null)
            {
                throw new ArgumentNullException();
            }
            if (nodeToRemove.Value.Equals(this.Value))
            {
                throw new ArgumentException();
            }
            var parrentNode = nodeToRemove.Parent;
            parrentNode.Children.Remove(nodeToRemove);            
        }

        public void Swap(T firstKey, T secondKey)
        {
            var firstNode = this.FindNode(firstKey);
            var secondNode = this.FindNode(secondKey);
            if (firstNode is null || secondNode is null)
            {
                throw new ArgumentNullException();
            }
            if (this.Value.Equals(firstKey) || this.Value.Equals(secondKey))
            {
                throw new ArgumentException();
            }
                var indexFirstNode = firstNode.Parent.Children.IndexOf(firstNode);
                var indexSecondNode = secondNode.Parent.Children.IndexOf(secondNode);

                var firstParent = firstNode.Parent;
                var secondParent = secondNode.Parent;
                firstNode.Parent.Children.Remove(firstNode);
                secondNode.Parent.Children.Remove(secondNode);

                firstNode.Parent = secondParent;
                secondNode.Parent = firstParent;

                firstParent.Children.Insert(indexFirstNode, secondNode);
                secondParent.Children.Insert(indexSecondNode, firstNode);

        }
    }
}

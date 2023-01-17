namespace Tree
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Tree<T> : IAbstractTree<T> , IEnumerable<Tree<T>>
    {
        private List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            this.Key = key;
            this.children = new List<Tree<T>>();

            foreach (var child in children)
            {
                this.AddChild(child);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            this.children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            this.Parent = parent;
        }

        public string AsString()
        {
            var sb = new StringBuilder();
            this.AsStringWithDfsParentFirst(this, sb, 0);
            //this.AsStringWithDfsChildrenFirst(this, sb);
            //this.AsStringWithBfs(this, sb);
            return sb.ToString().TrimEnd();
        }

        private void AsStringWithBfs(Tree<T> tree, StringBuilder sb)
        {
            var queue = new Queue<Tree<T>>();
            var currentNode = tree;
            queue.Enqueue(currentNode);
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue();
                sb.AppendLine(currentNode.Key.ToString());
                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        private void AsStringWithDfsChildrenFirst(Tree<T> tree, StringBuilder sb)
        {
            foreach (var child in tree.children)
            {
                this.AsStringWithDfsChildrenFirst(child, sb);
            }
            sb.AppendLine(tree.Key.ToString() + " ");
        }
        private void AsStringWithDfsParentFirst(Tree<T> node, StringBuilder sb, int countOfSpaces)
        {

            var stringForSpces = string.Concat(Enumerable.Repeat(" ", countOfSpaces));
            sb.AppendLine(stringForSpces + node.Key.ToString());
            foreach (var child in node.children)
            {
                AsStringWithDfsParentFirst(child, sb, countOfSpaces +2);
            }
            
        }

        public IEnumerable<T> GetInternalKeys()
        {
            var result = new List<T>();
            foreach (var child in this)
            {
                if (child.Parent != null && child.children.Count > 0)
                {
                    result.Add(child.Key);
                }
            }
            return result;
        }

        public IEnumerable<T> GetLeafKeys()
        {
            var result = new List<T>();
            foreach (var child in this)
            {
                if (child.children.Count == 0)
                {
                    result.Add(child.Key);
                }
            }
            return result;
        }

        public T GetDeepestKey()
        {
            var leafs = this.GetLeafs();
            var resultDepth = 0;
            T resultLeafKey = default;
            foreach (var leaf in leafs)
            {
                int leafDepth = this.CalculateDepth(leaf);
                if (leafDepth > resultDepth)
                {
                    resultDepth = leafDepth;
                    resultLeafKey = leaf.Key;
                }
            }

            return resultLeafKey;

        }

        private int CalculateDepth(Tree<T> leaf)
        {
            var currentDepth = 0;
            var currentLeaf = leaf;
            while (currentLeaf.Parent != null)
            {
                currentDepth++;
                currentLeaf = currentLeaf.Parent;

            }

            return currentDepth;
        }

        public List<Tree<T>> GetLeafs()
        {
            var result = new List<Tree<T>>();
            foreach (var child in this)
            {
                if (child.children.Count == 0)
                {
                    result.Add(child);
                }
            }
            return result;
        }

        public IEnumerable<T> GetLongestPath()
        {
            var result = new List<T>();
            var nodes = new Stack<T>();
            var deepestNode = this.GetDeepestNode();
            while (deepestNode.Parent != null)
            {
                nodes.Push(deepestNode.Key);
                deepestNode = deepestNode.Parent;
            }
            nodes.Push(deepestNode.Key);
            while (nodes.Count >0)
            {
                result.Add(nodes.Pop());
            }

            return result;
        }

        private Tree<T> GetDeepestNode()
        {
            var leafs = this.GetLeafs();
            var resultDepth = 0;
            Tree<T> resultLeaf = default;
            foreach (var leaf in leafs)
            {
                int leafDepth = this.CalculateDepth(leaf);
                if (leafDepth > resultDepth)
                {
                    resultDepth = leafDepth;
                    resultLeaf = leaf;
                }
            }

            return resultLeaf;
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
            result.Add(tree.Key);
            return result;
        }
        public IEnumerator<Tree<T>> GetEnumerator()
        {
            var queue = new Queue<Tree<T>>();
            var currentNode = this;
            queue.Enqueue(currentNode);
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue();
                yield return currentNode;
                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }

        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}

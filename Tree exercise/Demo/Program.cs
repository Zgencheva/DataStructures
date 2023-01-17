namespace Demo
{
    using System;
    using Tree;

    class Program
    {
        static void Main(string[] args)
        {
            var tree1 = new string[] { "9 17", "9 4", "9 14", "4 36", "14 53","14 59", "53 67", "53 73" };
            var treeFactory = new IntegerTreeFactory();

            var resultTree = treeFactory.CreateTreeFromStrings(tree1);
            //Console.WriteLine(resultTree.AsString());
            var longestPath = resultTree.GetLongestPath();
            foreach (var path in longestPath)
            {
                Console.WriteLine(path);
            }

        }
    }
}

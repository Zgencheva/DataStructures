using _01.RedBlackTree;

namespace Demo
{
    class Program
    {
        static void Main()
        {
            var rbt = new RedBlackTree<int>();
            rbt.Insert(2);
            rbt.Insert(1);
            rbt.Insert(3);

            var searchedTree = rbt.Search(2);
        }
    }
}

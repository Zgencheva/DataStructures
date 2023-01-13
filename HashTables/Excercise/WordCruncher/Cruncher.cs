//using System.Collections.Generic;
//using System.Linq;

//namespace WordCruncher
//{
//    internal class Cruncher
//    {
//        public Cruncher(string[] sylables, string word)
//        {
//            Sylables = sylables;
//            Word = word;
//        }

//        public Node Node { get; set; }
//        public string[] Sylables { get; }
//        public string Word { get; }

//        public Node OrderSyllabels(string[] syllables, string word)
//        {
//            for (int i = 0; i < syllables.Length; i++)
//            {
//                if (word.StartsWith(syllables[i]))
//                {
//                    this.Node.Syllable = syllables[i];
//                    this.Node.NexSyllable = this.OrderSyllabels(syllables
//                        .Where((x, index) => index != i).ToArray(), word);
//                }
//            }

//            return Node;
       
//        }
//    }
//}
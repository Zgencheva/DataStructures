using System;
using System.Collections.Generic;
using System.Linq;

namespace WordCruncher
{
    class Program
    {
        static void Main()
        {
            var syllables = Console.ReadLine().Split(", ");
            var word = Console.ReadLine();

            List<Node> groups = GenerateSyllablePaths(syllables, word);
            IEnumerable<string> result = Allpaths(groups);
            foreach (var path in result)
            {
                Console.WriteLine(path);
            }


          
        }
        public class Node
        {
            public Node(string syllable, List<Node> nextSyllables)
            {
                Syllable = syllable;
                this.NextSyllables = nextSyllables;
            }

            public string Syllable { get; set; }
            public List<Node> NextSyllables { get; set; }

            public override string ToString()
            {
                return this.Syllable.ToString();
            }
        }
        private static IEnumerable<string> Allpaths(List<Node> groups)
        {
           var result = new List<List<string>>();
            GenerateSyllablePathsResult(groups, new List<string>(), result);
            return new HashSet<string>(result.Select(x=> string.Join(" ", x)));
        }

        private static void GenerateSyllablePathsResult(List<Node> groups, List<string> currentPath, List<List<string>> result)
        {
            if (groups == null)
            {
                result.Add(new List<string>(currentPath));
                return;
            }

            foreach (var node in groups)
            {
                currentPath.Add(node.Syllable);
                GenerateSyllablePathsResult(node.NextSyllables, currentPath, result);

                currentPath.RemoveAt(currentPath.Count - 1);
            }

        }

        private static List<Node> GenerateSyllablePaths(string[] syllables, string word)
        {
            if (string.IsNullOrEmpty(word) || syllables.Length == 0)
            {
                return null;
            }
            var resulValues = new List<Node>();
            for (int i = 0; i < syllables.Length; i++)
            {
                var currentWord = syllables[i];

                if (word.StartsWith(currentWord))
                {
                    var nextSyllables = GenerateSyllablePaths(syllables
                        .Where((x, index) => index != i).ToArray(),
                        word.Substring(currentWord.Length));
                    resulValues.Add(new Node(currentWord, nextSyllables));
                }
            }

            return resulValues;
        }
    }
}

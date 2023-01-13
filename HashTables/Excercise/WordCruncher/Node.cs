using System;
using System.Collections.Generic;
using System.Text;

namespace WordCruncher
{
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
}

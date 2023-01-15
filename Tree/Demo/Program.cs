// See https://aka.ms/new-console-template for more information
using Tree;


var tree = new Tree<string>("A",
    new Tree<string>("B", 
        new Tree<string>("E"),
        new Tree<string>("G")
        ),
    new Tree<string>("C"),
    new Tree<string>("D",
        new Tree<string>("H")));
var BFS = tree.OrderDfs();
;
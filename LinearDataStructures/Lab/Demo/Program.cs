// See https://aka.ms/new-console-template for more information
using Problem01.List;
using Problem04.SinglyLinkedList;

var list = new SinglyLinkedList<int>();
list.AddFirst(1);
list.AddFirst(0);
list.RemoveLast();
Console.WriteLine(list.Count);
foreach (var item in list)
{
    Console.WriteLine(item);
}



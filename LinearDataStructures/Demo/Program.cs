// See https://aka.ms/new-console-template for more information
//using Problem01.List;


//var list = new Problem01.List.List<int>();
//list.Add(5);
//list.Add(8);
//list.Add(10);
//list.Add(0);
//list.Add(20);
//Console.WriteLine(list.Count);
//foreach (var item in list) 
//{
//    Console.WriteLine(item);

//}

var list = new List<int>();
list.Add(10);
list.Add(10);
list.Add(10);
list.Add(10);
list[0] = 5;
foreach (var item in list)
{
    Console.WriteLine(item);
}


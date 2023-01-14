using Hierarchy;

var hr = new Hierarchy<string>("Leonidas");
hr.Add("Leonidas", "Xena The Princess Warrior");
hr.Add("Leonidas", "General Protos");
hr.Add("Xena The Princess Warrior", "Gorok");
hr.Add("Xena The Princess Warrior", "Bozat");
hr.Add("General Protos", "Subotil");
hr.Add("General Protos", "Kira");
hr.Add("General Protos", "Zaler");

foreach (var value in hr)
{
    Console.WriteLine(value);
}

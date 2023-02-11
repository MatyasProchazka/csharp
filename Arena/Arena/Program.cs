using Arena;

Kostka kostka = new Kostka();

Bojovnik karel = new Bojovnik("Karel", 20, 5, 3, kostka);

Console.WriteLine(karel); 
Console.WriteLine(karel.Nazivu());
Console.WriteLine(karel.GrafickyZivot());

Console.ReadKey();
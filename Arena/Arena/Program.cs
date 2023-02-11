using Arena;

Kostka kostka = new Kostka();

Bojovnik bojovnik = new Bojovnik("Zalgoren", 100, 20, 10, kostka);

Bojovnik souper = new Bojovnik("Shadow", 60, 18, 15, kostka);

Console.WriteLine(bojovnik.GrafickyZivot());

souper.Utoc(bojovnik);

Console.WriteLine(souper.VratPosledniZpravu());
Console.WriteLine(bojovnik.VratPosledniZpravu());

Console.WriteLine(bojovnik.GrafickyZivot());
Console.ReadKey();
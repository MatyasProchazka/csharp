using Arena;

Kostka kostka = new Kostka(10);

Bojovnik bojovnik = new Bojovnik("Zalgoren", 100, 20, 10, kostka);
Bojovnik souper = new Mag("Shadow", 60, 18, 15, kostka, 30, 45);
ArenaBojovniku arena = new ArenaBojovniku(bojovnik, souper, kostka);

arena.Zapas();

Console.ReadKey();
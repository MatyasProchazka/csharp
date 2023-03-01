using Arena;

Console.WriteLine("Vítej v aréně! Nejprve si musiš vytvořit zápasníka, jak si přeješ se jmenovat?");
string jmenoZapasnika = Console.ReadLine();
while (string.IsNullOrEmpty(jmenoZapasnika))
{
    Console.WriteLine("neplatne zadani");
    jmenoZapasnika = Console.ReadLine();
}
Console.WriteLine("Ahoj {0}! Tvé počáteční staty jsou: 20 bodu utoku, 20 bodu obrany a 100 bodu maximalniho zdravi", jmenoZapasnika);


Kostka kostka = new Kostka(10);

Bojovnik bojovnik = new Bojovnik(jmenoZapasnika, 100, 20, 20, kostka);
Bojovnik souper = new Bojovnik("Golem", 60, 18, 15, kostka);
ArenaBojovniku arena = new ArenaBojovniku(bojovnik, souper, kostka);

bool zapasit = true;

while (zapasit)
{
    Console.WriteLine("Arena");
    Console.WriteLine("Menu:");
    Console.WriteLine("1) Zapasit \n2) Ukaz staty meho bojovnika \n3) Vylepsit moje staty \n4) Odejit (ztrati se vsechen postup)");
    string volbaMenu = Console.ReadLine();

    while (string.IsNullOrEmpty(volbaMenu))
    {
        Console.WriteLine("neplatny udaj, napis prosim platnou volbu");
        volbaMenu = Console.ReadLine();
    }

    switch (volbaMenu)
    {
        case "1":
            arena.Zapas();
            break;
        case "2":
            bojovnik.VypisStaty();
            break;
        case "3":
            Console.WriteLine("ZLEPSENI STATU ZA PENIZE! (zatim bez penez)\n1) pridat body do utoku\n2) pridat body do obrany\n3) pridat body do maximalniho zdravi\n4) zpatky do menu");
            string volbaMenuStaty = Console.ReadLine();
            Console.WriteLine("Zadej pozadovany pocet bodu, ktere chces pridat");
            int pocetBoduZvolenehoStatu = Convert.ToInt32(Console.ReadLine());

            bojovnik.PridatStat(volbaMenuStaty, pocetBoduZvolenehoStatu);

            break;
        case "4":
            zapasit = false;
            break;
        default:
            Console.WriteLine("Zadané číslo nebylo v nabídce\n");
            break;
    }
}

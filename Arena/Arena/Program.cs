using Arena;

Console.WriteLine("Vítej v aréně! Nejprve si musiš vytvořit zápasníka, jak si přeješ se jmenovat?");
string jmenoZapasnika = Console.ReadLine();
// checkuje jestli je zadany vstup platny
while (string.IsNullOrEmpty(jmenoZapasnika))
{
    Console.WriteLine("neplatne zadani");
    jmenoZapasnika = Console.ReadLine();
}
Console.WriteLine("Ahoj {0}! Tvé počáteční staty jsou: 20 bodu utoku, 20 bodu obrany a 100 bodu maximalniho zdravi", jmenoZapasnika);
Console.ReadKey();

//tvorba instance kostky
Kostka kostka = new Kostka(10);

//tvorba instance hrace a pocitacoveho protivnika, oba maji dane staty ze zacatku (v budoucnu lehce nahodne)
Bojovnik bojovnik = new Bojovnik(jmenoZapasnika, 85, 20, 20, kostka);
Bojovnik souper = new Bojovnik("Golem", 60, 15, 13, kostka);
ArenaBojovniku arena = new ArenaBojovniku(bojovnik, souper, kostka);

//podminka pro beh hlavniho menu
bool zapasit = true;

// hlavni loop
while (zapasit)
{
    Console.Clear();
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
        //zacne zapas
        case "1":
            arena.Zapas();
            break;
        //vypise aktualni staty hrace
        case "2":
            bojovnik.VypisStaty();
            break;
        //menu na zvyseni statu o zvolenou hodnotu (do budoucna za herni menu)
        case "3":
            string[] moznosti = { "1", "2", "3", "4" };
            Console.Clear();
            Console.WriteLine("ZLEPSENI STATU ZA PENIZE!\nAktuální zůstatek: {0}\n1) pridat body do utoku ({1} zlataky/bod)\n2) pridat body do obrany ({2} zlataky/bod)\n3) pridat body do maximalniho zdravi({3} zlataky/bod)\n4) zpatky do menu", bojovnik.Penize, bojovnik.ZjistitCenuZaStat("1"), bojovnik.ZjistitCenuZaStat("2"), bojovnik.ZjistitCenuZaStat("3"));
            string volbaMenuStaty = Console.ReadLine();
            while (!moznosti.Contains(volbaMenuStaty))
            {
                Console.WriteLine("toto neni v menu, zadej prosim tvoji volbu znovu:");
                volbaMenuStaty = Console.ReadLine();
            }
            if (volbaMenuStaty == "4")
            {
                break;
            }
            Console.WriteLine(bojovnik.ZjistitCenuZaStat(volbaMenuStaty));
            Console.WriteLine("Zadej pozadovany pocet bodu, ktere chces pridat");
            //zde se musi pridat exception handling kdyz se vlozi neco jineho nez cislo!!!
            int pocetBoduZvolenehoStatu = Convert.ToInt32(Console.ReadLine());

            //prida dany stat podle zadane hodnoty
            bojovnik.PridatStat(volbaMenuStaty, pocetBoduZvolenehoStatu);
            break;
        //opusteni hry
        case "4":
            Console.WriteLine("Zvládl jsi zdolat {0} protivníků! Klikni pro ukončení", bojovnik.PocetKol);
            Console.ReadKey();
            zapasit = false;
            break;
        //pripad kdy uzivatel zada neco jineho nez v menu
        default:
            Console.WriteLine("Zadané číslo nebylo v nabídce\n");
            break;
    }
}

Console.WriteLine("Vitej v kalkulacce");

bool pokracovat = true;

while(pokracovat)
{
    Console.WriteLine("zadejete prvni cislo:");
    float a = float.Parse(Console.ReadLine());

    Console.WriteLine("zadejte druhe cislo:");
    float b = float.Parse(Console.ReadLine());

    Console.WriteLine("Zvolte si operaci:");
    Console.WriteLine("1 - sčítání");
    Console.WriteLine("2 - odčítání");
    Console.WriteLine("3 - násobení");
    Console.WriteLine("4 - dělení");

    char volba = Console.ReadKey().KeyChar;
    float vysledek = 0;
    bool platnaVolba = true;
    switch (volba)
    {
        default: 
            platnaVolba = false;
            break;
        case '1':
            vysledek = a + b;
            break;
        case '2':
            vysledek = a - b;
            break;
        case '3':
            vysledek = a * b;
            break;
        case '4':
            vysledek = a / b;
            break;
    }

    if (platnaVolba)
    {
        Console.WriteLine();
        Console.WriteLine("Vysledek: {0}", vysledek);
    }
    else
    {
        Console.WriteLine("neplatna volba");
    }

    Console.WriteLine("dalsi priklad? [a/n]");
    platnaVolba = false;

    while (!platnaVolba)
    {
        switch (Console.ReadKey().KeyChar.ToString().ToLower())
        {
            case "a":
                pokracovat= true;
                platnaVolba= true;
                break;
            case "n":
                pokracovat = false;
                platnaVolba = true;
                break;
            default:
                Console.WriteLine("zadejte a/n");
                break;
        }
    }
    Console.WriteLine();
}
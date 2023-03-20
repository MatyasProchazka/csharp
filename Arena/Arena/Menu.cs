public class Menu
{
    private string s = "Arena\nMenu:\n1) Zapasit \n2) Dungeon - READY\n3) Muj bojovnik \n4) Inventar \n5) Vylepsit moje staty \n6) Obchod\n7) Odejit (ztrati se vsechen postup)";
    public bool InMenu { get; set; }
    public void OnDungeonTimerFinished(object sender, EventArgs e)
    {
        s = "Arena\nMenu:\n1) Zapasit \n2) Dungeon - READY\n3) Muj bojovnik \n4) Inventar \n5) Vylepsit moje staty \n6) Obchod\n7) Odejit (ztrati se vsechen postup)";
        if (InMenu)
        {
            VypsatMenu();
        }
    }

    public void OnDungeonEntered(object sender, EventArgs e)
    {
        s = "Arena\nMenu:\n1) Zapasit \n2) Dungeon\n3) Muj bojovnik \n4) Inventar \n5) Vylepsit moje staty \n6) Obchod\n7) Odejit (ztrati se vsechen postup)";
    }
    public void VypsatMenu()
    {
        Console.Clear();
        Console.WriteLine(s);
    }
}
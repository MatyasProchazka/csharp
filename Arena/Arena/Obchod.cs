using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Obchod
    {

        private Bojovnik bojovnik;

        Kostka kostka = new Kostka();

        public int VelikostObchodu { get; private set; }

        List<Zbran> ZbraneList = new List<Zbran>();

        //vsechna mozna jmena pro zbrane, pri tvorbe se vybira z nich, lze pridat libovolny pocet nazvu
        String[] jmenaZbrani = { "Mjolnir", "Endbringer", "Lament", "Twilight Sculptor", "Desire's Spellblade", "Guard's Mithril Katana", "Bloodweep", "Night's Fall" };

        public Obchod(Bojovnik bojovnik, int velikostObchodu)
        {
            this.bojovnik = bojovnik;
            VelikostObchodu = velikostObchodu;

            for (int i = 0; i < VelikostObchodu; i++)
            {
                PridatZbran(i);
            }
        }

        public void PridatZbran(int index)
        {
            int utok = NahodnyUtok(bojovnik.PocetKol);
            ZbraneList.Insert(index, new Zbran(VygenerovatJmeno(), utok, NahodnyKritickaSance(bojovnik.PocetKol), VypocitatCenuZbrane(utok, bojovnik.PocetKol)));
        }

        public void VypsatNabidku()
        {
            Console.WriteLine(NahodnyUtok(1));
            int idVypis = 1;
            Console.WriteLine("OBCHOD\nAktuální zůstatek: {0} zlataku", bojovnik.Penize);
            foreach (Zbran zbran in ZbraneList) 
            { 
                Console.WriteLine("{4}) Zbran: {0}, {1}, {2}, {3}", zbran.Jmeno, zbran.Utok, zbran.KrtitickaSance, zbran.Cena, idVypis);
                idVypis++;
            }
        }
        public void KoupitZbran(int moznost)
        {
            // MUSIM PRIDAT EXCEPTION HANDELING KDYZ CISLO MIMO LIST!!!!
            if (moznost != 0 && moznost <=4)
            {
                if (bojovnik.Penize >= ZbraneList[moznost - 1].Cena)
                {
                    Console.WriteLine("koupena zbran: {0}!", ZbraneList[moznost - 1].Jmeno);
                    bojovnik.UbratPenize(ZbraneList[moznost - 1].Cena);
                    bojovnik.ZmenitZbran(ZbraneList[moznost - 1]);
                    ZbraneList.RemoveAt(moznost - 1);
                    PridatZbran(moznost - 1);
                }
                else
                {
                    Console.WriteLine("Nedostatek financí");
                }
            }
            else
            {
                Console.WriteLine("Neplatny vstup, kliknutim se vratis zpatky do menu");
            }
            
        }

        private int NahodnyUtok(int pocetKol)
        {
            return kostka.VygenerovatCislo(pocetKol + 1, 2 + (pocetKol * 2)) + bojovnik.PocetKol + kostka.VygenerovatCislo(0, 6);
        }
        
        private int NahodnyKritickaSance(int pocetKol)
        {
            int vratitSanci = (pocetKol * 2) + kostka.VygenerovatCislo(1, 10);
            if (vratitSanci < 100)
            {
                return vratitSanci;
            }
            else
            {
                return 100;
            }

        }
        private int VypocitatCenuZbrane(int utokZbrane, int pocetKol)
        {
            double cena = Math.Round((utokZbrane / 1.5) + pocetKol + kostka.VygenerovatCislo(pocetKol, pocetKol * 3));
            int vratitCenu = Convert.ToInt32(cena);
            return vratitCenu;
        }

        private string VygenerovatJmeno()
        {
            return jmenaZbrani[kostka.VygenerovatCislo(1, jmenaZbrani.Length)];
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Arena
{
    internal class ArenaBojovniku
    {
        /// <summary>
        /// instance prvniho bojovnika
        /// </summary>
        private Bojovnik hrac;
        /// <summary>
        /// instance druheho bojovnika
        /// </summary>
        private Bojovnik npc;
        /// <summary>
        /// instance hraci kostky
        /// </summary>
        private Kostka kostka;

        private int pridanePenize;

        /// <summary>
        /// konstruktor tridy arena
        /// </summary>
        /// <param name="bojovnik1"></param>
        /// <param name="bojovnik2"></param>
        /// <param name="kostka"></param>
        public ArenaBojovniku(Bojovnik hrac, Bojovnik npc, Kostka kostka)
        {
            this.hrac = hrac;
            this.npc = npc;
            this.kostka = kostka;
        }
        /// <summary>
        /// vykresleni zakladnich informaci
        /// </summary>
        private void Vykresli()
        {
            Console.Clear();
            Console.WriteLine("-------------- Aréna -------------- \n");
            Console.WriteLine("Bojovníci: \n");
            VypisBojovnika(hrac);
            Console.WriteLine();
            VypisBojovnika(npc);
            Console.WriteLine();    
        }

        private void VypisBojovnika(Bojovnik b)
        {
            Console.WriteLine(b);
            Console.Write("Zivot: ");
            Console.WriteLine(b.GrafickyZivot());

            if (b is Mag)
            {
                Console.Write("Mana: ");
                Console.WriteLine(((Mag)b).GrafickaMana());
            }

        }

        /// <summary>
        /// vypsani zpravy s dramatickou pauzou
        /// </summary>
        /// <param name="zprava"></param>
        private void VypisZpravu(string zprava)
        {
            Console.WriteLine(zprava);
            //pauza pri boji, vetsi cislo == delsi pausa a pomalejsi souboj
            Thread.Sleep(650);
        }

        public void Zapas()
        {
            Console.Clear();
            Console.WriteLine("Vítejte v aréně!");
            Console.WriteLine("Dnes se utkají {0} s {1}! \n", hrac, npc);
            // prohození bojovníků
            Console.WriteLine("Zapas muze zacit...");
            Console.ReadKey();
            // cyklus s bojem
            while (hrac.Nazivu() && npc.Nazivu())
            {
                hrac.Utoc(npc);
                Vykresli();
                VypisZpravu(hrac.VratPosledniZpravu()); // zpráva o útoku
                VypisZpravu(npc.VratPosledniZpravu()); // zpráva o obraně
                if (npc.Nazivu())
                {
                    npc.Utoc(hrac);
                    Vykresli();
                    VypisZpravu(npc.VratPosledniZpravu()); // zpráva o útoku
                    VypisZpravu(hrac.VratPosledniZpravu()); // zpráva o obraně
                }
                Console.WriteLine();
            }

            if (hrac.Nazivu())
            { 
                //nastavi hraci a npc zdravi na max a zesili npc
                hrac.VylecitBojovnika();
                hrac.PocetKol += 1;
                npc.PridatStatyNPC();
                npc.VylecitBojovnika();
                pridanePenize = 3 + kostka.Hod() + hrac.PocetKol * 2;
                hrac.PridatPenize(pridanePenize);
                Console.WriteLine("vyhral {0}, získal {1} zlaťáků a ma {2} vyhranych zapasu", hrac, pridanePenize, hrac.PocetKol);
                Console.ReadKey();

            }
            else
            {
                Console.WriteLine("vyhral {0} a {1} zemrel a hra konci po {2} vyhranych zapasech", npc, hrac, hrac.PocetKol);
                Console.ReadKey();
                System.Environment.Exit(1);
            }
        }

    }
}

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
        /// instance hraci kostky
        /// </summary>
        private Kostka kostka;

        private bool dungeonReady = true;

        public event EventHandler DungeonTimerFinished;
        public event EventHandler DungeonEntered;

        /// <summary>
        /// konstruktor tridy arena
        /// </summary>
        /// <param name="bojovnik1"></param>
        /// <param name="bojovnik2"></param>
        /// <param name="kostka"></param>
        public ArenaBojovniku(Bojovnik hrac, Kostka kostka)
        {
            this.hrac = hrac;
            this.kostka = kostka;
        }
        /// <summary>
        /// vykresleni zakladnich informaci
        /// </summary>
        private void Vykresli(Bojovnik protivnik)
        {
            Console.Clear();
            Console.WriteLine("-------------- Aréna -------------- \n");
            Console.WriteLine("Bojovníci: \n");
            VypisBojovnika(hrac);
            Console.WriteLine();
            VypisBojovnika(protivnik);
            Console.WriteLine();
        }

        /// <summary>
        /// vypise jmeno, zivoty a popr. manu bojovnika
        /// </summary>
        /// <param name="b"></param>
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
        private void VypisZpravu(string zprava, int uspani = 500)
        {
            Console.WriteLine(zprava);
            //pauza pri boji, vetsi cislo == delsi pausa a pomalejsi souboj
            Thread.Sleep(uspani);
        }

        /// <summary>
        /// simulace zapasu, stridani utoku hrace a npc
        /// </summary>
        public void Zapas()
        {
            Bojovnik protivnik = hrac.VygenerovatProtivnika();
            Console.Clear();
            Console.WriteLine("Vítejte v aréně!");
            Console.WriteLine("Dnes se utkají {0} s {1}! \n", hrac, protivnik);
            // prohození bojovníků
            Console.WriteLine("Zapas muze zacit...");
            Console.ReadKey();
            // cyklus s bojem
            while (hrac.Nazivu() && protivnik.Nazivu())
            {
                //utoceni hrace
                hrac.Utoc(protivnik);
                Vykresli(protivnik);
                VypisZpravu(hrac.VratPosledniZpravu()); // zpráva o útoku
                VypisZpravu(protivnik.VratPosledniZpravu()); // zpráva o obraně
                if (protivnik.Nazivu())
                {
                    //utoceni NPC
                    protivnik.Utoc(hrac);
                    Vykresli(protivnik);
                    VypisZpravu(protivnik.VratPosledniZpravu()); // zpráva o útoku
                    VypisZpravu(hrac.VratPosledniZpravu()); // zpráva o obraně
                }
                Console.WriteLine();
            }

            if (hrac.Nazivu())
            {
                //nastavi hraci a npc zdravi na max a zesili npc, prida penize hraci
                hrac.VylecitBojovnika();
                hrac.PocetKol += 1;
                int pridanePenize = 3 + kostka.Hod() + hrac.PocetKol * 2;
                hrac.PridatPenize(pridanePenize);
                Console.WriteLine("vyhral {0}, získal {1} zlaťáků a ma {2} vyhranych zapasu", hrac, pridanePenize, hrac.PocetKol);
                Console.ReadKey();

            }
            else
            {
                //hrac umrel a hra se vypina (bude predelano)
                Console.WriteLine("vyhral {0}, musis se vylepsit a zkusit to znovu", protivnik);
                Console.ReadKey();
                hrac.VylecitBojovnika();
            }
        }

        public void Dungeon()
        {
            if (dungeonReady)
            {
                OnDungeonEntered();
                Console.WriteLine("vitej v dungeonu, zde se muzes vylepsit prochazenim pater proti cim dal tim tezsim nepritelum");
                Console.ReadKey();

                int pocetPoschodi = 1;
                int RychlostZprav = 200;
                int odmena = 0;
                while (hrac.Nazivu())
                {
                    Bojovnik protivnik = hrac.VygenerovatProtivnikaDuengoen(pocetPoschodi);
                    while (protivnik.Nazivu())
                    {
                        if (hrac.Nazivu())
                        {
                            hrac.Utoc(protivnik);
                            Vykresli(protivnik);
                            VypisZpravu(hrac.VratPosledniZpravu(), RychlostZprav); // zpráva o útoku
                            VypisZpravu(protivnik.VratPosledniZpravu(), RychlostZprav); // zpráva o obraně
                            if (protivnik.Nazivu())
                            {
                                //utoceni NPC
                                protivnik.Utoc(hrac);
                                Vykresli(protivnik);
                                VypisZpravu(protivnik.VratPosledniZpravu(), RychlostZprav); // zpráva o útoku
                                VypisZpravu(hrac.VratPosledniZpravu(), RychlostZprav); // zpráva o obraně
                            }
                            Console.WriteLine();
                        }
                        else
                        {
                            Console.WriteLine($"hrac {hrac} umrel");
                            break;
                        }

                    }
                    pocetPoschodi += 1;
                    double pripsanaCastka = pocetPoschodi * (1 + (Math.Ceiling(Convert.ToDouble(hrac.PocetKol) / 10)));
                    if (pocetPoschodi < 10)
                    {
                        pripsanaCastka = pripsanaCastka / 2;
                        odmena += Convert.ToInt32(Math.Round(pripsanaCastka));
                    }

                }
                Console.WriteLine($"hrac {hrac} prekonal {pocetPoschodi} pater dungeonu a ziskal {odmena} zlataku");
                hrac.VylecitBojovnika();
                hrac.PridatPenize(odmena);

                dungeonReady = false;
                new Thread(() => { dungeonReady = CasovacDungeon(); }).Start();

                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Ještě neuplynul cooldown na dungeon, prijd za chvili");
                Console.ReadKey();
            }
        }

        private bool CasovacDungeon()
        {
            int cooldownDungeon = 30000;
            Thread.Sleep(cooldownDungeon);
            Console.WriteLine("");
            OnDungeonTimerFinished();

            return true;
        }

        protected virtual void OnDungeonTimerFinished()
        {
            if (DungeonTimerFinished != null)
            {
                DungeonTimerFinished(this, EventArgs.Empty);
            }
        }

        protected virtual void OnDungeonEntered()
        {
            if (DungeonEntered!= null)
            {
                DungeonEntered(this, EventArgs.Empty);
            }
        }
    }
}

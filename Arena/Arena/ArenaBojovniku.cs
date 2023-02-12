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
        private Bojovnik bojovnik1;
        /// <summary>
        /// instance druheho bojovnika
        /// </summary>
        private Bojovnik bojovnik2;
        /// <summary>
        /// instance hraci kostky
        /// </summary>
        private Kostka kostka;

        /// <summary>
        /// konstruktor tridy arena
        /// </summary>
        /// <param name="bojovnik1"></param>
        /// <param name="bojovnik2"></param>
        /// <param name="kostka"></param>
        public ArenaBojovniku(Bojovnik bojovnik1, Bojovnik bojovnik2, Kostka kostka)
        {
            this.bojovnik1 = bojovnik1;
            this.bojovnik2 = bojovnik2;
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
            VypisBojovnika(bojovnik1);
            Console.WriteLine();
            VypisBojovnika(bojovnik2);
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
            Thread.Sleep(500);
        }

        public void Zapas()
        {
            Bojovnik b1 = bojovnik1;
            Bojovnik b2 = bojovnik2;
            Console.WriteLine("Vítejte v aréně!");
            Console.WriteLine("Dnes se utkají {0} s {1}! \n", bojovnik1, bojovnik2);
            // prohození bojovníků
            bool zacinaBojovnik2 = (kostka.Hod() <= kostka.VratPocetStran() / 2);
            if (zacinaBojovnik2)
            {
                b1 = bojovnik2;
                b2 = bojovnik1;
            }
            Console.WriteLine("Začínat bude bojovník {0}! \nZápas může začít...", b1);
            Console.ReadKey();
            // cyklus s bojem
            while (b1.Nazivu() && b2.Nazivu())
            {
                b1.Utoc(b2);
                Vykresli();
                VypisZpravu(b1.VratPosledniZpravu()); // zpráva o útoku
                VypisZpravu(b2.VratPosledniZpravu()); // zpráva o obraně
                if (b2.Nazivu())
                {
                    b2.Utoc(b1);
                    Vykresli();
                    VypisZpravu(b2.VratPosledniZpravu()); // zpráva o útoku
                    VypisZpravu(b1.VratPosledniZpravu()); // zpráva o obraně
                }
                Console.WriteLine();
            }

            if (b1.Nazivu())
            {
                Console.WriteLine("vyhral {0}", b1);
            }
            else
            {
                Console.WriteLine("vyhral {0}", b2);
            }
        }

    }
}

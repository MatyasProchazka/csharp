using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Kostka
    {
        /// <summary>
        /// pocet stran kostky
        /// </summary>
        private int pocetStran;
        /// <summary>
        /// generator nahodny cisel
        /// </summary>
        private Random random;
        ///
        public Kostka(int aPocetStran = 6)
        {
            pocetStran = aPocetStran;
            random = new Random();
        }
        /// <summary>
        /// vrátí počet stran hraci kostky
        /// </summary>
        /// <returns>pocet stran hraci kostky</returns>
        public int VratPocetStran()
        {
            return pocetStran;
        }
        /// <summary>
        /// vykona hod kostkou
        /// </summary>
        /// <returns>cislo od 1 do poctu stran</returns>
        public int Hod()
        {
            return random.Next(1, pocetStran + 1);
        }
        public override string ToString()
        {
            return String.Format("kostka s {0} stranami", pocetStran);
        }

        public int VygenerovatCislo(int minPocet, int maxPocet)
        {
            return random.Next(minPocet, maxPocet);
        }
    }
}

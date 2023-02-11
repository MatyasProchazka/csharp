using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Bojovnik
    {
        /// <summary>
        /// jmeno bojovnika
        /// </summary>
        private string jmeno;
        /// <summary>
        /// aktualni pocet hp bojovnika
        /// </summary>
        private int zivot;
        /// <summary>
        /// maximalni pocet hp bojovnika
        /// </summary>
        private int maxZivot;
        /// <summary>
        /// utok v hp
        /// </summary>
        private int utok;
        /// <summary>
        /// obrana v hp
        /// </summary>
        private int obrana;
        /// <summary>
        /// instance hraci kostky
        /// </summary>
        private Kostka kostka;

        /// <summary>
        /// konstruktor pro atributy
        /// </summary>
        /// <param name="jmeno"></param>
        /// <param name="zivot"></param>
        /// <param name="utok"></param>
        /// <param name="obrana"></param>
        /// <param name="kostka"></param>
        public Bojovnik(string jmeno, int zivot, int utok, int obrana, Kostka kostka)
        {
            this.jmeno = jmeno;
            this.zivot = zivot;
            this.maxZivot = zivot;
            this.utok = utok;
            this.obrana = obrana;
            this.kostka = kostka;
        }
        /// <summary>
        /// vypsani jmena bojovnika
        /// </summary>
        /// <returns>jmeno bojovnika</returns>
        public override string ToString()
        {
            return jmeno;
        }
        /// <summary>
        /// vypise jestli je bojovnik zivy
        /// </summary>
        /// <returns>true nebo false jestli bojovnik zije</returns>
        public bool Nazivu()
        {
            return (zivot > 0);
        }
        /// <summary>
        /// vypise graficky aktualni pocet hp
        /// </summary>
        /// <returns>graficky zapis momentalnich hp</returns>
        public string GrafickyZivot()
        {
            string s = "[";
            int celkem = 20;
            double pocet = Math.Round(((double)zivot /maxZivot) * celkem);
            if ((pocet == 0) && (Nazivu()))
            {
                pocet = 1;
            }
            for (int i = 0; i < pocet; i++)
            {
                s += "#";
            }
            s = s.PadRight(celkem + 1);
            s += "]";
            return s;
        }
    }
}

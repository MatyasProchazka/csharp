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

        private string zprava;

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
        /// <summary>
        /// strhne hp pri utoku soupere
        /// </summary>
        /// <param name="uder"></param>
        public void BranSe(int uder)
        {
            int zraneni = uder - (obrana - kostka.Hod());
            if (zraneni > 0)
            {
                zivot -= zraneni;
                zprava = String.Format("{0} utrpěl poškození {1} hp", jmeno, zraneni);
                if (zivot <= 0)
                {
                    zivot = 0;
                    zprava += " a zemrel";
                }
            }
            else
            {
                zprava = String.Format("{0} odrazil utok", jmeno);
            }
            NastavZpravu(zprava);
        }
        /// <summary>
        /// ubere hp protivnikovi
        /// </summary>
        /// <param name="souper"></param>
        public void Utoc(Bojovnik souper)
        {
            int uder = utok + kostka.Hod();
            NastavZpravu(String.Format("{0} utoci s uderem za {1} hp", jmeno, uder));
            souper.BranSe(uder);

        }

        private void NastavZpravu(string zprava)
        {
            this.zprava = zprava;
        }

        public string VratPosledniZpravu()
        {
            return zprava;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Mag: Bojovnik
    {
        /// <summary>
        /// mana maga
        /// </summary>
        private int mana;
        /// <summary>
        /// maximalni mana maga
        /// </summary>
        private int maxMana;
        /// <summary>
        /// hodnota magickeho utoku maga
        /// </summary>
        private int magickyUtok;

        /// <summary>
        /// konstruktor tridy maga
        /// </summary>
        /// <param name="jmeno"></param>
        /// <param name="zivot"></param>
        /// <param name="utok"></param>
        /// <param name="obrana"></param>
        /// <param name="kostka"></param>
        /// <param name="mana"></param>
        /// <param name="magickyUtok"></param>
        public Mag(string jmeno, int zivot, int utok, int obrana, Kostka kostka, Zbran zbran, int mana, int magickyUtok): base(jmeno, zivot, utok, obrana, kostka, zbran) 
        {
            this.mana = mana;
            this.maxMana = mana;
            this.magickyUtok = magickyUtok;
        }

        /// <summary>
        /// zautoci pomoci magickeho utoku jestli ma dostatek many, jinak zautoci normalne a doplni si manu
        /// </summary>
        /// <param name="souper"></param>
        public override void Utoc(Bojovnik souper)
        {
            if (mana < maxMana)
            {
                mana += 10;

                if (mana > maxMana)
                {
                    mana = maxMana;
                }
                base.Utoc(souper);
            }
            else
            {
                int uder = magickyUtok + kostka.Hod();
                NastavZpravu(String.Format("{0} použil magii za {1} hp", jmeno, uder));
                souper.BranSe(uder);
                mana = 0;
            }
        }

        public string GrafickaMana()
        {
            return GrafickyUkazatel(mana, maxMana);
        }

    }
}

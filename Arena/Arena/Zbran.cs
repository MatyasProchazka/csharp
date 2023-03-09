using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Zbran
    {
        /// <summary>
        /// jmeno zbrane
        /// </summary>
        public string Jmeno { get; private set; }
        /// <summary>
        /// sila utoku zbrane
        /// </summary>
        public int Utok { get; private set; }
        /// <summary>
        /// sance na kriticky zasah zbrane
        /// </summary>
        public int KrtitickaSance { get; private set; }
        /// <summary>
        /// cena koupe zbrane
        /// </summary>
        public int Cena { get; private set; }

        public Zbran(string jmeno, int utok, int krtitickaSance, int cena)
        {
            Jmeno = jmeno;
            Utok = utok;
            KrtitickaSance = krtitickaSance;
            Cena = cena;
        }

        /// <summary>
        /// provede utok a zohledni sanci na kriticky zasah
        /// </summary>
        /// <returns>kolik bodu hp zthrne</returns>
        public int Utoc()
        {
            if (JeKritickyZasah())
            {
                //vetsi nasobek -> silnejsi kriticky zasah
                return Utok*3;
            }
            else
            {
                return Utok;
            }
            
        }

        /// <summary>
        /// vygeneruje nahodne cislo a rozhodne, jestli byl utok kritickym zasahem nebo ne
        /// </summary>
        /// <returns>jestli je utok kritivkym zasahem</returns>
        private bool JeKritickyZasah()
        {
            Random random= new Random();
            int cislo = random.Next(1, 101);
            return cislo <= KrtitickaSance;
        }

        public override string ToString()
        {
            return Jmeno;
        }

    }
}

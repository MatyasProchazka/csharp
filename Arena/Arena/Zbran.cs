using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Zbran
    {
        //public int Id { get; set; }
        public string Jmeno { get; private set; }

        public int Utok { get; private set; }

        public int KrtitickaSance { get; private set; }

        public int Cena { get; private set; }

        public Zbran(string jmeno, int utok, int krtitickaSance, int cena)
        {
            //Id = id;
            Jmeno = jmeno;
            Utok = utok;
            KrtitickaSance = krtitickaSance;
            Cena = cena;
        }

        public int Utoc()
        {
            if (JeKritickyZasah())
            {
                return Utok*3;
            }
            else
            {
                return Utok;
            }
            
        }

        private bool JeKritickyZasah()
        {
            Random random= new Random();
            int cislo = random.Next(1, 101);
            return cislo <= KrtitickaSance;
        }

        public void ZvysitUtok(int hodnota)
        {
            Utok += hodnota;
        }



        public override string ToString()
        {
            return Jmeno;
        }

    }
}

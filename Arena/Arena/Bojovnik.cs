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
        protected string jmeno;
        /// <summary>
        /// aktualni pocet hp bojovnika
        /// </summary>
        protected int zivot;
        /// <summary>
        /// maximalni pocet hp bojovnika
        /// </summary>
        protected int maxZivot;
        /// <summary>
        /// utok v hp
        /// </summary>
        protected int utok;
        /// <summary>
        /// obrana v hp
        /// </summary>
        protected int obrana;
        /// <summary>
        /// instance hraci kostky
        /// </summary>
        protected Kostka kostka;

        private string zprava;

        public int PocetKol { get; set; }

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
        /// vypise graficky aktualni pocet parametru
        /// </summary>
        /// <returns>graficky zapis momentalnich parametru</returns>
        protected string GrafickyUkazatel(int aktualni, int maximalni)
        {
            string s = "[";
            int celkem = 20;
            double pocet = Math.Round(((double)aktualni /maximalni) * celkem);
            if ((pocet == 0) && (Nazivu()))
            {
                pocet = 1;
            }
            for (int i = 0; i < pocet; i++)
            {
                s += "█";
            }
            s = s.PadRight(celkem + 1);
            s += "]";
            return s;
        }
        /// <summary>
        /// zobrazi graficky hp
        /// </summary>
        /// <returns>graficky zapis hp</returns>
        public string GrafickyZivot()
        {
            return GrafickyUkazatel(zivot, maxZivot);
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
        public virtual void Utoc(Bojovnik souper)
        {
            int uder = utok + kostka.Hod();
            NastavZpravu(String.Format("{0} utoci s uderem za {1} hp", jmeno, uder));
            souper.BranSe(uder);

        }
        /// <summary>
        /// nastavi zpravu ktera se bude vypisovat
        /// </summary>
        /// <param name="zprava"></param>
        protected void NastavZpravu(string zprava)
        {
            this.zprava = zprava;
        }
        /// <summary>
        /// vrati zpravu ktera je naposledy ulozena
        /// </summary>
        /// <returns>zprava ktera se vypise</returns>
        public string VratPosledniZpravu()
        {
            return zprava;
        }

        /// <summary>
        /// vypise aktualni staty hrace
        /// </summary>
        public void VypisStaty()
        {
            Console.Clear();
            Console.WriteLine("JMENO: {0}\nUtok: {1}\nObrana: {2}\nMaximální zdraví: {3}\n", jmeno, utok, obrana, maxZivot);
            Console.ReadKey();
        }

        /// <summary>
        /// podle zadaneho typu statu a zadaneho poctu bodu prida dany pocet bodu k danemu statu hrace
        /// </summary>
        /// <param name="typStatu"></param>
        /// <param name="pocetBodu"></param>
        public void PridatStat(string typStatu, int pocetBodu)
        {
            switch (typStatu)
            {
                case "1":
                    utok += pocetBodu;
                    Console.WriteLine("Pridano {0} bodu do utoku, celkovy utok: {1}", pocetBodu, utok);
                    Console.ReadKey();
                    break;
                case "2":
                    obrana += pocetBodu;
                    Console.WriteLine("Pridano {0} bodu do obrany, celkova obrana: {1}", pocetBodu, obrana);
                    Console.ReadKey();
                    break;
                case "3":
                    maxZivot += pocetBodu;
                    Console.WriteLine("Pridano {0} bodu do maximalniho zdravi, celkove zdravi: {1}", pocetBodu, maxZivot);
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("neplatny vstup");
                    break;
            }
        }
        /// <summary>
        /// da hraci pocet zivotu stejny jako maximalni pocet
        /// </summary>
        public void VylecitBojovnika()
        {
            zivot = maxZivot;
        }

        /// <summary>
        /// funkce pouze pro pocitacoveho soupere, po kazdem kole mu prida nahodne/nahodne a dane pocet statu ke vsem statum, aby byl v dalsim kole tezsi na porazeni
        /// </summary>
        public void PridatStatyNPC()
        {
            Kostka kostkaNPC = new Kostka(5);
            utok += kostkaNPC.Hod();
            obrana += kostkaNPC.Hod();
            maxZivot += kostkaNPC.Hod() + 4;
        }
    }
}

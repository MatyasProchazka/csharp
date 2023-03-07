using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public Zbran Zbran { get; set; }

        private string zprava;

        public int PocetKol { get; set; }

        public int Penize { get; private set; }

        protected double cena;

        /// <summary>
        /// konstruktor pro atributy
        /// </summary>
        /// <param name="jmeno"></param>
        /// <param name="zivot"></param>
        /// <param name="utok"></param>
        /// <param name="obrana"></param>
        /// <param name="kostka"></param>
        public Bojovnik(string jmeno, int zivot, int utok, int obrana, Kostka kostka, Zbran zbran)
        {
            this.jmeno = jmeno;
            this.zivot = zivot;
            this.maxZivot = zivot;
            this.utok = utok;
            this.obrana = obrana;
            this.kostka = kostka;
            Penize = 10;
            PocetKol = 0;
            Zbran = zbran;
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
            s += string.Format("]     UTOK: {0}   OBRANA: {1}   ZDRAVI: {2}/{3}", utok, obrana, zivot, maxZivot);
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
            int uder = utok + kostka.Hod() + Zbran.Utoc();
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
            Console.WriteLine("JMENO: {0}\nUtok: {1}\nObrana: {2}\nMaximální zdraví: {3}\nPeníze: {4} zlaťáků\nAktuální zbraň: {5}", jmeno, utok, obrana, maxZivot, Penize, Zbran);
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
                    cena = (ZjistitCenuZaStat(typStatu) * pocetBodu);
                    if (Penize >= cena)
                    {
                        utok += pocetBodu;
                        Penize -= (int)cena;
                        Console.WriteLine("Pridano {0} bodu do utoku, celkovy utok: {1}", pocetBodu, utok);
                    }
                    else
                    {
                        Console.WriteLine("Nedostatek prostředků");
                    }
                    Console.ReadKey();
                    break;
                case "2":
                    cena = (ZjistitCenuZaStat(typStatu) * pocetBodu);
                    if (Penize >= cena)
                    {
                        obrana += pocetBodu;
                        Penize -= (int)cena;
                        Console.WriteLine("Pridano {0} bodu do obrany, celkova obrana: {1}", pocetBodu, obrana);
                    }
                    else
                    {
                        Console.WriteLine("Nedostatek prostředků");
                    }
                    Console.ReadKey();
                    break;
                case "3":
                    cena = (ZjistitCenuZaStat(typStatu) * pocetBodu);
                    if (Penize >= cena)
                    {
                        maxZivot += pocetBodu;
                        Penize -= (int)cena;
                        Console.WriteLine("Pridano {0} bodu do maximalniho zdravi, celkove zdravi: {1}", pocetBodu, maxZivot);
                    }
                    else
                    {
                        Console.WriteLine("Nedostatek prostředků");
                    }
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
            Kostka kostkaNPC = new Kostka(4);
            utok += (kostkaNPC.Hod() + 1);
            obrana += (kostkaNPC.Hod() + 1);
            maxZivot += (kostkaNPC.Hod() + 3);
        }

        /// <summary>
        /// pripocita zadany pocet penez k penezum hrdiny
        /// </summary>
        /// <param name="pocetPenez"></param>
        public void PridatPenize(int pocetPenez)
        {
            Penize += pocetPenez;
        }

        public void UbratPenize(int pocetPenez)
        {
            Penize -= pocetPenez;
        }

        public double ZjistitCenuZaStat(string typStatu)
        {
            switch (typStatu)
            {
                case "1":
                    cena = Math.Round((Math.Round(Math.Sqrt(utok), 0) / 2), 0);
                    return cena;
                case "2":
                    cena = Math.Round((Math.Round(Math.Sqrt(obrana), 0) / 1), 0);
                    return cena;
                case "3":
                    cena = Math.Round((Math.Round(Math.Sqrt(maxZivot), 0) / 8), 0);
                    return cena;
                default:
                    return 0;
            }
        }

        public void ZmenitZbran(Zbran novaZbran)
        {
            Zbran = novaZbran;
        }
    }
}

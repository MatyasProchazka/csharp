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
        /// <summary>
        /// vytovreni atributu zbrane bojovnika
        /// </summary>
        public Zbran Zbran { get; set; }
        /// <summary>
        /// zprava ktera se vypise 
        /// </summary>
        private string? zprava;
        /// <summary>
        /// pocet kol, ktera bojovnik vyhral
        /// </summary>
        public int PocetKol { get; set; }
        /// <summary>
        /// penize, ktere bojovnik a muze je pouziva ke koupi
        /// </summary>
        public int Penize { get; private set; }

        public int VelikostInventare { get; private set; }
        
        private List<Zbran> inventar = new List<Zbran>();

        string[] jmenaProtivniku = { "Barnabas", "Josef", "Michael", "Joe", "Kostlivec z hlubin", "Tvoje mama", "Otec", "Srdce" };

        /// <summary>
        /// konstruktor pro atributy
        /// </summary>
        /// <param name="jmeno"></param>
        /// <param name="zivot"></param>
        /// <param name="utok"></param>
        /// <param name="obrana"></param>
        /// <param name="kostka"></param>
        public Bojovnik(string jmeno, int zivot, int utok, int obrana, int velikostInventare, Kostka kostka, Zbran zbran)
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
            VelikostInventare = velikostInventare;

            for (int i = 0; i<VelikostInventare; i++)
            {
                inventar.Add(new Zbran("Prazdne", 0, 0, 0));
            }
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
            double cena;
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

        /// <summary>
        /// odecte penize od poctu penez hrdiny
        /// </summary>
        /// <param name="pocetPenez"></param>
        public void UbratPenize(int pocetPenez)
        {
            Penize -= pocetPenez;
        }

        /// <summary>
        /// vrati pozadovanou cenu za 1 bod zvyseni statu na zaklade aktualnich statu (1 = utok, 2 = obrana, 3 = zivot)
        /// </summary>
        /// <param name="typStatu"></param>
        /// <returns>cena za 1 bod zadaneho statu</returns>
        public double ZjistitCenuZaStat(string typStatu)
        {
            double cena;
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

        /// <summary>
        /// vymeni aktualne drzenou zbran za zadanou zbran
        /// </summary>
        /// <param name="novaZbran"></param>
        public void ZmenitZbran(Zbran novaZbran)
        {
            Zbran docasnaZbran = Zbran;
            int index = inventar.IndexOf(novaZbran);

            Zbran = novaZbran;
        }

        public bool DatZbranDoInventare(Zbran novaZbran)
        {
            int index = 0;
            bool udelano = false;
            foreach (Zbran zbran in inventar)
            {
                if (zbran.Jmeno == "Prazdne")
                {
                    inventar.RemoveAt(index);
                    inventar.Insert(index, novaZbran);
                    udelano = true;
                    break;
                }
                

                index++;
            }
            return udelano;

        }

        public void VypsatInventar()
        {
            Console.Clear();
            int index = 1;
            Console.WriteLine("INVENTAR\n");
            foreach(Zbran zbran in inventar)
            {
                Console.WriteLine("{0}) {1}", index, zbran.Jmeno);
                index++;
            }
        }

        public void NasaditZbran(int indexZbrane)
        {
            Zbran docasnaZbran = Zbran;
            Zbran = inventar[indexZbrane];
            inventar.RemoveAt(indexZbrane);
            inventar.Insert(indexZbrane, docasnaZbran);
        }

        public void ZobrazeniZbrane(int index)
        {
            Console.Clear();
            Zbran zbran = inventar[index];
            if (zbran.Jmeno != "Prazdne") 
            {
                Console.WriteLine("ZBRAN: {0}\nUtok: {1}\nKriticka sance: {2}\nCena: {3}", zbran.Jmeno, zbran.Utok, zbran.KrtitickaSance, zbran.Cena);
                Console.WriteLine("\nMOZNOSTI:\n1) Nasadit\n2) Prodat");
                int volba = int.Parse(Console.ReadLine());
                switch (volba)
                {
                    case 1:
                        NasaditZbran(index);
                        break;
                    case 2:
                        ProdatZbran(inventar[index]);
                        break;
                    default:
                        Console.WriteLine("chybne zadani");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Nic tu neni");
                Console.ReadKey();
            }
        }

        public void ProdatZbran(Zbran zbran)
        {
            Console.Clear();
            int prodejniCena = Convert.ToInt32(Math.Round(zbran.Cena * 0.4));
            int index = inventar.IndexOf(zbran);

            Console.WriteLine("chces prodat zbran za {0} zlataku?\n1) ANO\n2) NE", prodejniCena);
            try
            {
                int volba = int.Parse(Console.ReadLine());
                switch (volba)
                {
                    case 1:
                        inventar.RemoveAt(index);
                        inventar.Insert(index, new Zbran("Prazdne", 0, 0, 0));
                        Penize += prodejniCena;

                        Console.WriteLine("prodano za {0} zlataku", prodejniCena);
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("vracis se zpatky do menu");
                        Console.ReadKey();
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("spatne, jdes zpatky do menu");
            }
        }

        public Bojovnik VygenerovatProtivnika()
        {
            Obchod obchod = new Obchod(this, 0);
            string jmeno = jmenaProtivniku[kostka.VygenerovatCislo(0, jmenaProtivniku.Length)];
            int utok = 15 + PocetKol + 2;
            int zivot = 60 + PocetKol * 1;
            int obrana = 18 + PocetKol + 2;
            int velikostInventare = 0;
            Zbran zbran = obchod.NovaZbran();

            return new Bojovnik(jmeno, zivot, utok, obrana, velikostInventare, this.kostka, zbran);
        }

        public Bojovnik VygenerovatProtivnikaDuengoen(int poschodi)
        {
            string jmeno = jmenaProtivniku[kostka.VygenerovatCislo(0, jmenaProtivniku.Length)];
            int utok = 5 + poschodi * 3;
            int zivot = 40 + poschodi * 3;
            int obrana = 7 + poschodi * 2;
            int velikostInventare = 0;
            Zbran zbran = new Zbran("nic", 0, 0, 0);

            return new Bojovnik(jmeno, zivot, utok, obrana, velikostInventare, this.kostka, zbran);
        }

        public double PodilZivotu()
        {
            return zivot / maxZivot;
        }
    }
}

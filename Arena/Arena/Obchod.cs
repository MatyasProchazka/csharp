using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Obchod
    {
        /// <summary>
        /// vytvoreni instance bojovnika/hrace
        /// </summary>
        private Bojovnik bojovnik;
        /// <summary>
        /// vytvoreni instance kostky
        /// </summary>
        Kostka kostka = new Kostka();
        /// <summary>
        /// vlastnost ktera udava kolik predmetu bude v obchode zobrazeno
        /// </summary>
        public int VelikostObchodu { get; private set; }
        /// <summary>
        /// vytvori list ve kterem jsou ulozeny vsechny vytvorene instance zbrani momentalne nabizene v obchode
        /// </summary>
        List<Zbran> ZbraneList = new List<Zbran>();
        /// <summary>
        /// array obsahujici vsechny jmena pouzitelna pro zbran, nahodne se z nich vybira pri tvorbe nove zbrane
        /// </summary>
        String[] jmenaZbrani = { "Mjolnir", "Endbringer", "Lament", "Twilight Sculptor", "Desire's Spellblade", "Guard's Mithril Katana", "Bloodweep", "Night's Fall" };

        public Obchod(Bojovnik bojovnik, int velikostObchodu)
        {
            this.bojovnik = bojovnik;
            VelikostObchodu = velikostObchodu;

            // pri vygenerovani instance obchodu prida zbrane na zaklade zadae velikosti obchodu
            for (int i = 0; i < VelikostObchodu; i++)
            {
                PridatZbran(i);
            }
        }

        /// <summary>
        /// vytvori a prida do listu zbrani novou instanci zbrane se staty vygenerovanymi na zaklade momentalniho kola a nahodneho faktoru
        /// </summary>
        /// <param name="index"></param>
        public void PridatZbran(int index)
        {
            int utok = NahodnyUtok(bojovnik.PocetKol);
            ZbraneList.Insert(index, new Zbran(VygenerovatJmeno(), utok, NahodnyKritickaSance(bojovnik.PocetKol), VypocitatCenuZbrane(utok, bojovnik.PocetKol)));
        }

        /// <summary>
        /// zakladnni nabidka obchodu ze ktere lze vybirat
        /// </summary>
        public void VypsatNabidku()
        { 
            int idVypis = 1;
            Console.WriteLine("OBCHOD\nAktuální zůstatek: {0} zlataku", bojovnik.Penize);
            // projede listem zbrani a vypise jednotlive zbrane s jejich staty
            foreach (Zbran zbran in ZbraneList) 
            { 
                Console.WriteLine("{4}) Zbran: {0}, {1}, {2}, {3}", zbran.Jmeno, zbran.Utok, zbran.KrtitickaSance, zbran.Cena, idVypis);
                idVypis++;
            }
        }

        /// <summary>
        /// podle vybrane moznosti da hraci jeho zvolenou zbran, odebere mu penize a vymeni zbran v obchode za novou
        /// </summary>
        /// <param name="moznost"></param>
        public void KoupitZbran(int moznost)
        {
            // MUSIM PRIDAT EXCEPTION HANDELING KDYZ CISLO MIMO LIST!!!!
            if (moznost != 0 && moznost <=VelikostObchodu)
            {
                int index = moznost - 1;
                if (bojovnik.Penize >= ZbraneList[index].Cena)
                {
                    Console.WriteLine("koupena zbran: {0}!", ZbraneList[index].Jmeno);
                    //ubere hraci penize a nasadi mu jeho zbran
                    bojovnik.UbratPenize(ZbraneList[index].Cena);
                    bojovnik.ZmenitZbran(ZbraneList[index]);
                    //vymeni zbran v obchode za novou
                    ZbraneList.RemoveAt(index);
                    PridatZbran(index);
                }
                else
                {
                    Console.WriteLine("Nedostatek financí");
                }
            }
            else
            {
                Console.WriteLine("Neplatny vstup, kliknutim se vratis zpatky do menu");
            }
            
        }

        /// <summary>
        /// vygeneruje utok pro zbran na zaklade kola ve kterem bojovnik je a nahodnych parametru
        /// </summary>
        /// <param name="pocetKol"></param>
        /// <returns>utok pro zbran</returns>
        private int NahodnyUtok(int pocetKol)
        {
            return kostka.VygenerovatCislo(pocetKol + 1, 2 + (pocetKol * 2)) + bojovnik.PocetKol + kostka.VygenerovatCislo(0, 6);
        }

        /// <summary>
        /// vygeneruje kritickou  sanci pro zbran na zaklade kola ve kterem bojovnik je a nahodnych parametru, jestlize je sance vetsi nez 100, vrati pouze 100 (vice nedava smysl) 
        /// </summary>
        /// <param name="pocetKol"></param>
        /// <returns>kritickou sanci na utok pro zbran</returns>
        private int NahodnyKritickaSance(int pocetKol)
        {
            int vratitSanci = (pocetKol * 2) + kostka.VygenerovatCislo(1, 10);
            if (vratitSanci < 100)
            {
                return vratitSanci;
            }
            else
            {
                return 100;
            }

        }

        /// <summary>
        /// vrati cenu zbrane na zaklade utoku, ktery zbran ma, kole, ve kterem je bojovnik a nahodneho faktoru
        /// </summary>
        /// <param name="utokZbrane"></param>
        /// <param name="pocetKol"></param>
        /// <returns>cena zbrane</returns>
        private int VypocitatCenuZbrane(int utokZbrane, int pocetKol)
        {
            double cena = Math.Round((utokZbrane / 1.25) + pocetKol + kostka.VygenerovatCislo(pocetKol, pocetKol * 4));
            int vratitCenu = Convert.ToInt32(cena);
            return vratitCenu;
        }

        /// <summary>
        /// vybere z array jmen nahodne jmeno
        /// </summary>
        /// <returns>nahodne jmeno pro zbran</returns>
        private string VygenerovatJmeno()
        {
            return jmenaZbrani[kostka.VygenerovatCislo(1, jmenaZbrani.Length)];
        }

    }
}

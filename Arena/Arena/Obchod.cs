using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arena
{
    internal class Obchod
    {

        private Bojovnik bojovnik;

        public int VelikostObchodu { get; private set; }

        List<Zbran> ZbraneList = new List<Zbran>();

        public Obchod(Bojovnik bojovnik, int velikostObchodu)
        {
            this.bojovnik = bojovnik;
            VelikostObchodu = velikostObchodu;

            for (int i = 0; i < VelikostObchodu; i++)
            {
                PridatZbran(i, "Nevim");
            }
        }

        public void PridatZbran(int index, string jmeno)
        {
            ZbraneList.Insert(index, new Zbran(jmeno, bojovnik.PocetKol * 3 + 4, 15, (bojovnik.PocetKol * 4) + 20));
        }

        public void VypsatNabidku()
        {
            int idVypis = 1;
            Console.WriteLine("OBCHOD\nAktuální zůstatek: {0} zlataku", bojovnik.Penize);
            foreach (Zbran zbran in ZbraneList) 
            { 
                Console.WriteLine("{4}) Zbran: {0}, {1}, {2}, {3}", zbran.Jmeno, zbran.Utok, zbran.KrtitickaSance, zbran.Cena, idVypis);
                idVypis++;
            }
        }
        public void KoupitZbran(int moznost)
        {
            // MUSIM PRIDAT EXCEPTION HANDELING KDYZ CISLO MIMO LIST!!!!
            if (moznost != 0)
            {
                if (bojovnik.Penize >= ZbraneList[moznost - 1].Cena)
                {
                    bojovnik.UbratPenize(ZbraneList[moznost - 1].Cena);
                    bojovnik.ZmenitZbran(ZbraneList[moznost - 1]);
                    ZbraneList.RemoveAt(moznost - 1);
                    PridatZbran(moznost - 1, "SKEVELE");
                }
                else
                {
                    Console.WriteLine("Nedostatek financí");
                }
            }
            
        }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrvniAplikace
{
    internal class Zdravic
    {
        /// <summary>
        /// text pozdravu
        /// </summary>
        public string text;
        /// <summary>
        /// pozdraveni uzivatele textem pozdravu a jeho jmena
        /// </summary>
        /// <param name="jmeno"></param>
        /// <returns></returns>
        public string Pozdrav(string jmeno)
            {
                return String.Format("{0}, {1}", text, jmeno);
            }
    }
}

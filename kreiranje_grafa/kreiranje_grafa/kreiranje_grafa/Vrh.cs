using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kreiranje_grafa
{
    public class Vrh
    {
        public string naziv;
        public char ulazIzlaz = '0'; //ulaz:1, izlaz:2, ostalo:0
        public int red;
        public int stupac;

        public bool desno = false;
        public bool gore = false;
        public bool lijevo = false;
        public bool dolje = false;

        public bool nedovrseniVrh = false;
    }
}

using Soneta.Business;
using Soneta.Towary;
using Soneta.Types;
using SonetaProjekt.Extender1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Worker(typeof(LiczenieLiterWNazwieeWorker), typeof(Towar))]

namespace SonetaProjekt.Extender1
{
    public class LiczenieLiterWNazwieeWorker
    {
        Date dt = Date.Today;

        [Context]
        public Towar Towar { get; set; }
        private int _iloscLiterWNazwie;

        public int IloscLiterWNazwie
        {
            get
            {
               return  LiczIloscLiterWNazwie();
               
            }
            set
            {
                _iloscLiterWNazwie = value;
            }
        }
        private int LiczIloscLiterWNazwie()
        {
            return Towar.Nazwa.Length;

        }

    }
}

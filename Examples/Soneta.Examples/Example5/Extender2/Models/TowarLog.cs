using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soneta.Examples.Example5.Extender2
{
    public class TowarLog
    {
        public TowarLog()
        {

            ListaWpisow = new List<WpisyTabeliTowar>();
        }
       
        public List<WpisyTabeliTowar> ListaWpisow { get; private set; }
        public void DodajWpis(Powodzenie powodzenie,string wiadomosc,string opis)
        {
            var wpis = new WpisyTabeliTowar();
            wpis.Id =Guid.NewGuid();
            wpis.Powodzenie = powodzenie;
            wpis.Wiadomosc = wiadomosc;
            wpis.Opis = opis;

            ListaWpisow.Add(wpis);


        }
    }
   

}

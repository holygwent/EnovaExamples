using Soneta.Business;
using Soneta.Examples.Example5.Extender2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Worker(typeof(WyswietlenieWpisowWLogach))]


namespace Soneta.Examples.Example5.Extender2
{
    public class WyswietlenieWpisowWLogach
    {
        public WyswietlenieWpisowWLogach(TowarLog log)
        {
  
            ListaWpisow = log.ListaWpisow;
            _orginalnaLista = log.ListaWpisow;
        }

        private Powodzenie _status;
        private readonly List<WpisyTabeliTowar> _orginalnaLista;
        public Powodzenie Status
        {
            get { return _status; }
            set { 
                _status = value;
                if (value == Powodzenie.Wszystko)
                {
                    ListaWpisow = _orginalnaLista;
                }
                else
                ListaWpisow = _orginalnaLista.Where(x => x.Powodzenie == value).ToList();
            }
        }

        public List<WpisyTabeliTowar> ListaWpisow { get; set; }
        


    }
}

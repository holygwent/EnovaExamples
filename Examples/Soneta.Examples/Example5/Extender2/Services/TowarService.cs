using Soneta.Towary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soneta.Examples.Example5.Extender2
{
    public interface ITowarService
    {
        void DodajTowar(string[] wartosci, TowaryModule towaryModule);
    }
    public  class TowarService: ITowarService
    {
        private readonly IWeryfikacjaDanychTowarService _weryfikacjaDanychTowarService; 
        public TowarService()
        {
            _weryfikacjaDanychTowarService = new WeryfikacjaDanychTowarService();
        }
       public void DodajTowar(string[] wartosci,TowaryModule towaryModule)
        {
            
            _weryfikacjaDanychTowarService.WeryfikujWprowadzaneDane(wartosci, towaryModule);

            Towar towar = new Towar();

            towaryModule.Towary.AddRow(towar);

            towar.Kod = wartosci[0];
            towar.Nazwa = wartosci[1];
            towar.EAN = wartosci[2];

          
        }
    }
}

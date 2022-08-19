using Soneta.Business;
using Soneta.Towary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonetaProjekt.Extender2.Serwisy
{
    public interface IPobierzDane
    {
        string PobierzKodyKreskowe(Towar towar);
        string PobierzDefinicjeCen( Towar towar, DefinicjaCeny configDef);
    }
    public  class PobierzDane: IPobierzDane
    {
        public string PobierzKodyKreskowe(Towar towar)
        {
           // var rowTowar = TowaryModule.GetInstance(sesja).Towary.WgKodu[towar.Kod];

            var kodyKreskowe = towar.KodyKreskowe;
            string kody = "";
            foreach (var kod in kodyKreskowe)
            {
                kody += "," + kod.Kod;
            }
            if (kody !="")
            {
                kody = kody.Substring(1);
            }
           
            return  kody;
           

           
        }
        public string PobierzDefinicjeCen( Towar towar, DefinicjaCeny configDef)
        {
          //  var listaCen = TowaryModule.GetInstance(sesja).Ceny.WgTowar[towar].CreateView().ToList<Cena>();
            var wartoscCeny = towar.Ceny.Pobierz(configDef).Netto.Value.ToString();
           
           // var cena = listaCen.Where(x => x.Definicja == configDef).Single();
            return wartoscCeny;
        }

    }
}

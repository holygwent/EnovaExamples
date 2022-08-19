using Soneta.Business;
using Soneta.Config;
using Soneta.Towary;
using SonetaProjekt.Extender2;
using SonetaProjekt.Extender2.Serwisy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly:Worker(typeof(PokazTabeleZKonfiguracjiWorker),typeof(Towar))]
namespace SonetaProjekt.Extender2
{
    public  class PokazTabeleZKonfiguracjiWorker
    {
        public PokazTabeleZKonfiguracjiWorker()
        {
            _pobierzDaneSerwis = new PobierzDane();
        }

        private readonly IPobierzDane _pobierzDaneSerwis;
        [Context]
        public Session Session { get; set; }
        [Context]
        public Towar Towar { get; set; }

        public string Kolumna { get {var dane = ZawartoscKolumny(); return dane;   } private set { } }

        public string ZawartoscKolumny()
        {



            var configValue = GetValue("TypKolumny", "");


            if (configValue == "Kod")
            {

               return Towar.Kod ;
            }
            else if (configValue == "KodyKreskowe")
            {


                return _pobierzDaneSerwis.PobierzKodyKreskowe(Towar);
            }
            else if (configValue == "DefinicjeCen")
            {
                var configDefinicjaCeny = GetValue("TypDefinicjiCeny", new DefinicjaCeny());
              return _pobierzDaneSerwis.PobierzDefinicjeCen(Towar, configDefinicjaCeny);

            }
            else
            {
                return  "Błąd w opcjach lub brak opcji" ;
            }
        }


        private T GetValue<T>(string name, T def)
        {
            return GetValue(Session, name, def);
        }

        public static T GetValue<T>(Session session, string name, T def)
        {
            var cfgManager = new CfgManager(session);

            var node1 = cfgManager.Root.FindSubNode("SonetaProjekt", false);

            //Jeśli nie znaleziono gałęzi, zwracamy wartosć domyślną
            if (node1 == null) return def;

            var node2 = node1.FindSubNode("Ustawienia Tabeli", false);
            if (node2 == null) return def;

            var attr = node2.FindAttribute(name, false);
            if (attr == null) return def;

            if (attr.Value == null) return def;
            if (attr.Value.GetType() == typeof(Guid))
            {

                try
                {
                    var definicjaCeny = TowaryModule.GetInstance(session).DefinicjeCen[(Guid)attr.Value];
                    object toReturn = definicjaCeny;
                    return (T)toReturn;
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            return (T)attr.Value;
        }

    }
}

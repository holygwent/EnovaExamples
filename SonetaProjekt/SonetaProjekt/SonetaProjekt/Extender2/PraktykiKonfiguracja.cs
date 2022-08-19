using Soneta.Business;
using Soneta.Config;
using Soneta.Handel;
using Soneta.Towary;
using SonetaProjekt.Extender2;
using SonetaProjekt.Extender2.Serwisy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


[assembly: Worker(typeof(PraktykiKonfiguracja))]


//zwroc worker z listą i zrob combo box z 2 enumami
namespace SonetaProjekt.Extender2
{
    public class PraktykiKonfiguracja : ContextBase
    {
        public PraktykiKonfiguracja(Context c) : base(c)
        {
        }

        [Context]
        public Session Session { get; set; }
        
      
        public string Typ
        {
            get
            {
                return GetValue("TypKolumny", "");
            }
            set
            {
               
                    SetValue("TypKolumny", value, AttributeType._string);

                OnChanged();
            }
        }
       // private DefinicjaCeny _definicjaCeny;
        public DefinicjaCeny DefinicjaCeny
        {
            get
            {
                return  GetValue("TypDefinicjiCeny", new DefinicjaCeny());
                
               
            }
            set
            {
                SetValue("TypDefinicjiCeny", value, AttributeType._guid);
                OnChanged();
            }
        }
        public bool VisibleForDefinicjaCeny
        {
            get
            {
                if (Typ == "DefinicjeCen")
                {
                    return true;
                }
                else return false;
            }

        }



       



        //Metoda odpowiada za ustawianie wartosci parametrów konfiguracji
        private void SetValue<T>(string name, T value, AttributeType type)
        {
            SetValue(Session, name, value, type);
        }

        //Metoda odpowiada za pobieranie wartosci parametrów konfiguracji
        private T GetValue<T>(string name, T def)
        {
            return GetValue(Session, name, def);
        }

        //Metoda odpowiada za ustawianie wartosci parametrów konfiguracji
        public static void SetValue<T>(Session session, string name, T value, AttributeType type)
        {
            using (var t = session.Logout(true))
            {
                var cfgManager = new CfgManager(session);
                //wyszukiwanie gałęzi głównej 
                var node1 = cfgManager.Root.FindSubNode("SonetaProjekt", false) ??
                            cfgManager.Root.AddNode("SonetaProjekt", CfgNodeType.Node);

                //wyszukiwanie liścia 
                var node2 = node1.FindSubNode("Ustawienia Tabeli", false) ??
                            node1.AddNode("Ustawienia Tabeli", CfgNodeType.Leaf);

                //wyszukiwanie wartosci atrybutu w liściu 
                var attr = node2.FindAttribute(name, false);
                if (attr == null)
                    node2.AddAttribute(name, type, value);
                else
                    attr.Value = value;

                t.CommitUI();
            }
        }

        //Metoda odpowiada za pobieranie wartosci parametrów konfiguracji
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

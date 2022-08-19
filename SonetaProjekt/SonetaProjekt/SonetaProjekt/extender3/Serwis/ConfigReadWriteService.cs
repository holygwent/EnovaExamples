using Soneta.Business;
using Soneta.Config;
using Soneta.Handel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SonetaProjekt.extender3.Serwis
{
    public interface IConfigReadWriteService
    {
        T GetValue<T>(Session sesja, string name, T def);
        void SetValue<T>(Session session, string name, T value, AttributeType type);
    }
    public class ConfigReadWriteService: IConfigReadWriteService
    {
      //  public string Cecha { get; set; }

        //Metoda odpowiada za pobieranie wartosci parametrów konfiguracji
        public T GetValue<T>(Session sesja, string name, T def)
        {
            return GetValueFromConfig(sesja, name, def);
        }


        //Metoda odpowiada za pobieranie wartosci parametrów konfiguracji
        private  static  T GetValueFromConfig<T>(Session session, string name, T def)
        {
            var cfgManager = new CfgManager(session);

            var node1 = cfgManager.Root.FindSubNode("SonetaProjekt", false);

            //Jeśli nie znaleziono gałęzi, zwracamy wartosć domyślną
            if (node1 == null) return def;

            var node2 = node1.FindSubNode("Eksportowanie DokHand", false);
            if (node2 == null) return def;

            var attr = node2.FindAttribute(name, false);
            if (attr == null) return def;

            if (attr.Value == null) return def;

            return (T)attr.Value;
        }


        public void SetValue<T>( Session session ,string name, T value, AttributeType type)
        {
            SetValueFunc( session, name, value, type);
        }
      

     

        //Metoda odpowiada za ustawianie wartosci parametrów konfiguracji
        private static void SetValueFunc<T>(Session session, string name, T value, AttributeType type)
        {
            using (var t = session.Logout(true))
            {
                var cfgManager = new CfgManager(session);
                //wyszukiwanie gałęzi głównej 
                var node1 = cfgManager.Root.FindSubNode("SonetaProjekt", false) ??
                            cfgManager.Root.AddNode("SonetaProjekt", CfgNodeType.Node);

                //wyszukiwanie liścia 
                var node2 = node1.FindSubNode("Eksportowanie DokHand", false) ??
                            node1.AddNode("Eksportowanie DokHand", CfgNodeType.Leaf);

                //wyszukiwanie wartosci atrybutu w liściu 
                var attr = node2.FindAttribute(name, false);
                if (attr == null)
                    node2.AddAttribute(name, type,  value);//czemu nie dziala samo przeslanie value?
                else
                    attr.Value = value;

                t.CommitUI();
            }
        }

    }
}

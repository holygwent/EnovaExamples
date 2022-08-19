using Soneta.Business;
using Soneta.Examples.Example5.Extender3;
using Soneta.Towary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Worker(typeof(GrupowanieKontrahentowDlaDokHandlowychWorker), typeof(Towar))]
namespace Soneta.Examples.Example5.Extender3
{
    public class GrupowanieKontrahentowDlaDokHandlowychWorker:ContextBase
    {
        public GrupowanieKontrahentowDlaDokHandlowychWorker(Context c):base(c)
        {

        }
        [Context]
         public Session Session { get; set; }

        [Context]
        public Context Context { get; set; }

        [Context]
        public Towar Towar { get; set; }

        private int _dane;

        public int Dane
        {
            get { return GrupujKontrahentowWedlugDokHandlowych(); }
           private set { _dane = value; }
        }

        

        public int  GrupujKontrahentowWedlugDokHandlowych()
        {


            var PozycjeDokumentowWedlugTowaru = Handel.HandelModule.GetInstance(Session).PozycjeDokHan.WgTowar[Towar];//[towarZCOntextu.First()].CreateView();
          
           HashSet<int> ListaKotrahentID = new HashSet<int>();
            List<Handel.DokumentHandlowy> dokumenty = new List<Handel.DokumentHandlowy>();
            //zliczanie ile kontrahentow kupiło towar 
            foreach (var record in PozycjeDokumentowWedlugTowaru)
            {
                
                var id = record.Dokument.Kontrahent?.ID;
                
                if (id!=null)
                {
                    ListaKotrahentID.Add((int)id); 
                } ;



            }
            return ListaKotrahentID.Count;//zliczanie ile kontrahentow kupiło towar 

            //liczenie ilosci dokumentow towaru dla kazdego kontrahenta ktory go kupił

            //foreach (var KontrahentID in ListaKotrahentID)
            //{

              

            //    foreach (var record in PozycjeDokumentowWedlugTowaru)
            //    {
            //        if (record.Dokument.Kontrahent?.ID != null)
            //        {
            //            var kontrahendId = record.Dokument.Kontrahent.ID;
            //            if (KontrahentID == kontrahendId)
            //            {
            //                dokumenty.Add(record.Dokument);
            //            }
            //        }
            //    }
            //}

            //return dokumenty.Count;//ilosc wszystkich dokumentow dla każdego towaru 
          




           
        }


    }
}

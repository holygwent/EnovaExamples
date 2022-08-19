using Soneta.Business;
using Soneta.CRM;
using Soneta.Handel;
using Soneta.Kasa.Config;
using SonetaProjekt.NewFolder;
using System;
using Soneta.Types;

using System.Linq;
using Soneta.Towary;
using Soneta.Magazyny;
using static Soneta.Handel.DokumentHandlowy;

[assembly:Worker(typeof(KontrahenciDuo),typeof(Kontrahent))]
namespace SonetaProjekt.NewFolder
{
    public class KontrahenciDuo
    {
        [Context]
        public Kontrahent kontrahenci { get; set; }
        [Context]
        public Session Sesja { get; set; }

        [Action("Soneta Examples/dd", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public void MojaFunkcjaTestowa4()
        {
            
            //using (var newSesja = Sesja.Login.CreateSession(false, false)) //tryb edycji potrzebny zeby dodawac i edytowac
            //{
            //    using (var t = newSesja.Logout(true)) //tryb edycji potrzebny zeby dodawac i edytowac
            //    {

            //        var definicja = HandelModule.GetInstance(newSesja).DefDokHandlowych.CreateView();
            //        definicja.Condition = new FieldCondition.Equal("Symbol", "ZO");

            //        var hm = HandelModule.GetInstance(newSesja);
            //        var towar = TowaryModule.GetInstance(newSesja).Towary.WgKodu["BUT_NAR_49012345678910111213141516171819202122232425262728293031323334353637383940414243444546"] as Towar;



            //        var nowyDokument = new DokumentHandlowy();

            //        hm.DokHandlowe.AddRow(nowyDokument);
            //        nowyDokument.Data = Date.Now;
            //        try
            //        {
            //            nowyDokument.Definicja = definicja.ToArray<DefDokHandlowego>().Single();
            //        }
            //        catch (Exception)
            //        {

            //            throw new Exception("czemu jest wiecej symboli ZO w bazie danych?");
            //        }
            //        nowyDokument.Magazyn = nowyDokument.GetListMagazyn().ToList<Magazyn>().First();
            //        nowyDokument.Kontrahent = kontrahenci;

            //        var cos = new PozycjaDokHandlowego(nowyDokument);
            //        hm.PozycjeDokHan.AddRow(cos);
            //        cos.Towar = towar;
            //        cos.CenaPoRabacie = new DoubleCy(4.06);







            //        //stworz pozycje 





            //        t.Commit();
            //    }
            //    newSesja.Save();
            //}

          
            

        
        }

    }
   
}

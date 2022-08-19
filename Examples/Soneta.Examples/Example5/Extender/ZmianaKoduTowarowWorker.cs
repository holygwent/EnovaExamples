using Soneta.Business;
using Soneta.Examples.Example5.Extender;
using Soneta.Towary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Soneta.Types;
using System.Threading.Tasks;
using Soneta.Business.UI;
using System.Windows.Forms;
using Soneta.Handel;

[assembly: Worker(typeof(ZmianaKoduTowarowWorker), typeof(Towar))]
namespace Soneta.Examples.Example5.Extender
{
    public class ZmianaKoduTowarowWorker
    {
        
        Date dt = Date.Today;

        [Context]
        public ZmianaKoduTowarowParams @params { get; set; } //pobiera parametry ponieważ ma context



        [Context]
        public Towar Towar { get; set; } //pobiera pojedynczy towar ponieważ ma context

        [Action("Soneta Examples/Zmiana kodu", Priority = 1, Icon = ActionIcon.Wizard, Description = "Zmiana kodu na nowy", CommandShortcut = Commands.CommandShortcut.X, Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public object ZmianaKodu()
        {
            while (@params.NowyKod.EndsWith(" "))
            {
              @params.NowyKod =   @params.NowyKod.Substring(0, (@params.NowyKod.Length - 1));
            }
            using (var t = @params.Session.Logout(true)) //tryb edycji potrzebny zeby dodawac i edytowac
            {
                var tm = TowaryModule.GetInstance(@params.Session);
                var view = tm.Towary.CreateView();//tworzenie widoku zawierajacy wszystkie towary
                view.Condition &= new FieldCondition.Equal("Kod", @params.NowyKod); //filtrowanie tabeli po conditions(warunkach)

                if (!t.Session.ReadOnly)
                {
                    if (@params.NowyKod != ""
                         & @params.NowyKod != null
                         & Towar.Kod != @params.NowyKod & view.Count ==0) 
                    {
                        try
                        {

                            Towar.Kod = @params.NowyKod;
                        }
                        catch (Exception)
                        {

                            MessageBoxToReturn("Nie udało sie podmienić danych", "Błąd");
                        }
                       
                    }
                    else
                    {
                        string error = "Nastąpił błąd przy zmianie kodu!\n Możliwe: \nKod był taki sam jak stary\nKod sie  dublował z innym w tabeli\nKod był pusty";
                        string title = "Błąd";
                        
                         return  MessageBoxToReturn(error,title);

                    }


                }
                try
                {
                    t.Commit();//zapis danych
                }
                catch (Exception)
                {

                    MessageBoxToReturn("Nie udało sie podmienić danych", "Błąd");

                }
               
               
                return MessageBoxToReturn("Zmieniono dane","Kommunikat");
            }


        }
       
   
        public MessageBoxInformation MessageBoxToReturn(string text,string title)
        {
            return new MessageBoxInformation(title)
            {
                Text =text,
               
            };
        }

        
        
    }

    public class ZmianaKoduTowarowParams : ContextBase
    {

        public ZmianaKoduTowarowParams(Context context) : base(context)
        {

            Soneta.Towary.Towar[] towary;
            var towaryNieKonwertowane = context["Towar[]"];

            try
            {
                towary = (Soneta.Towary.Towar[])towaryNieKonwertowane;
            }
            catch (Exception e)
            {

                throw e;
            }
            
            string pierwszyKod = towary.SingleOrDefault().Kod;
            if (pierwszyKod != null)
            {
                
                    OrginalnyKod = pierwszyKod;
             
            }
            else
                throw new NullReferenceException();


        }

        public string OrginalnyKod { get; set; }
        public string NowyKod { get; set; }

    }

}

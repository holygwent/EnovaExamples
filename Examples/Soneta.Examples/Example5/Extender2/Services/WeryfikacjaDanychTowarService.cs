using Soneta.Business;
using Soneta.Towary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soneta.Examples.Example5.Extender2
{
    public interface IWeryfikacjaDanychTowarService
    {
         void WeryfikujWprowadzaneDane(string[] wartosci, TowaryModule tm);
    }
    public class WeryfikacjaDanychTowarService: IWeryfikacjaDanychTowarService
    {
       
       public void  WeryfikujWprowadzaneDane(string[] wartosci, TowaryModule tm)//rozbij na funkcje  dla kazdej wartosci
        {
            var view = tm.Towary.CreateView();
            view.Condition = new FieldCondition.Equal("Kod", wartosci[0]);

            if (wartosci[0] == "" | wartosci[0] == null)
            {
                throw new ArgumentNullException("Kod nie może być pusty");
            }
            if (wartosci[0].StartsWith(" "))
            {
                throw new Exception("Kod nie może zaczynać sie od spacji");
            }
            if (view.Count >0)
            {
                throw new InvalidOperationException("istnieje wiersz o takim kodzie");
            }
            if (wartosci[1] == "" | wartosci[1] == null)
            {
                throw new ArgumentNullException("Nazwa nie może być pusta");
            }
            if (wartosci[1].ElementAt(0) == wartosci[1].ToLower().ElementAt(0))
            {
                char pierwszaDuzaLitera = wartosci[1].ToUpper().ElementAt(0);
                wartosci[1] = pierwszaDuzaLitera + wartosci[1].Substring(1);
            }
            if (wartosci[1].StartsWith(" "))
            {
                throw new Exception("Nazwa nie może zaczynać sie od spacji");
            }
            

        }
    }
}

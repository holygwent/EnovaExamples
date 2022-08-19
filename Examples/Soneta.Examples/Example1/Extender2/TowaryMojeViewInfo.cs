using Soneta.Business;
using Soneta.Business.UI;
using Soneta.CRM;
using Soneta.Examples.Example1.Extender;
using Soneta.Examples.Example3.Extender;
using Soneta.Towary;

[assembly: FolderView("Soneta.Examples/Towary Moje",
    Priority = 999,
    Description = "Towary Moje zadanie",
    TableName = "Towary",
    ViewType = typeof(TowaryMojeViewInfo)
)]

namespace Soneta.Examples.Example1.Extender
{

    public class TowaryMojeViewInfo : ViewInfo
    {

        //filtrowanie po nazwie i kodzie dla towarow stworzyć ViewInfo
        //nie zopomnij o innego roczaju assembly 



        public TowaryMojeViewInfo()
        {

            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "TowaryMoje";
            //Inicjowanie contextu 
            InitContext += TowaryMojeViewInfo_InitContext;
            //tworzenie view zawierajacego konkretne dane
            CreateView += TowaryMojeViewInfo_CreateView;


        }

        void TowaryMojeViewInfo_InitContext(object sender, ContextEventArgs args)
        {
            args.Context.TryAdd(() => new FiltrParams(args.Context));
        }


        void TowaryMojeViewInfo_CreateView(object sender, CreateViewEventArgs args)
        {
            FiltrParams parameters;//filtry
            if (!args.Context.Get(out parameters))
                return;
            args.View = ViewCreate(parameters);
            args.View.AllowNew = false;
        }

        protected View ViewCreate(FiltrParams pars)
        {


            var tm = TowaryModule.GetInstance(pars.Context.Session);
            var view = tm.Towary.CreateView();
            if (pars.Fraza != "" & pars.Fraza != null)
            {
                view.Condition = new FieldCondition.Like("Kod", $"%{pars.Fraza}%");
                view.Condition |= new FieldCondition.Like("Nazwa", $"%{pars.Fraza}%");
            }



            return view;
        }


    }

    public class FiltrParams : ContextBase //filtr
    {


        public FiltrParams(Context context) : base(context)
        {
        }
        private string _fraza;
        public string Fraza
        {
            get
            {
                return _fraza;
            }
            set {
                _fraza = value;
                OnChanged();
            
            }
        }




    }

}

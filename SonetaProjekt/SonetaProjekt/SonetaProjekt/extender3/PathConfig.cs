using Soneta.Business;
using Soneta.Config;
using Soneta.Handel;
using SonetaProjekt.extender3;
using SonetaProjekt.extender3.Serwis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Worker(typeof(PathConfig))]
namespace SonetaProjekt.extender3
{
    public class PathConfig : ContextBase
    {
        private readonly IConfigReadWriteService _configReadWriteService;

        public PathConfig(Context c) : base(c)
        {
            _configReadWriteService = new ConfigReadWriteService();

        }

        [Context]
        public Session Session { get; set; }

        public string Path
        {
            get
            {
                return _configReadWriteService.GetValue(Session, "Path", "");
            }
            set
            {
                if (value is null)
                {
                    value = "";
                }
                _configReadWriteService.SetValue(Session, "Path", value, AttributeType._string);

                OnChanged();
            }
        }
       
        public string WybCechyDokumentu
        {
            get
            {
                return _configReadWriteService.GetValue(Session, "Cecha", "");

            }
            set
            {
                _configReadWriteService.SetValue(Session, "Cecha", value, AttributeType._string);

            }
        }

        public object GetListWybCechyDokumentu()
        {
            var Cechy = HandelModule.GetInstance(Session).DokHandlowe.FeatureDefinitions.CreateView();
            Cechy.Condition = new FieldCondition.Equal("TypeNumber", FeatureTypeNumber.Bool);
            var CechyOkreslonyTyp = Cechy.ToArray<FeatureDefinition>();
            return CechyOkreslonyTyp.Select(x => x.Name).ToArray<string>();

        }








    }
}

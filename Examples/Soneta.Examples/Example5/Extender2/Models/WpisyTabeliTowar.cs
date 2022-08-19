using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Soneta.Examples.Example5.Extender2
{
    public enum Powodzenie
    {
        Wszystko = 0,
        Sukces =1,
        Error=2,
        kodIstnieje=3,
        pustePole=4,
        

    }
    public class WpisyTabeliTowar
    {
        public Guid Id { get; set; }
        public Powodzenie Powodzenie { get; set; }
        public string Wiadomosc { get; set; }
        public string Opis { get; set; }
    }
}

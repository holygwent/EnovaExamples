using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Soneta.Examples.Example5.Extender2
{
    public interface ICsvDocumentService
    {
        FileDto CzytajDokument(string sciezka);
    }
    public class CsvDocumentService : ICsvDocumentService
    {
        public FileDto CzytajDokument(string sciezka)
        {
            List<string> tekstPliku = new List<string>();
            
         
               
                if (sciezka.EndsWith(".csv") == true)
                {
                    using (StreamReader sr = File.OpenText(sciezka))
                    {
                        while (!sr.EndOfStream)
                        {
                            tekstPliku.Add(sr.ReadLine());
                            
                        }
                    }

                }
                else
                     tekstPliku = null;
                
               
                    
            
            
            return new FileDto { Tekst = tekstPliku };
        }
    }
}

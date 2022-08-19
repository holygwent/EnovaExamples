using Soneta.Business;
using Soneta.Config;
using Soneta.Handel;
using SonetaProjekt.extender3;
using SonetaProjekt.extender3.Serwis;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: Worker(typeof(ExportDokumentowWorker), typeof(DokHandlowe))]
namespace SonetaProjekt.extender3
{
    public class ExportDokumentowWorker
    {
        private readonly IConfigReadWriteService _configReadWriteService;
        public ExportDokumentowWorker()
        {
            _configReadWriteService = new ConfigReadWriteService();
        }
        [Context]
        public Session Session { get; set; }

        [Context]
        public DokumentHandlowy[] DokHandlowe { get; set; }

        [Action("Soneta Examples/export dokumentow do wyslania emailem", Mode = ActionMode.SingleSession | ActionMode.Progress)]
        public void ExportDokumentow()
        {
            string path = _configReadWriteService.GetValue(Session, "Path", "");
            if (path == "")
            {
                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Nie ma w ustawieniach config podanej sciezki", EventLogEntryType.Error);
                }
                throw new Exception("Wpisz sciezke do config");
            }

           

            foreach (var dok in DokHandlowe)
            {
                var tekst = new StringBuilder();
                bool cecha;

                try
                {
                     cecha = dok.Features.GetBool(_configReadWriteService.GetValue(Session, "Cecha", ""));

                }
                catch (Exception)
                {

                    throw new Exception("problem z wartoscia cechy");
                }


                if (cecha == false)
                {
                    if (File.Exists(path + "\\" + dok.Numer.ToString().Replace('/', '_') + ".txt"))
                    {
                        throw new Exception("Exported file exists");
                    }
                    using (StreamWriter sw = File.CreateText(path+"\\"+dok.Numer.ToString().Replace('/','_')+ ".txt"))
                    {
                        var kontrahent = dok.Kontrahent;
                        tekst.Append($"Email:{kontrahent.EMAIL}\n");//email kontrahenta
                        tekst.Append($"Kontrahent: {kontrahent.Nazwa}\n");//nazwa kontrahenta
                        tekst.Append($"Dokument: {dok.Numer.ToString()}\nData:{dok.Data.ToString()}\n");//numer dokumentu oraz data dokumentu
                        foreach (var pozycja in dok.Pozycje.ToArray<PozycjaDokHandlowego>())
                        {
                            tekst.Append($"{pozycja.PełnaNazwa},{pozycja.Ilosc.ToString()},Cena:{pozycja.Cena.ToString()},Całkowita wartość:{pozycja.WartoscCy.ToString()}\n");//pozycja oddzielona przecinkiem
                        }
                     
                        sw.WriteLine(tekst.ToString());//zapisuje do pliku

                        using(var t = Session.Logout(true))
                        {
                            var cos = _configReadWriteService.GetValue(Session, "Cecha", "");
                          dok.Features[cos] = true;
                           
                            t.Commit();
                        }
                       
                    }
                }
                else
                {
                    using (EventLog eventLog = new EventLog("Application"))
                    {
                        eventLog.Source = "Application";
                        eventLog.WriteEntry("Cecha ustawiona na true czyli byl dokument ekspandowany,nazwa:" + dok.Numer, EventLogEntryType.Error);
                    }
                }

            }

        }







    }
}

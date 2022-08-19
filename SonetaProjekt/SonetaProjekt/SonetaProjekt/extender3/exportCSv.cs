//using Soneta.Business;
//using Soneta.Config;
//using Soneta.Handel;
//using SonetaProjekt.extender3;
//using SonetaProjekt.extender3.Serwis;
//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//[assembly: Worker(typeof(ExportDokumentowWorker), typeof(DokHandlowe))]
//namespace SonetaProjekt.extender3
//{
//    public class ExportDokumentowWorker
//    {
//        private readonly IConfigReadService _configReadWriteService;
//        public ExportDokumentowWorker()
//        {
//            _configReadWriteService = new ConfigReadWriteService();
//        }
//        [Context]
//        public Session Session { get; set; }

//        [Context]
//        public DokumentHandlowy[] DokHandlowe { get; set; }

//        [Action("Soneta Examples/export dokumentow do wyslania emailem", Mode = ActionMode.SingleSession | ActionMode.Progress)]
//        public void ExportDokumentow()
//        {
//            string path = _configReadWriteService.GetValue(Session, "Path", "");
//            if (path == "")
//            {
//                using (EventLog eventLog = new EventLog("Application"))
//                {
//                    eventLog.Source = "Application";
//                    eventLog.WriteEntry("Nie ma w ustawieniach config podanej sciezki", EventLogEntryType.Error);
//                }
//                throw new Exception("Wpisz sciezke do config");
//            }



//            foreach (var dok in DokHandlowe)
//            {
//                var tekst = new StringBuilder();
//                var cecha = _configReadWriteService.GetValue(Session, dok.Numer.ToString() + "CechaEksportowania", false);
//                if (cecha == false)
//                {
//                    using (StreamWriter sw = File.CreateText(path + dok.Numer.ToString() + ".csv"))
//                    {
//                        var kontrahent = dok.Kontrahent;
//                        tekst.Append($"{kontrahent.Nazwa};");//nazwa kontrahenta
//                        tekst.Append($"{kontrahent.EMAIL};");//email kontrahenta
//                        tekst.Append($"{dok.Numer.ToString()};{dok.Data.ToString()};");//numer dokumentu oraz data dokumentu
//                        foreach (var pozycja in dok.Pozycje.ToArray<PozycjaDokHandlowego>())
//                        {
//                            tekst.Append($"{pozycja.PełnaNazwa},{pozycja.Ilosc.ToString()},Cena:{pozycja.Cena.ToString()},Suma:{pozycja.WartoscCy.ToString()}|");//pozycja oddzielona przecinkiem
//                        }
//                        tekst.Remove(tekst.Length - 1, 1);
//                        tekst.Append(";");
//                        sw.WriteLine(tekst.ToString());//zapisuje do pliku
//                        _configReadWriteService.SetValue(Session, dok.Numer.ToString() + "CechaEksportowania", true, AttributeType._boolean);

//                    }
//                }
//                else
//                {
//                    using (EventLog eventLog = new EventLog("Application"))
//                    {
//                        eventLog.Source = "Application";
//                        eventLog.WriteEntry("Cecha ustawiona na true czyli byl dokument ekspandowany,nazwa:" + dok.Numer, EventLogEntryType.Error);
//                    }
//                }

//            }

//        }







//    }
//}

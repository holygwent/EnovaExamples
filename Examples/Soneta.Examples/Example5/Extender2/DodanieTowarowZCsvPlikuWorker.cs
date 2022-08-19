using Soneta.Business;
using Soneta.Business.UI;
using Soneta.Examples.Example5.Extender2;
using Soneta.Tools;
using Soneta.Towary;
using Soneta.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

[assembly: Worker(typeof(DodanieTowarowZCsvPlikuWorker), typeof(Towary))]


namespace Soneta.Examples.Example5.Extender2
{
    public class DodanieTowarowZCsvPlikuWorker //: ContextBase
    {

        public DodanieTowarowZCsvPlikuWorker()//(Context context) : base(context)
        {
            _towarService = new TowarService();
            _documentService = new CsvDocumentService();
            File = new FileDto();
        }
        private readonly ITowarService _towarService;
        private readonly ICsvDocumentService _documentService;

        [Context]
        public DodanieTowarowZCsvPlikuParams @params { get; set; }


        public FileDto File { get; set; }

        public TowarLog Log { get; set; }

        [Action("Soneta Examples/Dodanie Danych z CSV", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress, Icon = ActionIcon.Wizard)]
        public object DodanieTowarowZCsv()
        {
            int row = 0;
            File = _documentService.CzytajDokument(@params.PlikSciezka);

            TowarLog log = new TowarLog();

            List<string> wiersze = File.Tekst;
            if (wiersze != null)
            {

                if (@params.CzyIstniejeNaglowek)
                {
                    //usuwanie naglowka
                    var BezNaglowka = wiersze.Skip(1);
                    wiersze = BezNaglowka.ToList();

                }

                var tm = TowaryModule.GetInstance(@params.Session);
                using (var t = @params.Session.Logout(true))
                {

                    foreach (var wiersz in wiersze)
                    {
                        string[] wartosci = wiersz.Split(';');
                        var liniaWPliku = @params.CzyIstniejeNaglowek ? row + 2 : row + 1;

                        try
                        {
                            _towarService.DodajTowar(wartosci, tm);
                            log.DodajWpis(Powodzenie.Sukces, "dodano plik", $"dodano plik o nazwie:{wartosci[1]}");

                        }
                        catch (InvalidOperationException e)
                        {
                            log.DodajWpis(Powodzenie.kodIstnieje, e.Message,
                                  $"taki kod istnieje dla wiersza o kodzie:{wartosci[0]}, linia w pliku:{liniaWPliku.ToString()}");
                        }
                        catch (ArgumentNullException e)
                        {
                            log.DodajWpis(Powodzenie.pustePole, e.Message,
                                   $"puste potrzebne kod lub nazwa dla wiersza w lini pliku:{liniaWPliku.ToString()}");
                        }
                        catch (Exception e)
                        {

                            log.DodajWpis(Powodzenie.Error, e.Message,
                                $"błąd dla wiersza o nazwie:{wartosci[1]} oraz kodzie:{wartosci[0]} linia w pliku:{liniaWPliku.ToString()}");
                        }


                        //prograss bar enovy
                        Percent percentProgress = new Percent((decimal)row / wiersze.Count());
                        TraceInfo.SetProgressBar(percentProgress);
                        TraceInfo.WriteProgress($"Przetwarzany Towar o nazwie: {wartosci[1]}");

                        row++;
                    }

                    t.Commit();
                }



            }
            else
                return MessageBoxToReturn("Nieprawidłowy plik!", "Komunikat");
            Log = log;
            return new WyswietlenieWpisowWLogach(Log);
        }

        public MessageBoxInformation MessageBoxToReturn(string tekst, string tytul)//enova okienko w wiadomosciami i bledami
        {

            return new MessageBoxInformation(tytul)
            {
                Text = tekst,

            };

        }



    }
    public class DodanieTowarowZCsvPlikuParams : ContextBase
    {

        public DodanieTowarowZCsvPlikuParams(Context context) : base(context)
        {

        }


        public bool CzyIstniejeNaglowek { get; set; }

        private string _nazwaPliku;
        public string NazwaPliku
        {
            get { return _nazwaPliku; }
            set { _nazwaPliku = value; OnChanged(); }
        }
        private string _plikSciezka;
        public string PlikSciezka
        {
            get { return _plikSciezka; }
            set { _plikSciezka = value; NazwaPliku = PlikSciezka.Substring(PlikSciezka.LastIndexOf(@"\") + 1); }//spojrz na xml jak wygląda przycisk do wyciaganie plikow
        }






    }

}

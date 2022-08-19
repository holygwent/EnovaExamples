using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

using WindowsService1.Services;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
            _sendEmailService = new SendEmailService();


        }
        private readonly ISendEmailService _sendEmailService;


        protected override void OnStart(string[] args)
        {
            Timer timer = new Timer();
            timer.Interval = 10000;
            timer.Enabled = true;
            timer.Elapsed += new ElapsedEventHandler(this.Feature);
            timer.Start();
        }

        protected override void OnStop()
        {
        }

        public void Feature(object sender, ElapsedEventArgs args)
        {
            string path = @"C:\Users\48502\Desktop\dokumenty do wyslania";
            
                string[] filesName = Directory.GetFiles(path, "*.txt");
                foreach (var fileNameWithPath in filesName)
                {
                    _sendEmailService.SendEmail(fileNameWithPath);
                }

          
        }

    }
}


//public void Feature(object sender, ElapsedEventArgs args)
//{
//    string path = @"C:\Users\48502\Desktop\DokumentyDoWyslaniaEmailem.csv";
//    try
//    {
//        using (var reader = new StreamReader(path, Encoding.UTF8))
//        {
//            List<Dokument> dokuments = new List<Dokument>();



//            while (!reader.EndOfStream)
//            {
//                string wiersz = reader.ReadLine();
//                string[] komorki = wiersz.Split(';');
//                string[] pozycje = komorki[4].Split('|');
//                dokuments.Add(new Dokument()
//                {
//                    NazwaKontrahenta = komorki[0],
//                    Email = komorki[1],
//                    NumerDokumentu = komorki[2],
//                    Data = komorki[3],
//                    Pozycje = pozycje
//                });
//            }

//            //catch
//            _sendEmailService.SendEmail(dokuments);
//        }
//    }

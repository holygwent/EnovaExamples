using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsService1.Services
{
    public interface IFileWriteService
    {
        void CreateFileWyslane(string fileNameWithPath);
        void CreateFileNieWyslane(string fileNameWithPath, string errorMessage,string stackTrace);
    }
    public class FileWriteService: IFileWriteService
    {
        

        private  void EnsureDirectoryExists(string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Directory.Exists)
            {
                System.IO.Directory.CreateDirectory(fi.DirectoryName);
            }
        }


        //do przetestowania
        public void CreateFileWyslane(string fileNameWithPath)
        {
            string savePath = @"C:\Users\48502\Desktop\Wyslane\";
            string fileName = fileNameWithPath.Remove(0, fileNameWithPath.LastIndexOf('\\') + 1);
            string wholeSavePath = savePath + fileName;
            try
            {
                EnsureDirectoryExists(savePath);
                File.Move(fileNameWithPath, wholeSavePath);
            }
            catch (Exception e)
            {

                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                    eventLog.WriteEntry("Błąd przenoszeniu plikow " + e.Message +"\n\n"+ e.StackTrace, EventLogEntryType.Error);
                };
            }
           

        }
        public void CreateFileNieWyslane(string fileNameWithPath, string errorMessage, string stackTrace)
        {
            string savePath = @"C:\Users\48502\Desktop\NieWyslane\";
            string fileName = fileNameWithPath.Remove(0, fileNameWithPath.LastIndexOf('\\')+1);
            string wholeSavePath = savePath + fileName;
            try
            {
                EnsureDirectoryExists(savePath);
                File.Move(fileNameWithPath, wholeSavePath);
                using (StreamWriter writer = new StreamWriter(wholeSavePath, true))
                {
                    writer.WriteLine(errorMessage);
                    writer.WriteLine(stackTrace);


                }
            }
            catch (Exception e)
            {

                using (EventLog eventLog = new EventLog("Application"))
                {
                    eventLog.Source = "Application";
                     eventLog.WriteEntry("Błąd : " + e.Message + "\n\n" + e.StackTrace, EventLogEntryType.Error);
                };
            }
           
        }
    }
}

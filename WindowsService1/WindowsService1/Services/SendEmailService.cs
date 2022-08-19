using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace WindowsService1.Services
{
    public interface ISendEmailService
    {
        void SendEmail(string filePathWithName);
    }
    public class SendEmailService : ISendEmailService
    {
        private readonly IFileWriteService _fileWriteService;
        public SendEmailService()
        {
            _fileWriteService = new FileWriteService();
        }
        public void SendEmail(string filePathWithName)
        {
            try
            {

                string[] findEmail;
                string[] separatorEmail = new string[] { "Email:" };

                using (var reader = new StreamReader(filePathWithName, Encoding.UTF8))
                {

                    findEmail = reader.ReadLine().Split(separatorEmail, StringSplitOptions.None);



                }

                string email = findEmail.Where(x => x.Contains("@")).FirstOrDefault();
                if (email is null | email == "")
                {
                    throw new Exception("nieporawny email");
                }
                if (email.Where(x => x == '@').Count() != 1)
                {
                    throw new Exception("nieporawny email");
                }

                using (SmtpClient smtp = new SmtpClient())
                {
                    using (MailMessage message = new MailMessage())
                    {
                        message.From = new MailAddress("dcmieczyslaw@gmail.com");
                        message.To.Add(new MailAddress(email));
                        message.Subject = "dokumenty z pozycjami";
                        message.IsBodyHtml = true;
                        message.Body = "Plik w załączniku";
                        Attachment data = new Attachment(filePathWithName);
                        message.Attachments.Add(data);
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = new NetworkCredential(podaj email do konta, wpisz haslo umozliwiajace apce laczenie sie z gmail);
                        smtp.EnableSsl = true;

                        smtp.Send(message);
                        
                    }
                   
                }
                _fileWriteService.CreateFileWyslane(filePathWithName);
            }
            catch (Exception e)
            {

                _fileWriteService.CreateFileNieWyslane(filePathWithName, e.Message, e.StackTrace);

            }





        }
    }
}

using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace StudentTracking.CORE.EmailService
{
    public class MailSender
    {
        private readonly SmtpSettings _smtpSettings;

        //IOptions, .NET Core uygulamalarında yapılandırma(configuration) değerlerini bir sınıfa bind etmek için kullanılır.
        public MailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public void SendEmail(string toAddresss, string subject, string body, string attachmentPath)
        {
            try
            {
                var smtpClient = new SmtpClient("smtp.office365.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                    EnableSsl = true,
                };

                var fromAddress = new MailAddress(_smtpSettings.Username, "Deneme Ad Soyad");

                var toAddress = new MailAddress(toAddresss);

                var mailMessage = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true,
                };

                if (!string.IsNullOrEmpty(attachmentPath) && File.Exists(attachmentPath))
                {
                    var attachment = new Attachment(attachmentPath, MediaTypeNames.Application.Octet);
                    ContentDisposition disposition = attachment.ContentDisposition;
                    disposition.CreationDate = File.GetCreationTime(attachmentPath);
                    disposition.ModificationDate = File.GetLastWriteTime(attachmentPath);
                    disposition.ReadDate = File.GetLastAccessTime(attachmentPath);
                    mailMessage.Attachments.Add(attachment);
                }

                smtpClient.Send(mailMessage);
                Console.WriteLine("E-posta başarıyla gönderildi");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }
        }
    }
}

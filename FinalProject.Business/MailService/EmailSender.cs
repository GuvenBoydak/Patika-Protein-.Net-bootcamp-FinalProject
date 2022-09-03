using FinalProject.Base;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace FinalProject.Business
{
    public static class EmailSender
    {

        private static IConfiguration _configuration;

        static EmailSender()
        {

            _configuration = Configuration;
        }

        public static IConfiguration Configuration
        {
            get
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");
                _configuration = builder.Build();
                return _configuration;
            }
        }

        public static async Task SendAsync(AppUser appUser,string subject,string body)
        {
            SmtpClient smtp = new SmtpClient();       
            smtp.Host ="smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential(_configuration.GetSection("Mail").Value,_configuration.GetSection("Password").Value);

            MailMessage mail = new MailMessage(_configuration.GetSection("Mail").Value, appUser.Email);
            mail.Subject = subject;
            mail.Body = body;

            try
            {
                await smtp.SendMailAsync(mail);
            }
            catch (Exception)
            {

                Console.WriteLine($"Mesaj gönderilemedi.");
            }
        }
    }
}

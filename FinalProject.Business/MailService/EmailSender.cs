using FinalProject.Entities;
using System.Net;
using System.Net.Mail;

namespace FinalProject.Business
{
    public static class EmailSender
    {
        public static async Task SendAsync(AppUser appUser,string subject,string body)
        {
            SmtpClient smtp = new SmtpClient();       
            smtp.Host ="smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("yzl3157test@gmail.com", "atlkbjteiruyhcww");

            MailMessage mail = new MailMessage("yzl3157test@gmail.com", appUser.Email);
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

using FinalProject.Entities;
using System.Net;
using System.Net.Mail;

namespace FinalProject.RabbitMqConsumer
{
    public static class EmailSender
    {
        public static async Task SendAsync(AppUser appUser)
        {
            SmtpClient smtp = new SmtpClient();       
            smtp.Host ="smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.Credentials = new NetworkCredential("yzl3157test@gmail.com", "atlkbjteiruyhcww");

            MailMessage mail = new MailMessage("denememailservice@gmail.com", appUser.Email);
            mail.Subject = "Bilgilendirme.";
            mail.Body = $"Kulanıcı Başarıyla Kayıt olmuştur. \n \n Kullanıcı UserName : {appUser.UserName} \n  Kullanıcı İsmi : {appUser.FirstName} ";
            
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

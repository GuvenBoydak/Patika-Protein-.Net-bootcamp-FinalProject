using FinalProject.Entities;
using Hangfire;

namespace FinalProject.Business
{
    
    public static class DelayedJob
    {
        /// <summary>
        /// Tetiklendikten 3 sanıye sonra çalışan BackgroundJob.
        /// </summary>
        public static void SendMailJob(AppUser appUser)
        {
            string subject = "Hesap İşlemleri";
            string body = "Parolanız 3 kez yanlış girilmiştir ve Hesabınız askıya alınmıştır.";

            try
            {
                BackgroundJob.Schedule(() => EmailSender.SendAsync(appUser, subject, body), TimeSpan.FromSeconds(3));
            }
            catch (Exception)
            {
                throw new Exception("Hesap İşlemleri Mail gönderimi başarısız.");
            }
        }
    }
}

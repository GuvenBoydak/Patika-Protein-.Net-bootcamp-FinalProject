using FinalProject.Base;
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
            string body = "Parolanız 3 kez yanlış girilmiştir ve Hesabınız askıya alınmıştır. \n Tekrar Actifleştirmek için lütfen linke tıklayınız.\n   https://localhost:7137/api/appusers/activation/" + appUser.ActivationCode + " ";


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

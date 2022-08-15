using FinalProject.Entities;
using Hangfire;

namespace FinalProject.Business
{

    public  class FireAndForgetJob:IFireAndForgetJob
    {
        private  readonly IAppUserService _user;

        public FireAndForgetJob(IAppUserService user)
        {
            _user = user;
        }

        /// <summary>
        /// Tetiklendiginde hemen bir defa çalışan backgroundJob.
        /// </summary>
        public async Task SendMailJobAsync(AppUser appUser)
        {
            string subject = $"Hoş Geldiniz {appUser.UserName}";
            string body = $"Giriş İşlemi Başarılı Hoş geldiniz \n {appUser.UserName}";

            try
            {    
                 BackgroundJob.Enqueue(() => EmailSender.SendAsync(appUser,subject,body));
            }
            catch (Exception)
            {

                throw new Exception("Mail Gönderme Başarısız");
            }

            //Mail göndermede hata yoksa Status u success e cekiyoruz.
            appUser.EmailStatus = EmailStatus.Success;
            await _user.UpdateAsync(appUser);

        }
    }
}

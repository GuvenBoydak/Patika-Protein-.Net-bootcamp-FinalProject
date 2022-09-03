﻿using FinalProject.Base;

using Hangfire;

namespace FinalProject.Business
{
    public interface IFireAndForgetJob
    {
        //Metot hata aldıkca 1 eksilerek çalişmaya devam eder en fazla 5 defa. En son status fail a düşer.
        [AutomaticRetry(Attempts = 5 , OnAttemptsExceeded = AttemptsExceededAction.Fail)]
        void SendMailJob(AppUser appUser);
    }
}

using FluentValidation;

namespace FinalProject.MVCUI
{
    public class AppUserRegisterModelValidator : AbstractValidator<AppUserRegisterModel>
    {
        public AppUserRegisterModelValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kulanıcı İsmi Boş olamalıdır.").MaximumLength(50).WithMessage("Kulanıcı ismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Boş olmamlıdır.").MaximumLength(75).WithMessage("Email Alanı maksimum 75 karakter olamlıdır.").EmailAddress().WithMessage("Girilen deger Email formatinda olmalıdır.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre Alanı boş olmamalıdır.").MinimumLength(8).WithMessage("Şifre alanı en az 8 karakter olamlıdır.").MaximumLength(20).WithMessage("Şifre Alanı en fazla 20 karakter olamlıdır.");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Kulanıcı ismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Kulanıcı soyismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.PhoneNumber).MaximumLength(15).WithMessage("Telefon Numarası en fazla 15 karakter olmalıdır.").Matches("^[0-9]+$").WithMessage("Sadece Rakam Giriniz");
        }
    }
}

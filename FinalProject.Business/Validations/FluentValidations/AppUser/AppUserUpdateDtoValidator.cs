using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class AppUserUpdateDtoValidator:AbstractValidator<AppUserUpdateDto>
    {
        public AppUserUpdateDtoValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(50).WithMessage("Kulanıcı ismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.Email).MaximumLength(75).WithMessage("Email Alanı maksimum 75 karakter olamlıdır.").EmailAddress().WithMessage("Girilen deger Email formatinda olmalıdır.");
            RuleFor(x => x.FirstName).MaximumLength(50).WithMessage("Kulanıcı ismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.LastName).MaximumLength(50).WithMessage("Kulanıcı soyismi en fazla 50 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.PhoneNumber).MaximumLength(15).WithMessage("Telefon Numarası en fazla 15 karakter olmalıdır.").Matches("^[0-9]+$").WithMessage("Sadece Rakam Giriniz");
        }
    }
}

using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class AppUserPasswordUpdateDtoValidator : AbstractValidator<AppUserPasswordUpdateDto>
    {
        public AppUserPasswordUpdateDtoValidator()
        {
            RuleFor(x => x.OldPassword).NotEmpty().WithMessage("Şifre Alanı boş olmamalıdır.").MinimumLength(8).WithMessage("Şifre alanı en az 8 karakter olamlıdır.").MaximumLength(20).WithMessage("Şifre Alanı en fazla 20 karakter olamlıdır.");
            RuleFor(x => x.NewPassword).NotEmpty().WithMessage("Şifre Alanı boş olmamalıdır.").MinimumLength(8).WithMessage("Şifre alanı en az 8 karakter olamlıdır.").MaximumLength(20).WithMessage("Şifre Alanı en fazla 20 karakter olamlıdır.");
            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("Şifre Alanı boş olmamalıdır.").MinimumLength(8).WithMessage("Şifre alanı en az 8 karakter olamlıdır.").MaximumLength(20).WithMessage("Şifre Alanı en fazla 20 karakter olamlıdır.");
        }
    }
}

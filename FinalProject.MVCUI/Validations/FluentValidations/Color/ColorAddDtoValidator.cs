using FluentValidation;

namespace FinalProject.MVCUI
{
    public class ColorAddModelValidator:AbstractValidator<ColorModel>
    {
        public ColorAddModelValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Renk İsmi boş geçilemez").MaximumLength(50).WithMessage("Renk ismi en fazla 40 karakter olamlıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
        }
    }
}

using FluentValidation;

namespace FinalProject.MVCUI
{
    public class ColorUpdateModelValidator : AbstractValidator<ColorModel>
    {
        public ColorUpdateModelValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Renk İsmi boş geçilemez").MaximumLength(50).WithMessage("Renk ismi en fazla 40 karakter olamlıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
        }
    }
}

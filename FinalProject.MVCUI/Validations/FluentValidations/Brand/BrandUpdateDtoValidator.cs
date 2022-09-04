using FluentValidation;

namespace FinalProject.MVCUI
{
    public class BrandUpdateModelValidator : AbstractValidator<BrandModel>
    {
        public BrandUpdateModelValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Marka İsmi boş geçilemez").MaximumLength(50).WithMessage("Marka ismi en fazla 50 karakter olamlıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
        }
    }
}

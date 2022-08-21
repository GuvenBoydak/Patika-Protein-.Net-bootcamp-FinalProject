using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class ColorUpdateDtoValidator : AbstractValidator<ColorUpdateDto>
    {
        public ColorUpdateDtoValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Renk İsmi boş geçilemez").MaximumLength(50).WithMessage("Renk ismi en fazla 40 karakter olamlıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖö\s@]*$").WithMessage("Sadece Harf Giriniz.");
        }
    }
}

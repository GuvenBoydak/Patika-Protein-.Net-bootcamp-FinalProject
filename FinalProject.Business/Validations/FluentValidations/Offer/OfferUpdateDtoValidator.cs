using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class OfferUpdateDtoValidator : AbstractValidator<OfferUpdateDto>
    {
        public OfferUpdateDtoValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş olamamalı").GreaterThan(0).WithMessage("Fiyat alanı sıfırdan büyük olmalıdır");

        }
    }
}

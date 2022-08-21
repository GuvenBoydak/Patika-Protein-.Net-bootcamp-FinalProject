using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class OfferUpdateDtoValidator : AbstractValidator<OfferUpdateDto>
    {
        public OfferUpdateDtoValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Price).InclusiveBetween(0, int.MaxValue).WithMessage("Fiyat alanı sıfırdan büyük olmalıdır");
            RuleFor(x => x.ProductID).InclusiveBetween(0, int.MaxValue).WithMessage("Ürün alanı sıfırdan büyük olmalıdır");

        }
    }
}

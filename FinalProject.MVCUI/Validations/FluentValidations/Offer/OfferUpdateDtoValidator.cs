using FluentValidation;

namespace FinalProject.MVCUI
{
    public class OfferUpdateModelValidator : AbstractValidator<OfferModel>
    {
        public OfferUpdateModelValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Price).InclusiveBetween(0, int.MaxValue).WithMessage("Fiyat alanı sıfırdan büyük olmalıdır");
            RuleFor(x => x.ProductID).InclusiveBetween(0, int.MaxValue).WithMessage("Ürün alanı sıfırdan büyük olmalıdır");

        }
    }
}

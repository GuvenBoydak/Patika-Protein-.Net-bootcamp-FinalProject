using FluentValidation;

namespace FinalProject.MVCUI
{
    public class OfferAddModelValidator:AbstractValidator<OfferModel>
    {
        public OfferAddModelValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş olamamalı").GreaterThan(0).WithMessage("Fiyat alanı sıfırdan büyük olmalıdır");
            RuleFor(x => x.ProductID).NotEmpty().WithMessage("Ürün alanı boş olamamalı").GreaterThan(0).WithMessage("Ürün alanı sıfırdan büyük olmalıdır");
        }
    }
}

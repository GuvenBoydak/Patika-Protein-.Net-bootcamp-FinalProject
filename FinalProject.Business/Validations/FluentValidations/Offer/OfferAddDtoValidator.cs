using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class OfferAddDtoValidator:AbstractValidator<OfferAddDto>
    {
        public OfferAddDtoValidator()
        {
            RuleFor(x => x.Price).NotEmpty().WithMessage("Fiyat alanı boş olamamalı").GreaterThan(0).WithMessage("Fiyat alanı sıfırdan büyük olmalıdır");
            RuleFor(x => x.AppUserID).NotEmpty().WithMessage("Kulanıcı alanı boş olamamalı").GreaterThan(0).WithMessage("kullanıcı alanı sıfırdan büyük olmalıdır");
            RuleFor(x => x.ProductID).NotEmpty().WithMessage("Ürün alanı boş olamamalı").GreaterThan(0).WithMessage("Ürün alanı sıfırdan büyük olmalıdır");
        }
    }
}

using FluentValidation;

namespace FinalProject.MVCUI
{
    public class ProductUpdateModelValidator:AbstractValidator<ProductModel>
    {
        public ProductUpdateModelValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Ürün Adı en fazla 100 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖö\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.UnitPrice).InclusiveBetween(0,int.MaxValue).WithMessage("Girilen Fiyat sıfırdan büyük olamlı");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Girilen deger en Fazla 500 karakter olmalıdır.")
                .Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖöIıUuOo\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.CategoryID).InclusiveBetween(0, int.MaxValue).WithMessage("Kategori Id alını sıfırdan büyük olmalıdır.");
        }
    }
}
    
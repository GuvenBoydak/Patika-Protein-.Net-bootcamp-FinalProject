using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class ProductUpdateDtoValidator:AbstractValidator<ProductUpdateDto>
    {
        public ProductUpdateDtoValidator()
        {
            RuleFor(x => x.ID).NotEmpty().WithMessage("ID alanı boş geçilemez").GreaterThan(0).WithMessage("ID deger 0 dan buyuk olmalıdır.");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Ürün Adı en fazla 100 karakter olmalıdır.").Matches("^[a-zA-ZiİçÇşŞğĞÜüÖö]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Girilen Fiyat sıfırdan büyük olamlı");
            RuleFor(x => x.UnitInStock).GreaterThan(0).WithMessage("Girilen deger sıfırdan büyük olamlıdır.");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("Girilen deger en Fazla 500 karakter olmalıdır.").Matches("^[a-zA-ZiİçÇşŞğĞÜüÖö]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.CategoryID).GreaterThan(0).WithMessage("Kategori Id alını sıfırdan büyük olmalıdır.");
        }
    }
}

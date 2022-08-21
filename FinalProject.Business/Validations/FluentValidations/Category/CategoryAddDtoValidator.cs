using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class CategoryAddDtoValidator:AbstractValidator<CategoryAddDto>
    {
        public CategoryAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori İsmi boş geçilemez").MaximumLength(50).WithMessage("Kategori ismi en fazla 50 karakter olamlıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖö\s@]*$").WithMessage("Sadece Harf Giriniz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Kategori Açıklaması alanı boş geçilemez.").MaximumLength(500).WithMessage("Girilen deger en Fazla 500 karakter olmalıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖö\s@]*$").WithMessage("Sadece Harf Giriniz.");
        }
    }
}

using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class BrandAddDtoValidator:AbstractValidator<BrandAddDto>
    {
        public BrandAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Marka İsmi boş geçilemez").MaximumLength(50).WithMessage("Marka ismi en fazla 50 karakter olamlıdır.").Matches(@"^[a-zA-ZiİçÇşŞğĞÜüÖö\s@]*$").WithMessage("Sadece Harf Giriniz.");
        }
    }
}

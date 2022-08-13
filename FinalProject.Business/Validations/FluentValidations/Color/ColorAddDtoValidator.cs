using FinalProject.DTO;
using FluentValidation;

namespace FinalProject.Business
{
    public class ColorAddDtoValidator:AbstractValidator<ColorAddDto>
    {
        public ColorAddDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Renk İsmi boş geçilemez").MaximumLength(50).WithMessage("Renk ismi en fazla 40 karakter olamlıdır.").Matches("^[a-zA-ZiİçÇşŞğĞÜüÖö]*$").WithMessage("Sadece Harf Giriniz.");
        }
    }
}

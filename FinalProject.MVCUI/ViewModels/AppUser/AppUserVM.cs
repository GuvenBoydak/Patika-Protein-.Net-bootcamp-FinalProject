using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.MVCUI
{
    public class AppUserVM
    {
        public AppUser AppUser { get; set; }

        public AppUserPasswordUpdateDto AppUserPasswordUpdateDto { get; set; }

        public List<Product> Products { get; set; }

        public List<Offer> Offers { get; set; }

        public List<Category> Categories { get; set; }

        public List<Brand> Brands { get; set; }

        public List<Color> Colors { get; set; }

        public AppUserRegisterDto AppUserRegisterDto { get; set; }

        public AppUserLoginDto AppUserLoginDto { get; set; }

    }
}

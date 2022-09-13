namespace FinalProject.MVCUI
{
    public class AppUserVM
    {
        public AppUserModel AppUser { get; set; }
        public List<AppUserModel> AppUsers { get; set; }

        public AppUserPasswordUpdateModel AppUserPasswordUpdateModel { get; set; }

        public List<ProductModel> Products { get; set; }

        public List<OfferModel> Offers { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<BrandModel> Brands { get; set; }

        public List<ColorModel> Colors { get; set; }

        public AppUserRegisterModel AppUserRegisterModel { get; set; }

        public AppUserLoginModel AppUserLoginModel { get; set; }
    }
}

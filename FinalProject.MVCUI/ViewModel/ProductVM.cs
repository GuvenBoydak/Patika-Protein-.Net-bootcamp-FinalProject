using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.MVCUI
{
    public class ProductVM
    {
        public List<ProductModel> Products { get; set; }

        public List<CategoryModel> Categories { get; set; }

        public List<BrandModel> Brands { get; set; }

        public List<ColorModel> Colors { get; set; }

        public ProductModel Product { get; set; }

        public ProductAddWithFileModel ProductAddWithFileModel { get; set; }

        public OfferModel Offer { get; set; }

        public CategoryModel Category { get; set; }

        public List<SelectListItem> PriceList { get; set; }

        public AppUserModel AppUser { get; set; }
    }
}

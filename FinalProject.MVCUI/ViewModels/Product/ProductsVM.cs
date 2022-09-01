using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.MVCUI
{
    public class ProductsVM
    {
        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public List<Brand> Brands { get; set; }

        public List<Color> Colors { get; set; }

        public Product Product { get; set; }

        public ProductAddWitFileDto ProductAddWitFileDto { get; set; }

        public Offer Offer { get; set; }

        public CategoryDto Category { get; set; }

        public List<SelectListItem> PriceList { get; set; }

        public AppUser AppUser { get; set; }
    }
}

using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.MVCUI
{
    public class ProductsVM
    {
        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public List<Brand> Brands { get; set; }

        public List<Color> Colors { get; set; }

        public Product Product { get; set; }

        public ProductAddDto ProductAddDto { get; set; }

        public ProductUpdateDto ProductUpdateDto { get; set; }
    }
}

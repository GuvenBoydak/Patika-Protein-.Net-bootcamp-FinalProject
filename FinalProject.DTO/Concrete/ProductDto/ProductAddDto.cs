using FinalProject.Entities;

namespace FinalProject.DTO
{
    public class ProductAddDto : BaseDto
    {
        public string Name { get; set; }

        public int UnitsInStock { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public UsageStatus UsageStatus { get; set; }

        public int CategoryID { get; set; }

        public int? BrandID { get; set; }

        public int? ColorID { get; set; }

    }
}

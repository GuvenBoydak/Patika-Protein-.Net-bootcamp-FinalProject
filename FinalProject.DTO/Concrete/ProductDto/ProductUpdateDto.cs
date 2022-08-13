using FinalProject.Entities;

namespace FinalProject.DTO
{
    public class ProductUpdateDto : BaseDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int UnitInStock { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public bool IsOfferable { get; set; }

        public bool IsSold { get; set; }

        public UsageStatus UsageStatus { get; set; }

        public int CategoryID { get; set; }

        public int? BrandID { get; set; }

        public int? ColorID { get; set; }

        public int? OfferID { get; set; }
    }
}

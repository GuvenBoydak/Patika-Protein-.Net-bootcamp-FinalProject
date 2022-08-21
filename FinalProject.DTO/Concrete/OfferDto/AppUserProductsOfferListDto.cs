using FinalProject.Entities;

namespace FinalProject.DTO
{
    public class AppUserProductsOfferListDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public UsageStatus UsageStatus { get; set; }

        public int CategoryID { get; set; }

        public int? BrandID { get; set; }

        public int? ColorID { get; set; }

        public int? OfferID { get; set; }

        public int AppUserID { get; set; }

        public List<OfferDto> Offers { get; set; }

    }
}

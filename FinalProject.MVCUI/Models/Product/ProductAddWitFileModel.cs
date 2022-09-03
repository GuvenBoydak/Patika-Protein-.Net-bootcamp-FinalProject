namespace FinalProject.MVCUI
{
    public class ProductAddWithFileModel 
    {
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public string Description { get; set; }

        public UsageStatus UsageStatus { get; set; }

        public int CategoryID { get; set; }

        public int? BrandID { get; set; }

        public int? ColorID { get; set; }

        public IFormFile ImageUrl { get; set; }
    }
}

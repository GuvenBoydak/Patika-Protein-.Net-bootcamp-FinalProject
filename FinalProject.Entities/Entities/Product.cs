using FinalProject.Base;

namespace FinalProject.Entities
{
    public class Product:BaseEntity
    {
        public Product()
        {
            IsOfferable = false;
            IsSold = false;
        }

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

        public int AppUserID { get; set; }


        //Relational Properties

        public virtual Category Category { get; set; }

        public virtual Brand Brand { get; set; }

        public virtual Color Color { get; set; }

        public virtual Offer Offer { get; set; }

        public virtual AppUser AppUser { get; set; }


    }
}

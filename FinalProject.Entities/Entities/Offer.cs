using FinalProject.Base;

namespace FinalProject.Entities
{
    public class Offer:BaseEntity
    {
        public Offer()
        {
            IsApproved = false;
        }

        public decimal Price { get; set; }

        public bool IsApproved { get; set; }
        
        public int AppUserID { get; set; }


        //Relational Properties

        public virtual List<Product> Products { get; set; }

        public virtual AppUser AppUser { get; set; }


    }
}

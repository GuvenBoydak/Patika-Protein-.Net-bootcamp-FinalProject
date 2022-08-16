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

        public int ProductID { get; set; }

        //Relational Properties
        [DapperIgnore]
        public  Product Product { get; set; }
        [DapperIgnore]
        public  AppUser AppUser { get; set; }


    }
}

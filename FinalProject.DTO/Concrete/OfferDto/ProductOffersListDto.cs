using FinalProject.Entities;

namespace FinalProject.DTO
{
    public class ProductOffersListDto
    {
        public int ID { get; set; }

        public decimal Price { get; set; }

        public bool IsApproved { get; set; }

        public int AppUserID { get; set; }

        public int ProductID { get; set; }
    }
}

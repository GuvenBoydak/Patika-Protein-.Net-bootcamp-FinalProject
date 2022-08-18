namespace FinalProject.DTO
{
    public class OfferBuyProductDto
    {
        public decimal Price { get; set; }

        public bool IsApproved { get; set; }

        public int AppUserID { get; set; }

        public int ProductID { get; set; }
    }
}

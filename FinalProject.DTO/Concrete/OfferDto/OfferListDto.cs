namespace FinalProject.DTO
{
    public class OfferListDto : BaseDto
    {
        public int ID { get; set; }

        public decimal Price { get; set; }

        public bool IsApproved { get; set; }

        public int AppUserID { get; set; }
    }
}

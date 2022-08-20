namespace FinalProject.DTO
{
    public class OfferAddDto : BaseDto
    {
        public decimal Price { get; set; }

        public bool IsApproved { get; set; }

        public int AppUserID { get; set; }

        public int ProductID { get; set; }

    }
}

namespace FinalProject.DTO
{
    public class OfferUpdateDto : BaseDto
    {
        public int ID { get; set; }

        public decimal Price { get; set; }

        public bool IsApproved { get; set; }

        public int ProductID { get; set; }
    }
}

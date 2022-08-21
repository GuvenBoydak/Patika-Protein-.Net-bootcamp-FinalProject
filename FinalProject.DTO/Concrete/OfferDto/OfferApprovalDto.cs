namespace FinalProject.DTO
{
    public class OfferApprovalDto:BaseDto
    {
        public int ID { get; set; }

        public bool IsApproved { get; set; }

        public int ProductID { get; set; }
    }
}

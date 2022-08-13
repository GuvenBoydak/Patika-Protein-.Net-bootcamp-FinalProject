namespace FinalProject.DTO
{
    public class AppUserRegisterOptionalDto : BaseDto
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string? PhoneNumber { get; set; }
    }
}

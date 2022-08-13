namespace FinalProject.DTO
{
    public class AppUserPasswordUpdateDto : BaseDto
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}

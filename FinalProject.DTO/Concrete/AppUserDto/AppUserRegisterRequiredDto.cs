namespace FinalProject.DTO
{
    public class AppUserRegisterRequiredDto : BaseDto
    {
        public string UserName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}

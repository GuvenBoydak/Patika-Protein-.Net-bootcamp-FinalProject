namespace FinalProject.Base
{
    public class AppUser:BaseEntity
    {
        public AppUser()
        {
            Active = false;
            ActivationCode = Guid.NewGuid();
            LastActivty = DateTime.UtcNow;
            IsLock = false;
        }

        public string UserName { get; set; }

        public string Email { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public short IncorrectEntry { get; set; }

        public bool IsLock { get; set; }

        public Guid ActivationCode { get; set; }

        public bool Active { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime LastActivty { get; set; }

        public string PhoneNumber { get; set; }

        //Relational Properties
        [DapperIgnoreAttribute]
        public  List<AppUserRole> AppUserRoles { get; set; }

    }
}

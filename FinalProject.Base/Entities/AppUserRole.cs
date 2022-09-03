namespace FinalProject.Base
{
    public class AppUserRole:BaseEntity
    {
        public int AppUserID { get; set; }

        public int RoleID { get; set; }

        //Relational Properties
        [DapperIgnoreAttribute]
        public Role Role { get; set; }

        [DapperIgnoreAttribute]
        public AppUser AppUser { get; set; }

    }
}

namespace FinalProject.Base
{
    public class Role:BaseEntity
    {
        public string Name { get; set; }

        //Relational Properties
        [DapperIgnoreAttribute]
        public List<AppUserRole> AppUserRoles { get; set; }
    }
}

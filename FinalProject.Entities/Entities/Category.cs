using FinalProject.Base;

namespace FinalProject.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //Relational Properyties
        [DapperIgnore]
        public  List<Product> Products { get; set; }
    }
}

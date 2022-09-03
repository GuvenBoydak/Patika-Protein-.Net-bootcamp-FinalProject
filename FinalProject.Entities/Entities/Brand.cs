using FinalProject.Base;

namespace FinalProject.Entities
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }

        //Relational Properties
        [DapperIgnore]
        public  List<Product> Products { get; set; }
    }
}

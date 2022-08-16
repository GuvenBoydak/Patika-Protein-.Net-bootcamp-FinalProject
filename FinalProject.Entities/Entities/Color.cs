using FinalProject.Base;

namespace FinalProject.Entities
{
    public class Color:BaseEntity
    {
        public string Name { get; set; }

        //Relational Properties
        [DapperIgnore]
        public virtual List<Product> Products { get; set; }
    }
}

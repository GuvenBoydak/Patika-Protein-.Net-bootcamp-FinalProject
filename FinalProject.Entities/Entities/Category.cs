namespace FinalProject.Entities
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        //Relational Properyties

        public virtual List<Product> Products { get; set; }
    }
}

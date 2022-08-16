using FinalProject.Entities;

namespace FinalProject.DTO
{
    public class CategoryDto:BaseDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Product> Products { get; set; }
    }
}

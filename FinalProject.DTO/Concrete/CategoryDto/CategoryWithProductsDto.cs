namespace FinalProject.DTO
{
    public class CategoryWithProductsDto:BaseDto
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<ProductDto> Products { get; set; }
    }
}

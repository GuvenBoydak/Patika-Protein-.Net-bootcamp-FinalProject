using FinalProject.DTO;

namespace FinalProject.MVCUI
{
    public class CategoryVM
    {
        public List<CategoryListDto> CategoryListDtos { get; set; }


        public CategoryAddDto CategoryAddDto { get; set; }

        public CategoryUpdateDto CategoryUpdateDto { get; set; }
    }
}

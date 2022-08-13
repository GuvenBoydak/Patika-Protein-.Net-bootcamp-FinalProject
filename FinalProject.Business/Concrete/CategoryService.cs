using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class CategoryService : GenericService<Category>, ICategoryService
    {
        public CategoryService(IRepository<Category> repository) : base(repository)
        {
        }
    }

}

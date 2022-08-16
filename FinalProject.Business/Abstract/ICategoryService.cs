using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface ICategoryService : IBaseService<Category>
    {
        Task UpdateAsync(Category entity);

        void Delete(int id);

        Task<Category> GetCategoryWithProductsAsync(int id);
    }

}

using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Delete(int id);

        Task UpdateAsync(Category entity);

        Task<Category> GetCategoryWithProductsAsync(int id);
    }
}

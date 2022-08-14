using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IProductRepository : IRepository<Product>
    {
        void Delete(int id);

        Task UpdateAsync(Product entity);
    }
}

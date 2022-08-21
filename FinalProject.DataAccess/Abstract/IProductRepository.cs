using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IProductRepository : IRepository<Product>
    {
        void Delete(int id);

        Task UpdateAsync(Product entity);

        Task<List<Product>> GetByAppUserProductsWithOffers(int id);

        Task<List<Product>> GetByProductsPaginationAsync(int limit,int page);
    }
}

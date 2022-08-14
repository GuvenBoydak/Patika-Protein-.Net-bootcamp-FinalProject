using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface IProductService : IBaseService<Product>
    {
        Task UpdateAsync(Product entity);

        void Delete(int id);
    }

}

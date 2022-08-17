using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class ProductService : GenericRepository<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IDapperContext dapperContext, IProductRepository productRepository) : base(dapperContext)
        {
            _productRepository = productRepository;
        }

        public void Delete(int id)
        {
            try
            {
                _productRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error {typeof(Product).Name} =>  {e.Message}");
            }
        }

        public async Task<List<Product>> GetByAppUserProductsWithOffers(int id)
        {
            return await _productRepository.GetByAppUserProductsWithOffers(id);
        }

        public async Task UpdateAsync(Product entity)
        {
            try
            {
                await _productRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error {typeof(Product).Name} =>  {e.Message}");
            }
        }
    }
}

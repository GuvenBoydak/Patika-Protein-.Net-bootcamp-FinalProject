using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class ProductService : GenericService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) : base(productRepository)
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

                throw new Exception($"Delete_Error  =>  {e.Message}");
            }
        }

        /// <summary>
        /// Kulanıcının ürünlerinin tekliflerinin listesi
        /// </summary>
        /// <param name="id">kullanıcı id bilsigi</param>
        public async Task<List<Product>> GetByAppUserProductsWithOffers(int id)
        {
            return await _productRepository.GetByAppUserProductsWithOffers(id);
        }

        public Task<List<Product>> GetByProductsPaginationAsync(int limit, int page)
        {
            return _productRepository.GetByProductsPaginationAsync(limit,page);
        }

        public async Task UpdateAsync(Product entity)
        {
            try
            {
                await _productRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error  =>  {e.Message}");
            }
        }
    }
}

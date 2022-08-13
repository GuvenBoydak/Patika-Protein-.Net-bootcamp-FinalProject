using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class ProductService : GenericRepository<Product>, IProductService
    {
        public ProductService(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }
}

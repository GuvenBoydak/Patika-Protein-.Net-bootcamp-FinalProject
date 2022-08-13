using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpProductRespository : GenericRepository<Product>, IProductRepository
    {
        public DpProductRespository(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }



}

using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class BrandService : GenericRepository<Brand>, IBrandService
    {
        public BrandService(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }
}

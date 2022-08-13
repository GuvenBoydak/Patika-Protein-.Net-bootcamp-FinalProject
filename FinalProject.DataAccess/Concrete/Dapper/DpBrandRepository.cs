using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpBrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public DpBrandRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }



}

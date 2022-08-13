using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpCategoryRespository : GenericRepository<Category>, ICategoryRepository
    {
        public DpCategoryRespository(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }



}

using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpAppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public DpAppUserRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }



}

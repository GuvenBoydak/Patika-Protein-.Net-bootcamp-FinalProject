using FinalProject.Base;

namespace FinalProject.DataAccess
{
    public class DpAppUserRoleRepository : GenericRepository<AppUserRole>, IAppUserRoleRepository
    {
        public DpAppUserRoleRepository(IDapperContext dapperContext) : base(dapperContext)
        {

        }

    }
}

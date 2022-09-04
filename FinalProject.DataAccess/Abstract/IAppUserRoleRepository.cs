using FinalProject.Base;

namespace FinalProject.DataAccess
{
    public interface IAppUserRoleRepository:IRepository<AppUserRole>
    {
        Task<List<AppUserRole>> GetAppUserID(int id);

        void Delete(int id);

        Task UpdateAsync(AppUserRole appUserRole);
    }
}

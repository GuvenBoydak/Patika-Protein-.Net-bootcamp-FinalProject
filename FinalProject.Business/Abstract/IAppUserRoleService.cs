using FinalProject.Base;

namespace FinalProject.Business
{
    public interface IAppUserRoleService:IBaseService<AppUserRole>
    {
        Task UpdateAsync(AppUserRole appUserRole);

        void Delete(int id);
    }
}

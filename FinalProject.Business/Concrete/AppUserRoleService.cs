using FinalProject.Base;
using FinalProject.DataAccess;

namespace FinalProject.Business
{
    public class AppUserRoleService : GenericService<AppUserRole>, IAppUserRoleService
    {
        private readonly IAppUserRoleRepository _appUserRoleRepository;

        public AppUserRoleService(IAppUserRoleRepository appUserRoleRepository):base(appUserRoleRepository)
        {
            _appUserRoleRepository = appUserRoleRepository;
        }

        public void Delete(int id)
        {
            try
            {
                _appUserRoleRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error  =>  {e.Message}");
            }
        }

        public async Task UpdateAsync(AppUserRole appUserRole)
        {
            try
            {
              await  _appUserRoleRepository.UpdateAsync(appUserRole);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error  =>  {e.Message}");
            }
        }
    }
}

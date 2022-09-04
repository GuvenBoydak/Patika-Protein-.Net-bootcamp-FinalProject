using FinalProject.Base;
using FinalProject.DataAccess;

namespace FinalProject.Business
{
    public class RoleService : GenericService<Role>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService( IRoleRepository roleRepository) : base(roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public void Delete(int id)
        {
            try
            {
                _roleRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error  =>  {e.Message}");
            }
        }

        public async Task UpdateAsync(Role role)
        {
            try
            {
              await  _roleRepository.UpdateAsync(role);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error  =>  {e.Message}");
            }
        }
    }
}

using FinalProject.Base;

namespace FinalProject.Business
{
    public interface IRoleService:IBaseService<Role>
    {
        Task UpdateAsync(Role role);

        void Delete(int id);
    }
}

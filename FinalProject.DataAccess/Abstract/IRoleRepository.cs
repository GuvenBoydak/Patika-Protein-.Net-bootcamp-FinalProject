using FinalProject.Base;

namespace FinalProject.DataAccess
{
    public interface IRoleRepository:IRepository<Role>
    {

        void Delete(int id);

        Task UpdateAsync(Role role);
    }
}

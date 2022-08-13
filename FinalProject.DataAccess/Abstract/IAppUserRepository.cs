using FinalProject.Base;

namespace FinalProject.DataAccess
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByEmailAsync(string email);
    }
}

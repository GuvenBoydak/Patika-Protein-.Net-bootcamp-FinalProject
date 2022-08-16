using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByEmailAsync(string email);

        void Delete(int id);

        Task UpdateAsync(AppUser appUser);

        Task<List<AppUser>> GetByOffers(int id);
    }
}

using FinalProject.Entities;
using FinalProject.Base;


namespace FinalProject.DataAccess
{
    public interface IAppUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByEmailAsync(string email);

        void Delete(int id);

        Task UpdateAsync(AppUser appUser);

        Task<AppUser> GetByActivationCode(Guid code);

        Task<List<Product>> GetAppUserProductsAsync(int id);

        Task<List<Role>> GetRoles(AppUser appUser);
   }
}

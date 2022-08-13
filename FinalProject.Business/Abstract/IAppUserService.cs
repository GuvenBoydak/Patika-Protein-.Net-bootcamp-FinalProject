using FinalProject.Base;
using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface IAppUserService : IBaseService<AppUser>
    {
        Task<AppUser> RegisterAsync(AppUserRegisterRequiredDto requiredEntity,AppUserRegisterOptionalDto optionalEntity);

        Task<AppUser> LoginAsync(AppUserLoginDto entity);

        Task<AppUser> GetByEmailAsync(string email);

        AccessToken CreateAccessToken(AppUser entity);

        Task UpdatePasswordAsync(int id, AppUserPasswordUpdateDto entity);
    }

}

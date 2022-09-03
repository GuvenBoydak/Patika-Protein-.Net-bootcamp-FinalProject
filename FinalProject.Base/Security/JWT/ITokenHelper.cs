
namespace FinalProject.Base
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(AppUser appUser,List<Role> roles);
    }
}

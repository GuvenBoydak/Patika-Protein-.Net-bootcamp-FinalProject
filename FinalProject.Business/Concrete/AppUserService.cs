using FinalProject.Base;
using FinalProject.DataAccess;
using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class AppUserService : GenericService<AppUser>, IAppUserService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public AppUserService(IRepository<AppUser> repository, IAppUserRepository userRepository, ITokenHelper tokenHelper) : base(repository)
        {
            _userRepository = userRepository;
            _tokenHelper = tokenHelper;
        }

        public AccessToken CreateAccessToken(AppUser entity)
        {
            AccessToken accessToken = _tokenHelper.CreateToken(entity.UserName, entity.ID);
            return accessToken;
        }

        public void Delete(int id)
        {
            try
            {
                _userRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error {typeof(AppUser).Name} =>  {e.Message}");
            }
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            AppUser appUser = await _userRepository.GetByEmailAsync(email);
            return appUser;
        }

        public async Task<AppUser> LoginAsync(AppUserLoginDto entity)
        {
            AppUser appUser = await GetByEmailAsync(entity.Email);

            if (appUser == null)
                throw new InvalidOperationException($"{typeof(AppUser).Name}({entity.Email}) User Not Found");

            //appUser.LastActivty = DateTime.UtcNow;
            //await UpdateAsync(appUser);

            //Kulanıcı gridigi Password'u Databaseden gelen PasswordHash ve PasswordSalt ile hashleyip kontrol ediyoruz.
            if (!HashingHelper.VerifyPasswordHash(entity.Password, appUser.PasswordHash, appUser.PasswordSalt))
            {
                appUser.IncorrectEntry++; //Şifre yanlış girildiyse 1 atırıyoruz.

                if (appUser.IncorrectEntry == 3) appUser.IsLock = true; //3 kere yanlış girilen kulanıcıyı Lock ediyoruz.

                await UpdateAsync(appUser);
                throw new InvalidOperationException($"{typeof(AppUser).Name} User Password Does Not Match ");
            }
            return appUser;
        }

        public async Task<AppUser> RegisterAsync(AppUserRegisterDto registerDto)
        {
            AppUser appUser = await GetByEmailAsync(registerDto.Email);
            //Girilen Email'i databasede varmı Kontrol ediyoruz.
            if (appUser !=null)
                throw new InvalidOperationException($"{typeof(AppUser).Name}({registerDto.Email}) Email Already Exists");

            //PasswordHash ve PasswordSalt oluşturuyoruz.
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(registerDto.Password, out passwordHash, out passwordSalt);

            AppUser user = new AppUser()
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                DateOfBirth = registerDto.DateOfBirth.Value,
                PhoneNumber = registerDto.PhoneNumber,
            };

            try
            {
                _userRepository.Add(user);
            }
            catch (Exception e)
            {
                throw new Exception($"Register_Error {typeof(AppUser).Name} =>  {e.Message}");
            }
            return user;
        }

        public async Task UpdateAsync(AppUser entity)
        {
            try
            {
                await _userRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {
                throw new Exception($"Update_Error {typeof(AppUser).Name} =>  {e.Message}");

            }
        }

        public async Task UpdatePasswordAsync(int id, AppUserPasswordUpdateDto entity)
        {
            AppUser appUser = await _userRepository.GetByIDAsync(id);
            if (appUser == null) //Kulanıcıyı kontrol edıyoruz
                throw new InvalidOperationException($"{typeof(AppUser).Name}({id}) User Not Found");

            //Kulanıcı gridigi Password'u Databaseden gelen PasswordHash ve PasswordSalt ile hashleyip kontrol ediyoruz.
            if (!HashingHelper.VerifyPasswordHash(entity.OldPassword, appUser.PasswordHash, appUser.PasswordSalt))
                throw new InvalidOperationException($"{typeof(AppUser).Name} User Password Does Not Match ");

            //Yeni PasswordHash ve PasswordSalt oluşturuyoruz.
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(entity.NewPassword, out passwordHash, out passwordSalt);

            appUser.PasswordHash = passwordHash;
            appUser.PasswordSalt = passwordSalt;

            await UpdateAsync(appUser); //Yeni Şifreyle kullanıyı güncelliyoruz.
        }
    }
}

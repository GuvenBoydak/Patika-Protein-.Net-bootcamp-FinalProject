using FinalProject.Base;
using FinalProject.DataAccess;
using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class AppUserService : GenericRepository<AppUser>, IAppUserService
    {
        private readonly IAppUserRepository _userRepository;
        private readonly ITokenHelper _tokenHelper;

        public AppUserService(IDapperContext dapperContext, ITokenHelper tokenHelper) : base(dapperContext)
        {
            _tokenHelper = tokenHelper;
        }

        public AccessToken CreateAccessToken(AppUser entity)
        {
            AccessToken accessToken = _tokenHelper.CreateToken(entity.UserName,entity.ID);
            return accessToken;
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            AppUser appUser =await _userRepository.GetByEmailAsync(email);
            return appUser;
        }

        public async Task<AppUser> LoginAsync(AppUserLoginDto entity)
        {
            AppUser appUser = await GetByEmailAsync(entity.Email);

            if (appUser == null)
                throw new InvalidOperationException($"{typeof(AppUser).Name}({entity.Email}) User Not Found");

            appUser.LastActivty = DateTime.UtcNow;
            _userRepository.Update(appUser);

            //Kulanıcı gridigi Password'u Databaseden gelen PasswordHash ve PasswordSalt ile hashleyip kontrol ediyoruz.
            if (!HashingHelper.VerifyPasswordHash(entity.Password, appUser.PasswordHash, appUser.PasswordSalt))
            {
                appUser.IncorrectEntry ++; //Şifre yanlış girildiyse 1 atırıyoruz.

                if (appUser.IncorrectEntry == 3) appUser.IsLock = true; //3 kere yanlış girilen kulanıcıyı Lock ediyoruz.

                _userRepository.Update(appUser);
                throw new InvalidOperationException($"{typeof(AppUser).Name} User Password Does Not Match ");
            }

            return appUser;
        }

        public async Task<AppUser> RegisterAsync(AppUserRegisterRequiredDto requiredEntity, AppUserRegisterOptionalDto optionalEntity)
        {
            AppUser appUser = await GetByEmailAsync(requiredEntity.Email);
            //Girilen Email'i databasede varmı Kontrol ediyoruz.
            if (appUser.Email == requiredEntity.Email)
                throw new InvalidOperationException($"{typeof(AppUser).Name}({requiredEntity.Email}) Email Already Exists");

            //PasswordHash ve PasswordSalt oluşturuyoruz.
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(requiredEntity.Password, out passwordHash, out passwordSalt);

            appUser.UserName = requiredEntity.UserName;
            appUser.Email = requiredEntity.Email;
            appUser.PasswordHash = passwordHash;
            appUser.PasswordSalt= passwordSalt;
            appUser.LastActivty= DateTime.UtcNow;

            //AppUserRegisterOptionalDto ya girilen degerlri boşlukları silip kontrol ediyoruz. Girilen deger varsa appUser a atıyoruz.
            if (optionalEntity.FirstName.Trim() != null || optionalEntity.LastName.Trim() != null || optionalEntity.PhoneNumber.Trim() != null || optionalEntity.DateOfBirth != null)
            {
                appUser.PhoneNumber = optionalEntity.PhoneNumber;
                appUser.FirstName=optionalEntity.FirstName;
                appUser.LastName = optionalEntity.LastName;
                appUser.DateOfBirth = optionalEntity.DateOfBirth.Value;
            }

            try
            {
                _userRepository.Add(appUser);
            }
            catch (Exception)
            {
                throw new Exception($"Register_Error {typeof(AppUser).Name}");
            }
            return appUser;
        }

        public async Task UpdatePasswordAsync(int id, AppUserPasswordUpdateDto entity)
        {         
            AppUser appUser =await _userRepository.GetByIDAsync(id);
            if(appUser==null) //Kulanıcıyı kontrol edıyoruz
                throw new InvalidOperationException($"{typeof(AppUser).Name}({id}) User Not Found");

            //Kulanıcı gridigi Password'u Databaseden gelen PasswordHash ve PasswordSalt ile hashleyip kontrol ediyoruz.
            if (!HashingHelper.VerifyPasswordHash(entity.OldPassword, appUser.PasswordHash, appUser.PasswordSalt))
                throw new InvalidOperationException($"{typeof(AppUser).Name} User Password Does Not Match ");

            //Yeni PasswordHash ve PasswordSalt oluşturuyoruz.
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(entity.NewPassword, out passwordHash, out passwordSalt);

            appUser.PasswordHash = passwordHash;
            appUser.PasswordSalt = passwordSalt;

            _userRepository.Update(appUser); //Yeni Şifreyle kullanıyı güncelliyoruz.
        }
    }
}

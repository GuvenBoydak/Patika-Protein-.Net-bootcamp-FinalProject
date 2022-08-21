using Dapper;
using FinalProject.Entities;
using System.Data;

namespace FinalProject.DataAccess
{
    public class DpAppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public DpAppUserRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            AppUser deleteToAppUser = await GetByIDAsync(id);
            deleteToAppUser.DeletedDate = DateTime.UtcNow;
            deleteToAppUser.Status = DataStatus.Deleted;

            await UpdateAsync(deleteToAppUser);
        }

        /// <summary>
        /// ilgili Aktivasyon kodlu kullanıcı
        /// </summary>
        /// <param name="code">Aktivasyon kod bilgisi</param>
        public async Task<AppUser> GetByActivationCode(Guid code)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                return await con.QueryFirstOrDefaultAsync<AppUser>("select * from \"AppUsers\" where \"ActivationCode\" = @ActivationCode and \"Status\" != 2", new { ActivationCode = code });
            }
        }

        /// <summary>
        /// ilgili Email'li kullanıcı bilgisi
        /// </summary>
        /// <param name="email">kullanıcı Email bilgisi</param>
        public async Task<AppUser> GetByEmailAsync(string email)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                AppUser result = await con.QueryFirstOrDefaultAsync<AppUser>("select * from \"AppUsers\" where \"Email\" = @email and \"Status\" != 2", new {email=email});
                return result;
            }
        }



        public async Task UpdateAsync(AppUser appUser)
        {
            AppUser userToUpdate = await GetByIDAsync(appUser.ID);

            if(appUser.Status == DataStatus.Deleted)
            {
                userToUpdate.DeletedDate = appUser.DeletedDate;
                userToUpdate.Status = appUser.Status;
                CheckDefaultValues(userToUpdate,appUser);

                string query = "update \"AppUsers\" set \"UserName\" = @UserName,\"PasswordHash\"=@PasswordHash,\"PasswordSalt\" = @PasswordSalt,\"Email\" =@Email,\"IncorrectEntry\"=@IncorrectEntry,\"IsLock\"=@IsLock,\"EmailStatus\"=@EmailStatus,\"Active\"=@Active,\"FirstName\"=@FirstName,\"LastName\"=@LastName,\"LastActivty\"=@LastActivty,\"PhoneNumber\"=@PhoneNumber,\"DeletedDate\"=@DeletedDate,\"Status\"=@Status where \"ID\"=@ID ";
                _dbContext.Execute((con) =>
                {
                     con.Execute(query, userToUpdate);
                });
            }
            else
            {
                userToUpdate.UpdatedDate = DateTime.UtcNow;
                userToUpdate.Status = DataStatus.Updated;
                CheckDefaultValues(userToUpdate,appUser);

                string query = "update \"AppUsers\" set \"UserName\" = @UserName,\"PasswordHash\"=@PasswordHash,\"PasswordSalt\" = @PasswordSalt,\"Email\" =@Email,\"IncorrectEntry\"=@IncorrectEntry,\"IsLock\"=@IsLock,\"EmailStatus\"=@EmailStatus,\"Active\"=@Active,\"FirstName\"=@FirstName,\"LastName\"=@LastName,\"LastActivty\"=@LastActivty,\"PhoneNumber\"=@PhoneNumber,\"UpdatedDate\"=@UpdatedDate,\"Status\"=@Status where \"ID\"=@ID ";
                _dbContext.Execute( (con) =>
                {
                     con.Execute(query, userToUpdate);
                });
            }
        }

        //Parametreden gelen deger ie databasedeki degerleri karşılaştırıp degişiklige ugrayanları Güncelliyoruz.
        private void CheckDefaultValues(AppUser userToUpdate,AppUser appUser)
        {

            userToUpdate.UserName = appUser.UserName == default ? userToUpdate.UserName : appUser.UserName;
            userToUpdate.Email = appUser.Email == default ? userToUpdate.Email : appUser.Email;
            userToUpdate.PasswordHash = appUser.PasswordHash == default ? userToUpdate.PasswordHash : appUser.PasswordHash;
            userToUpdate.PasswordSalt = appUser.PasswordSalt == default ? userToUpdate.PasswordSalt : appUser.PasswordSalt;
            userToUpdate.IncorrectEntry = appUser.IncorrectEntry == default ? userToUpdate.IncorrectEntry : appUser.IncorrectEntry;
            userToUpdate.IsLock = appUser.IsLock;
            userToUpdate.EmailStatus = appUser.EmailStatus == default ? userToUpdate.EmailStatus : appUser.EmailStatus;
            userToUpdate.Active = appUser.Active == default ? userToUpdate.Active : appUser.Active;
            userToUpdate.FirstName = appUser.FirstName == default ? userToUpdate.FirstName : appUser.FirstName;
            userToUpdate.LastName = appUser.LastName == default ? userToUpdate.LastName : appUser.LastName;
            userToUpdate.LastActivty = appUser.LastActivty == default ? userToUpdate.LastActivty : appUser.LastActivty;
            userToUpdate.PhoneNumber = appUser.PhoneNumber == default ? userToUpdate.PhoneNumber : appUser.PhoneNumber;
        }
    }



}

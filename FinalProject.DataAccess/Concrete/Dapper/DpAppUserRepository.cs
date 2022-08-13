using Dapper;
using FinalProject.Base;
using System.Data;

namespace FinalProject.DataAccess
{
    public class DpAppUserRepository : GenericRepository<AppUser>, IAppUserRepository
    {
        public DpAppUserRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async Task<AppUser> GetByEmailAsync(string email)
        {
            string query = $"select * from \"AppUsers\" where \"Email\" == @email ";

            using (IDbConnection con = _dbContext.GetConnection())
            {
                AppUser result = await con.QueryFirstOrDefaultAsync<AppUser>(query, new {email=email});
                return result;
            }
        }
    }



}

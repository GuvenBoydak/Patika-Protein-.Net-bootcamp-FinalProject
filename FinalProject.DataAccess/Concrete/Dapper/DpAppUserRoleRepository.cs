using Dapper;
using FinalProject.Base;
using System.Data;

namespace FinalProject.DataAccess
{
    public class DpAppUserRoleRepository : GenericRepository<AppUserRole>, IAppUserRoleRepository
    {
        public DpAppUserRoleRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            AppUserRole appUserRole =await GetByIDAsync(id);
            appUserRole.DeletedDate = DateTime.Now;
            appUserRole.Status = DataStatus.Deleted;
            await UpdateAsync(appUserRole);
        }

        public async Task<List<AppUserRole>> GetAppUserID(int id)
        {
            using (IDbConnection con=_dbContext.GetConnection())
            {
                IEnumerable<AppUserRole> appUserRoles = await con.QueryAsync<AppUserRole>("Select * from \"AppUserRoles\" where \"AppUserID\" = @id", new { id = id });
                return appUserRoles.ToList();
            }
        }

        public async Task UpdateAsync(AppUserRole appUserRole)
        {
            AppUserRole updateAppUserRole = await GetByIDAsync(appUserRole.ID);

            if (appUserRole.Status == DataStatus.Deleted)
            {
                updateAppUserRole.Status = appUserRole.Status;
                updateAppUserRole.DeletedDate=appUserRole.DeletedDate;

                updateAppUserRole.AppUserID=appUserRole.AppUserID ==default ? updateAppUserRole.AppUserID : appUserRole.AppUserID;
                updateAppUserRole.RoleID=appUserRole.RoleID ==default ? updateAppUserRole.RoleID : appUserRole.RoleID;

                string query = "update \"AppUserRoles\" set \"AppUserID\"=@AppUserID, \"RoleID\"=@RoleID, \"DeletedDate\"=@DeletedDate, \"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute((con) =>
                {
                    con.Execute(query, updateAppUserRole);
                });
            }
            else 
            {
                updateAppUserRole.Status = appUserRole.Status;
                updateAppUserRole.UpdatedDate = appUserRole.UpdatedDate;

                updateAppUserRole.AppUserID = appUserRole.AppUserID == default ? updateAppUserRole.AppUserID : appUserRole.AppUserID;
                updateAppUserRole.RoleID = appUserRole.RoleID == default ? updateAppUserRole.RoleID : appUserRole.RoleID;

                string query = "update \"AppUserRoles\" set \"AppUserID\"=@AppUserID, \"RoleID\"=@RoleID, \"UpdatedDate\"=@UpdatedDate, \"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute((con) =>
                {
                    con.Execute(query, updateAppUserRole);
                });
            }
        }
    }
}

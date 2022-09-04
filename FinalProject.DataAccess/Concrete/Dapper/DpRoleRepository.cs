using Dapper;
using FinalProject.Base;

namespace FinalProject.DataAccess
{
    public class DpRoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public DpRoleRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            Role role = await GetByIDAsync(id);
            role.DeletedDate = DateTime.Now;
            role.Status = DataStatus.Deleted;
            await UpdateAsync(role);
        }

        public async Task UpdateAsync(Role role)
        {
            Role updateRole = await GetByIDAsync(role.ID);

            if (role.Status == DataStatus.Deleted)
            {
                updateRole.Status = role.Status;
                updateRole.DeletedDate = role.DeletedDate;

                updateRole.Name = role.Name == default ? updateRole.Name : role.Name;

                string query = "update \"Roles\" set \"Name\"=@Name, \"DeletedDate\"=@DeletedDate, \"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute((con) =>
                {
                    con.Execute(query, updateRole);
                });
            }
            else if (role.Status == DataStatus.Updated)
            {
                updateRole.Status = role.Status;
                updateRole.UpdatedDate = role.UpdatedDate;

                updateRole.Name = role.Name == default ? updateRole.Name : role.Name;

                string query = "update \"Roles\" set \"Name\"=@Name, \"UpdatedDate\"=@UpdatedDate, \"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute((con) =>
                {
                    con.Execute(query, updateRole);
                });
            }
        }
    }
}

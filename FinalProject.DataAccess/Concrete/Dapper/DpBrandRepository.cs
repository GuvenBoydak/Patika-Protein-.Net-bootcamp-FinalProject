using Dapper;
using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpBrandRepository : GenericRepository<Brand>, IBrandRepository
    {
        public DpBrandRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            Brand deletedEntity = await GetByIDAsync(id);
            deletedEntity.DeletedDate = DateTime.UtcNow;
            deletedEntity.Status = DataStatus.Deleted;

            await UpdateAsync(deletedEntity);
        }

        public async Task UpdateAsync(Brand entity)
        {
            Brand brandToUpdate = await GetByIDAsync(entity.ID);

            if(entity.Status == DataStatus.Deleted)
            {
                brandToUpdate.Status = entity.Status;
                brandToUpdate.DeletedDate= entity.DeletedDate;
                brandToUpdate.Name = entity.Name == default ? brandToUpdate.Name : entity.Name;

                string query = "update \"Brands\" set \"Name\"=@Name, \"DeletedDate\"=@DeletedDate, \"Status\"=@Status where \"ID\"=@ID";
                _dbContext.Execute(async (con) =>
                {
                    await con.ExecuteAsync(query, entity);
                });
            }
            else
            {
                brandToUpdate.Status = entity.Status;
                brandToUpdate.UpdatedDate = entity.UpdatedDate;
                brandToUpdate.Name = entity.Name == default ? brandToUpdate.Name : entity.Name;

                string query = "update \"Brands\" set \"Name\"=@Name, \"UpdatedDate\"=@UpdatedDate, \"Status\"=@Status where \"ID\"=@ID";
                _dbContext.Execute((con) =>
                {
                    con.Execute(query, entity);
                });
            }
        }
    }



}

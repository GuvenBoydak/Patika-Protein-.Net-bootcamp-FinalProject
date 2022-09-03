using Dapper;
using FinalProject.Entities;
using FinalProject.Base;


namespace FinalProject.DataAccess
{
    public class DpColorRepository : GenericRepository<Color>, IColorRepository
    {
        public DpColorRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            Color deleteToColor = await GetByIDAsync(id);
            deleteToColor.DeletedDate = DateTime.UtcNow;
            deleteToColor.Status = DataStatus.Deleted;

           await UpdateAsync(deleteToColor);
        }

        public async Task UpdateAsync(Color entity)
        {
            Color colorToUpdate = await GetByIDAsync(entity.ID);

            if (entity.Status == DataStatus.Deleted)
            {
                colorToUpdate.Status = entity.Status;
                colorToUpdate.DeletedDate = entity.DeletedDate;
                colorToUpdate.Name = entity.Name == default ? colorToUpdate.Name : entity.Name;

                string query = "update \"Colors\" set \"Name\"=@Name, \"DeletedDate\"=@DeletedDate, \"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute( (con) =>
                {
                     con.Execute(query, entity);
                });
            }
            else
            {
                colorToUpdate.Status = entity.Status;
                colorToUpdate.UpdatedDate = entity.UpdatedDate;
                colorToUpdate.Name = entity.Name == default ? colorToUpdate.Name : entity.Name;

                string query = "update \"Colors\" set \"Name\"=@Name, \"UpdatedDate\"=@UpdatedDate, \"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute( (con) =>
                {
                     con.Execute(query, entity);
                });
            }
        }
    }
}

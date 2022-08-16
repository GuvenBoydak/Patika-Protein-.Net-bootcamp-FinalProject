using Dapper;
using FinalProject.Entities;
using System.Data;

namespace FinalProject.DataAccess
{
    public class DpOfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        public DpOfferRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            Offer deleteToOffer=await GetByIDAsync(id);
            deleteToOffer.DeletedDate = DateTime.UtcNow;
            deleteToOffer.Status = DataStatus.Deleted;

            await UpdateAsync(deleteToOffer);
        }

        public async Task<List<Offer>> GetByAppUserIDAsync(int id)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<Offer> result = await con.QueryAsync<Offer>("select * from \"Offers\" where \"AppUserID\" = @id", new { id = id });
                return result.ToList();
            }
        }

        public async Task UpdateAsync(Offer entity)
        {
            Offer updateToOffer = await GetByIDAsync(entity.ID);
            if (entity.Status == DataStatus.Deleted)
            {
                updateToOffer.Status = entity.Status;
                updateToOffer.DeletedDate = entity.DeletedDate;
                updateToOffer.Price = entity.Price == default ? entity.Price : updateToOffer.Price;
                updateToOffer.IsApproved = entity.IsApproved == default ? entity.IsApproved : updateToOffer.IsApproved;
                updateToOffer.AppUserID = entity.AppUserID == default ? entity.AppUserID : updateToOffer.AppUserID;
                updateToOffer.ProductID = entity.ProductID == default ? entity.ProductID : updateToOffer.ProductID;


                string query = "update \"Offers\" set \"Price\"=@Price,\"IsApproved\"=@IsApproved,\"AppUserID\"=@AppUSerID,\"DeletedDate\"=@DeletedDate,\"Status\"=@Status where \"ID\"=@ID";
                _dbContext.Execute(async (con) =>
                {
                    await con.ExecuteAsync(query, updateToOffer);
                });
            }
            else
            {
                updateToOffer.Status = DataStatus.Updated;
                updateToOffer.UpdatedDate = DateTime.UtcNow;
                updateToOffer.Price = entity.Price == default ? entity.Price : updateToOffer.Price;
                updateToOffer.IsApproved = entity.IsApproved == default ? entity.IsApproved : updateToOffer.IsApproved;
                updateToOffer.AppUserID = entity.AppUserID == default ? entity.AppUserID : updateToOffer.AppUserID;
                updateToOffer.ProductID = entity.ProductID == default ? entity.ProductID : updateToOffer.ProductID;

                string query = "update \"Offers\" set \"Price\"=@Price,\"IsApproved\"=@IsApproved,\"AppUserID\"=@AppUSerID,\"UpdatedDate\"=@UpdatedDate,\"Status\"=@Status where \"ID\"=@ID";
                _dbContext.Execute(async (con) =>
                {
                    await con.ExecuteAsync(query, updateToOffer);
                });
            }
        }
    }
}

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

        public async Task DeleteAsync(int id)
        {
            Offer deleteToOffer=await GetByIDAsync(id);
            deleteToOffer.DeletedDate = DateTime.UtcNow;
            deleteToOffer.Status = DataStatus.Deleted;

            await UpdateAsync(deleteToOffer);
        }
        /// <summary>
        /// İlgili id li kulanıcının teklifleri
        /// </summary>
        /// <param name="id">kulanıcı id bilgisi</param>
        public async Task<List<Offer>> GetByAppUserIDAsync(int id)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<Offer> result = await con.QueryAsync<Offer>("select * from \"Offers\" where \"AppUserID\" = @id and \"Offers\".\"Status\" != 2", new { id = id });
                return result.ToList();
            }
        }

        /// <summary>
        /// İlgli id li ürünlerin teklifleri
        /// </summary>
        /// <param name="id">ürün id bilgisi</param>
        public async Task<List<Offer>> GetByOffersProductIDAsync(int id)
        {
            using (IDbConnection con=_dbContext.GetConnection())
            {
                IEnumerable<Offer> offers = await con.QueryAsync<Offer>("select * from  \"Offers\" where \"ProductID\" = @id and \"Offers\".\"Status\" != 2", new { id = id });
                return offers.ToList();
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


                string query = "update \"Offers\" set \"Price\"=@Price,\"IsApproved\"=@IsApproved,\"AppUserID\"=@AppUserID,\"DeletedDate\"=@DeletedDate,\"Status\"=@Status where \"ID\"=@ID";
                _dbContext.Execute(async (con) =>
                {
                    await con.ExecuteAsync(query, updateToOffer);
                });
            }
            else
            {
                updateToOffer.Status = DataStatus.Updated;
                updateToOffer.UpdatedDate = DateTime.UtcNow;
                updateToOffer.Price = entity.Price == default ? updateToOffer.Price : entity.Price;
                updateToOffer.IsApproved = entity.IsApproved == default ? updateToOffer.IsApproved : entity.IsApproved;
                updateToOffer.AppUserID = entity.AppUserID == default ? updateToOffer.AppUserID : entity.AppUserID;
                updateToOffer.ProductID = entity.ProductID == default ? updateToOffer.ProductID : entity.ProductID;

                string query = "update \"Offers\" set \"Price\"=@Price,\"IsApproved\"=@IsApproved,\"AppUserID\"=@AppUSerID,\"UpdatedDate\"=@UpdatedDate,\"Status\"=@Status where \"ID\"=@ID";
                _dbContext.Execute(async (con) =>
                {
                    await con.ExecuteAsync(query, updateToOffer);
                });
            }
        }
    }
}

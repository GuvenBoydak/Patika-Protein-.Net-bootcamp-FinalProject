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
        ///  Girilen kullanıcı Id'sinin yaptıgı teklifler
        /// </summary>
        /// <param name="id">kulanıcı id bilgisi</param>
        public async Task<List<Offer>> GetByAppUserOffersAsync(int id)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<Offer> result = await con.QueryAsync<Offer>("select * from \"Offers\" where \"AppUserID\" = @id and \"Status\" != 2", new { id = id });
                return result.ToList();
            }
        }

        /// <summary>
        /// Girilen Ürün Id'sine yapılan teklifler 
        /// </summary>
        /// <param name="id">ürün id bilgisi</param>
        public async Task<List<Offer>> GetByOffersProductIDAsync(int id)
        {
            using (IDbConnection con=_dbContext.GetConnection())
            {
                IEnumerable<Offer> offers = await con.QueryAsync<Offer>("select * from  \"Offers\" where \"ProductID\" = @id and \"Status\" != 2", new { id = id });
                return offers.ToList();
            }
        }

        /// <summary>
        /// Girilen ID'li kullanıcının ürünlerine gelen teklifler
        /// </summary>
        /// <param name="id">kullanıcı id bilgisi</param>
        public async Task<List<Product>> GetByAppUserProductsOffers(int id)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<Product> result = await con.QueryAsync<Product>("select * from \"Products\" where \"AppUserID\"=@id and \"Status\" != 2", new { id = id });
                foreach (Product item in result)
                {
                    item.Offers = con.Query<Offer>("Select * from \"Offers\" where \"ProductID\"=@id and \"Status\" != 2", new { id = item.ID }).ToList();
                }

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


                string query = "update \"Offers\" set \"Price\"=@Price,\"IsApproved\"=@IsApproved,\"AppUserID\"=@AppUserID,\"DeletedDate\"=@DeletedDate,\"Status\"=@Status where \"ID\"=@ID";
                _dbContext.Execute( (con) =>
                {
                     con.Execute(query, updateToOffer);
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
                _dbContext.Execute( (con) =>
                {
                     con.Execute(query, updateToOffer);
                });
            }
        }
    }
}

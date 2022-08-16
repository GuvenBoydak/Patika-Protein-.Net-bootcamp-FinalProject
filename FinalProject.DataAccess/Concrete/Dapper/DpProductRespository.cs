using Dapper;
using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpProductRespository : GenericRepository<Product>, IProductRepository
    {
        public DpProductRespository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            Product deleteToProduct = await GetByIDAsync(id);
            deleteToProduct.DeletedDate = DateTime.UtcNow;
            deleteToProduct.Status = DataStatus.Deleted;

            await UpdateAsync(deleteToProduct);
        }

        public async Task UpdateAsync(Product entity)
        {
            Product updateToProduct = await GetByIDAsync(entity.ID);

            if(entity.Status==DataStatus.Deleted)
            {
                updateToProduct.Status = entity.Status;
                updateToProduct.DeletedDate = entity.DeletedDate;
                CheckDefaultValues(updateToProduct, entity);

                string query = "update \"Products\" set \"Name\"=@Name, \"UnitInStock\"=@UnitInStock, \"UnitPrice\"=@UnitPrice, \"ImageUrl\"=@ImageUrl, \"Description\"=@Description, \"IsOfferable\"=@IsOfferable, \"IsSold\"=@IsSold, \"UsageStatus\"=@UsageStatus, \"CategoryID\"=@CategoryID, \"BrandID\"=@BrandID, \"ColorID\"=@ColorID, \"OfferID\"=@OfferID, \"AppUserID\"=@AppUserID, \"DeletedDate\"=@DeletedDate, \"Status\"=@Status where  \"ID\"=@ID";

                _dbContext.Execute(async (con) =>
                {
                    await con.ExecuteAsync(query, updateToProduct);
                });
            }
            else
            {
                updateToProduct.Status = DataStatus.Updated;
                updateToProduct.UpdatedDate = DateTime.UtcNow;
                CheckDefaultValues(updateToProduct, entity);

                string query = "update \"Products\" set \"Name\"=@Name, \"UnitInStock\"=@UnitInStock, \"UnitPrice\"=@UnitPrice, \"ImageUrl\"=@ImageUrl, \"Description\"=@Description, \"IsOfferable\"=@IsOfferable, \"IsSold\"=@IsSold, \"UsageStatus\"=@UsageStatus, \"CategoryID\"=@CategoryID, \"BrandID\"=@BrandID, \"ColorID\"=@ColorID, \"OfferID\"=@OfferID, \"AppUserID\"=@AppUserID, \"UpdatedDate\"=@UpdatedDate, \"Status\"=@Status where  \"ID\"=@ID";

                _dbContext.Execute(async (con) =>
                {
                    await con.ExecuteAsync(query, updateToProduct);
                });
            }
        }

        //Parametreden gelen deger ie databasedeki degerleri karşılaştırıp degişiklige ugrayanları Güncelliyoruz.
        private void CheckDefaultValues(Product updateToProduct, Product product)
        {
            updateToProduct.Name = product.Name == default ? product.Name : updateToProduct.Name;
            updateToProduct.UnitsInStock = product.UnitsInStock == default ? product.UnitsInStock : updateToProduct.UnitsInStock;
            updateToProduct.UnitPrice = product.UnitPrice == default ? product.UnitPrice : updateToProduct.UnitPrice;
            updateToProduct.ImageUrl = product.ImageUrl == default ? product.ImageUrl : updateToProduct.ImageUrl;
            updateToProduct.Description = product.Description == default ? product.Description : updateToProduct.Description;
            updateToProduct.IsOfferable = product.IsOfferable == default ? product.IsOfferable : updateToProduct.IsOfferable;
            updateToProduct.IsSold = product.IsSold == default ? product.IsSold : updateToProduct.IsSold;
            updateToProduct.UsageStatus = product.UsageStatus == default ? product.UsageStatus : updateToProduct.UsageStatus;
            updateToProduct.CategoryID = product.CategoryID == default ? product.CategoryID : updateToProduct.CategoryID;
            updateToProduct.BrandID = product.BrandID == default ? product.BrandID : updateToProduct.BrandID;
            updateToProduct.ColorID = product.ColorID == default ? product.ColorID : updateToProduct.ColorID;
            updateToProduct.OfferID = product.OfferID == default ? product.OfferID : updateToProduct.OfferID;
            updateToProduct.AppUserID = product.AppUserID == default ? product.AppUserID : updateToProduct.AppUserID;
        }
    }
}

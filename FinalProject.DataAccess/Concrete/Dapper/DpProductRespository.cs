﻿using Dapper;
using FinalProject.Entities;
using FinalProject.Base;
using System.Data;

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

        public async Task<List<Product>> GetByAppUserProductsWithOffers(int id)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<Product> result = await con.QueryAsync<Product>("select * from \"Products\"  where \"Products\".\"AppUserID\"=@id and \"Products\".\"Status\" != 2", new { id = id });
                foreach (Product item in result)
                {
                    item.Offers = con.Query<Offer>("select * from \"Offers\" inner join \"Products\" on \"Offers\".\"ProductID\"= \"Products\".\"ID\"  where \"Products\".\"ID\"=@id and \"Offers\".\"Status\" != 2", new { id = item.ID }).ToList();
                }

                return result.ToList();
            }
        }

        public async Task<List<Product>> GetByProductsPaginationAsync(int limit, int page)
        {
            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<Product> products = await con.QueryAsync<Product>("select * from \"Products\" where \"Status\" != '2' order by \"ID\"  limit  @limit offset  @page  ",
                    new
                    {
                        limit = limit,
                        page = (page - 1) * limit
                    });

                return products.ToList();
            }
        }

        public async Task UpdateAsync(Product entity)
        {
            Product updateToProduct = await GetByIDAsync(entity.ID);

            if(entity.Status==DataStatus.Deleted)
            {
                updateToProduct.Status = entity.Status;
                updateToProduct.DeletedDate = entity.DeletedDate;
                CheckDefaultValues(updateToProduct, entity);

                string query = "update \"Products\" set \"Name\"=@Name, \"UnitPrice\"=@UnitPrice, \"ImageUrl\"=@ImageUrl, \"Description\"=@Description, \"IsOfferable\"=@IsOfferable, \"IsSold\"=@IsSold, \"UsageStatus\"=@UsageStatus, \"CategoryID\"=@CategoryID, \"BrandID\"=@BrandID, \"ColorID\"=@ColorID, \"AppUserID\"=@AppUserID, \"DeletedDate\"=@DeletedDate, \"Status\"=@Status where  \"ID\"=@ID";

                _dbContext.Execute((con) =>
                {
                    con.ExecuteAsync(query, updateToProduct);
                });
            }
            else
            {
                updateToProduct.Status = DataStatus.Updated;
                updateToProduct.UpdatedDate = DateTime.UtcNow;
                CheckDefaultValues(updateToProduct, entity);

                string query = "update \"Products\" set \"Name\"= @Name, \"UnitPrice\"= @UnitPrice, \"ImageUrl\"= @ImageUrl, \"Description\"= @Description, \"IsOfferable\"= @IsOfferable, \"IsSold\"= @IsSold, \"UsageStatus\"= @UsageStatus, \"CategoryID\"= @CategoryID, \"BrandID\"= @BrandID, \"ColorID\"= @ColorID, \"AppUserID\"= @AppUserID, \"UpdatedDate\"= @UpdatedDate, \"Status\"= @Status where  \"ID\"= @ID";

                _dbContext.Execute( (con) =>
                {
                     con.Execute(query, updateToProduct);
                });
            }
        }

        //Parametreden gelen deger ie databasedeki degerleri karşılaştırıp degişiklige ugrayanları Güncelliyoruz.
        private void CheckDefaultValues(Product updateToProduct, Product product)
        {
            updateToProduct.Name = product.Name == default ? updateToProduct.Name : product.Name;
            updateToProduct.UnitPrice = product.UnitPrice == default ? updateToProduct.UnitPrice : product.UnitPrice;
            updateToProduct.ImageUrl = product.ImageUrl == default ? updateToProduct.ImageUrl : product.ImageUrl;
            updateToProduct.Description = product.Description == default ? updateToProduct.Description : product.Description;
            updateToProduct.IsOfferable = product.IsOfferable == true ? updateToProduct.IsOfferable : product.IsOfferable;
            updateToProduct.IsSold = product.IsSold == default ? updateToProduct.IsSold : product.IsSold;
            updateToProduct.UsageStatus = product.UsageStatus == default ? updateToProduct.UsageStatus : product.UsageStatus;
            updateToProduct.CategoryID = product.CategoryID == default ? updateToProduct.CategoryID : product.CategoryID;
            updateToProduct.BrandID = product.BrandID == default ? updateToProduct.BrandID : product.BrandID;
            updateToProduct.ColorID = product.ColorID == default ? updateToProduct.ColorID : product.ColorID;       
            updateToProduct.AppUserID = product.AppUserID == default ? updateToProduct.AppUserID : product.AppUserID;
        }
    }
}

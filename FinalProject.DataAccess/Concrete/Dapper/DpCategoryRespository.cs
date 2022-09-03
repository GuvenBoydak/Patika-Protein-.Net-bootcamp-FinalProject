﻿using Dapper;
using FinalProject.Entities;
using System.Collections.Generic;
using System.Data;
using FinalProject.Base;


namespace FinalProject.DataAccess
{
    public class DpCategoryRespository : GenericRepository<Category>, ICategoryRepository
    {
        public DpCategoryRespository(IDapperContext dapperContext) : base(dapperContext)
        {
        }

        public async void Delete(int id)
        {
            Category deletedCategory = await GetByIDAsync(id);
            deletedCategory.DeletedDate = DateTime.UtcNow;
            deletedCategory.Status = DataStatus.Deleted;

            await UpdateAsync(deletedCategory);
        }

        /// <summary>
        /// ilgili kategorideki ürünlerin listesi
        /// </summary>
        /// <param name="id">kategori id bilgisi</param>
        public async Task<Category> GetCategoryWithProductsAsync(int id)
        {
            using (IDbConnection con=_dbContext.GetConnection())
            {
                //id ye karşılık gelen category alıyoruz.
                Category result = await con.QueryFirstOrDefaultAsync<Category>("select * from \"Categories\" where \"ID\" =@id and \"Status\" != 2", new { id = id });   
                
                if(result!=null)//categorynin productlarına categoryId'si ID ile eşlesenleri alıyoruz.
                    result.Products =  con.Query<Product>("select * from \"Products\" where \"CategoryID\" =@id and \"Status\" != 2", new {id=id}).ToList();

                return result;
            }
        }

        public async Task UpdateAsync(Category entity)
        {
            Category updateToCategory = await GetByIDAsync(entity.ID);
            if (entity.Status == DataStatus.Deleted)
            {
                updateToCategory.Status = entity.Status;
                updateToCategory.DeletedDate = entity.DeletedDate;
                updateToCategory.Name = entity.Name == default ? updateToCategory.Name : entity.Name;
                updateToCategory.Description = entity.Description == default ? updateToCategory.Description : entity.Description;

                string query = "update \"Categories\" set \"Name\"=@Name,\"Description\"=@Description,\"DeletedDate\"=@DeletedDate,\"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute((con) =>
                {
                     con.Execute(query, updateToCategory);
                });
            }
            else
            {
                updateToCategory.Status = DataStatus.Updated;
                updateToCategory.UpdatedDate = DateTime.UtcNow;
                updateToCategory.Name = entity.Name == default ? updateToCategory.Name : entity.Name;
                updateToCategory.Description = entity.Description == default ? updateToCategory.Description : entity.Description;

                string query = "update \"Categories\" set \"Name\"=@Name,\"Description\"=@Description,\"UpdatedDate\"=@UpdatedDate,\"Status\"=@Status where \"ID\"=@ID";

                _dbContext.Execute( (con) =>
                {
                    con.Execute(query, updateToCategory);
                });
            }
        }
    }
}
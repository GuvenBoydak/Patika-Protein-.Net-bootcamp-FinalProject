﻿using Dapper;
using FinalProject.Entities;
using System.Data;
using FinalProject.Base;


namespace FinalProject.DataAccess
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected IDapperContext _dbContext;
        private string _tableName;

        public GenericRepository(IDapperContext dapperContext)
        {
            _dbContext = dapperContext;
            //Reflection ile T nin tipini alıp stringe ceviriyoruz.
            _tableName = typeof(T).Name.ToString();
        }

        //Girilen arguman eger Category ise substring ile y atıp sonuna ies ekliyoruz, degilse sonuna sadece s ekliyoruz ve bir tablo ismi oluşturuyoruz.
        private string GetTableName(string entity)
        {
            if (entity == "Category" || entity == "Categor")
            {
                _tableName = _tableName.Substring(0, 7);
                return $"{_tableName}ies";
            }
            else
                return $"{_tableName}s";
        }

        //Reflection sayesinde çalişma zamanında T nin propertylerine erişip bir IEnumerable<string> dödürüyoruz. 
        private IEnumerable<string> GetColumns()
        {
            return typeof(T)
                    .GetProperties()
                    .Where(e => e.Name != "ID"
                     && !Attribute.IsDefined(e, typeof(DapperIgnoreAttribute))
                )
                .Select(e => e.Name);
        }

        public void Add(T entity)
        {
            IEnumerable<string> columns = GetColumns();

            //columns'u başına ve sonuna \"  koyarak ve string.join ile aralarına , ayırıp bir string oluşturduk.
            string stringOfColumns = "\"" + string.Join("\"" + "," + "\"", columns) + "\"";

            //columns.Select ile her degere başına @ koyup property ile birleştirdik. @ ile birleştirdigimiz degerleri string.join ile aralarına , koyarak ayırıdık ve bir string oluşturduk.
            string stringOfParameters = string.Join(", ", columns.Select(e => "@" + e));

            //_tableName i GetTableName metoduna arguman olarak veriyoruz ve çogul bir tablo ismi oluşturuyoruz. string.join ile ayrıdıgımız propertyleri kolon kısmına stringOfColumns olarak ve bunlara karşılık başına @ ekledigimiz propertyleri stringOfParameters olarak verdik.
            string query = $"insert into \"{GetTableName(_tableName)}\" ({stringOfColumns}) values ({stringOfParameters})";

            //Execute metoduna arguman olarak Action<NpgsqlConnection> isteyen metoda NpgsqlConnection tipinde con baglantı nesnesini veriyoruz. 
            _dbContext.Execute( (con) =>
            {
               con.Execute(query, entity);
            });
        }

        public async Task<List<T>> GetActiveAsync()
        {
            string query = $"select * from \"{GetTableName(_tableName)}\" where \"Status\" != '2' ";

            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<T> result = await con.QueryAsync<T>(query);
                return result.ToList();
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            string query = $"select * from  \"{GetTableName(_tableName)}\"";

            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<T> result = await con.QueryAsync<T>(query);
                return result.ToList();
            }
        }

        public async Task<T> GetByIDAsync(int id)
        {
            string query = $"select * from \"{GetTableName(_tableName)}\" where \"ID\" = @id and \"Status\" != 2 ";

            using (IDbConnection con = _dbContext.GetConnection())
            {
                return await con.QueryFirstOrDefaultAsync<T>(query, new { id = id });
            }
        }

        public async Task<List<T>> GetPassiveAsync()
        {
            //Silinmiş veriler
            string query = $"select * from \"{GetTableName(_tableName)}\" where \"Status\" = '2' ";

            using (IDbConnection con = _dbContext.GetConnection())
            {
                IEnumerable<T> result = await con.QueryAsync<T>(query);
                return result.ToList();
            }
        }
    }
}




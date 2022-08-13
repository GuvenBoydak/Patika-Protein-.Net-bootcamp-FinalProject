using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<List<T>> GetAllAsync();

        Task<List<T>> GetActiveAsync();

        Task<List<T>> GetPassiveAsync();

        Task<T> GetByIDAsync(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(int id);
  
    }
}

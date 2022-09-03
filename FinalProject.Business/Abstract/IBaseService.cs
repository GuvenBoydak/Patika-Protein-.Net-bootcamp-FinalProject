using FinalProject.Base;


namespace FinalProject.Business
{
    public interface IBaseService<T> where T : BaseEntity
    { 
        Task<List<T>> GetAllAsync();

        Task<List<T>> GetActiveAsync();

        Task<List<T>> GetPassiveAsync();

        Task<T> GetByIDAsync(int id);

        void Add(T entity);
    }

}

using FinalProject.Base;
using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class GenericService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;


        public GenericService(IRepository<T> repository)
        {
            _repository = repository;

        }

        public void Add(T entity)
        {
            try
            {
                _repository.Add(entity);
            }
            catch (Exception e)
            {
                throw new Exception($"Save_Error {typeof(T).Name}  => {e.Message}");
            }      
        }


        public async Task<List<T>> GetActiveAsync()
        {
            try
            {
                return await _repository.GetActiveAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"GetActive_Error {typeof(T).Name}  => {e.Message}");
            } 
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                return await _repository.GetAllAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"GetAll_Error {typeof(T).Name}  => {e.Message}");
            }

        }

        public async Task<T> GetByIDAsync(int id)
        {
            try
            {
                return await _repository.GetByIDAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception($"GetByID_Error {typeof(T).Name}  => {e.Message}");
            }
        }

        public async Task<List<T>> GetPassiveAsync()
        {
            try
            {
                return await _repository.GetPassiveAsync();
            }
            catch (Exception e)
            {
                throw new Exception($"GetPassive_Error {typeof(T).Name}  => {e.Message}");
            }
        }
    }
}

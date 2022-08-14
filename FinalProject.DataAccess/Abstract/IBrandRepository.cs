using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IBrandRepository : IRepository<Brand>
    {
        void Delete(int id);

        Task UpdateAsync(Brand entity);
    }
}

using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IColorRepository : IRepository<Color>
    {
        void Delete(int id);

        Task UpdateAsync(Color entity);
    }
}

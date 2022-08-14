using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface IColorService : IBaseService<Color>
    {
        Task UpdateAsync(Color entity);

        void Delete(int id);
    }

}

using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface IBrandService : IBaseService<Brand>
    {
        Task UpdateAsync(Brand entity);

        void Delete(int id);
    }

}

using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class BrandService : GenericService<Brand>, IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository) : base(brandRepository)
        {
            _brandRepository = brandRepository;
        }

        public void Delete(int id)
        {
            try
            {
                _brandRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error  =>  {e.Message}");
            }
        }

        public async Task UpdateAsync(Brand entity)
        {
            try
            {
               await _brandRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error  =>  {e.Message}");
            }
        }
    }
}

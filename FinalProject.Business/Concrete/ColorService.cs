using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class ColorService : GenericRepository<Color>, IColorRepository
    {
        private readonly IColorRepository _colorRepository;

        public ColorService(IDapperContext dapperContext, IColorRepository colorRepository) : base(dapperContext)
        {
            _colorRepository = colorRepository;
        }

        public void Delete(int id)
        {
            try
            {
                _colorRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error {typeof(Color).Name} =>  {e.Message}");
            }
        }

        public async Task UpdateAsync(Color entity)
        {
            try
            {
                await _colorRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error {typeof(Color).Name} =>  {e.Message}");
            }
        }
    }
}

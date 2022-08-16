using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class OfferService : GenericRepository<Offer>, IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IDapperContext dapperContext, IOfferRepository offerRepository) : base(dapperContext)
        {
            _offerRepository = offerRepository;
        }

        public void Delete(int id)
        {
            try
            {
                _offerRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error {typeof(Offer).Name} =>  {e.Message}");
            }
        }

        public async Task<List<Offer>> GetByAppUserIDAsync(int id)
        {
            try
            {
              return  await _offerRepository.GetByAppUserIDAsync(id);
            }
            catch (Exception e)
            {

                throw new Exception($"GetByAppUserID_Error {typeof(Offer).Name} =>  {e.Message}");
            }
        }

        public async Task UpdateAsync(Offer entity)
        {
            try
            {
                await _offerRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error {typeof(Offer).Name} =>  {e.Message}");
            }
        }
    }
}

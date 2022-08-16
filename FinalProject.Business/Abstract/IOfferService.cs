using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface IOfferService : IBaseService<Offer>
    {
        Task UpdateAsync(Offer entity);

        void Delete(int id);

        Task<List<Offer>> GetByAppUserIDAsync(int id);
    }

}

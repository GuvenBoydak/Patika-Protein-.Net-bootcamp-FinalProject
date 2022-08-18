using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Task DeleteAsync(int id);

        Task UpdateAsync(Offer entity);

        Task<List<Offer>> GetByAppUserIDAsync(int id);

        Task<List<Offer>> GetByOffersProductIDAsync(int id);
    }
}

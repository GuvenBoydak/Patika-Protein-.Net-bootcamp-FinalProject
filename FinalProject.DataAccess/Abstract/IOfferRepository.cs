using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IOfferRepository : IRepository<Offer>
    {
        Task DeleteAsync(int id);

        Task UpdateAsync(Offer entity);

        Task<List<Offer>> GetByAppUserOffersAsync(int id);

        Task<List<Offer>> GetByOffersProductIDAsync(int id);

        Task<List<Product>> GetByAppUserProductsOffers(int id);
    }
}

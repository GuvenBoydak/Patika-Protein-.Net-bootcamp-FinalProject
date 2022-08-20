using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface IOfferService : IBaseService<Offer>
    {
        Task UpdateAsync(Offer entity);

        Task DeleteAsync(int id);

        Task<List<Offer>> GetByAppUserOffersAsync(int id);

        Task OfferApprovalAsync(Offer offer);

        Task BuyProductAsync(Offer offer);

        Task<List<Product>> GetByAppUserProductsOffersAsync(int id);

        Task<List<Offer>> GetByOffersProductIDAsync(int id);
    }

}

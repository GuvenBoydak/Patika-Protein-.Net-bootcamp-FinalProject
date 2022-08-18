using FinalProject.Entities;

namespace FinalProject.Business
{
    public interface IOfferService : IBaseService<Offer>
    {
        Task UpdateAsync(Offer entity);

        Task DeleteAsync(int id);

        Task<List<Offer>> GetByAppUserIDAsync(int id);

        Task OfferApproval(Offer offer);

        Task BuyProduct(Offer offer);
    }

}

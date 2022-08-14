using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public interface IOfferRepository : IRepository<Offer>
    {
        void Delete(int id);

        Task UpdateAsync(Offer entity);
    }
}

using FinalProject.Entities;

namespace FinalProject.DataAccess
{
    public class DpOfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        public DpOfferRepository(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }



}

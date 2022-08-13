using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class OfferService : GenericRepository<Offer>, IOfferService
    {
        public OfferService(IDapperContext dapperContext) : base(dapperContext)
        {
        }
    }
}

using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.MVCUI
{
    public class OfferVM
    {
        public List<Offer> Offers { get; set; }

        public Offer Offer { get; set; }

        public List<Product> Products { get; set; }

        public List<AppUser> AppUsers { get; set; }

    }
}

using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.MVCUI
{
    public class OfferVM
    {
        public List<OfferListDto> OfferListDtos { get; set; }

        public Offer Offer { get; set; }

        public OfferAddDto OfferAddDto { get; set; }

        public OfferUpdateDto OfferUpdateDto { get; set; }

        public List<ProductListDto> ProductListDtos { get; set; }

        public List<AppUserListDto> AppUserListDtos { get; set; }

    }
}

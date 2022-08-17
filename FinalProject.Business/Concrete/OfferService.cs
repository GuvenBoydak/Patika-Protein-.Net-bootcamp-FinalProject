using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class OfferService : GenericRepository<Offer>, IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IProductService _productService;

        public OfferService(IDapperContext dapperContext, IOfferRepository offerRepository, IProductService productService) : base(dapperContext)
        {
            _offerRepository = offerRepository;
            _productService = productService;
        }

        public async Task BuyProduct(Offer offer)//Ürün Satın alma 
        {
            Product product = await _productService.GetByIDAsync(offer.ProductID);
            product.UnitPrice = offer.Price;
            product.IsSold=true;
            product.IsOfferable = false;
            product.AppUserID=offer.AppUserID;
            await _productService.UpdateAsync(product);

            List<Offer> offers = await _offerRepository.GetByOffersProductIDAsync(offer.ProductID);//İlgili üründeki tüm Offerları siliyoruz.
            foreach (Offer item in offers)
            {
                Delete(item.ID);
            }
        }

        public void Delete(int id)
        {
            try
            {
                _offerRepository.Delete(id);
            }
            catch (Exception e)
            {

                throw new Exception($"Delete_Error  =>  {e.Message}");
            }
        }

        public async Task<List<Offer>> GetByAppUserIDAsync(int id)
        {
            try
            {
              return  await _offerRepository.GetByAppUserIDAsync(id);
            }
            catch (Exception e)
            {

                throw new Exception($"GetByAppUserID_Error  =>  {e.Message}");
            }
        }

        //Teklif Onaylandıysa ilgili Ürünün bilgilerini güncelliyoruz.
        public async Task OfferApproval(Offer offer)
        {
            if (offer.IsApproved == true)
                await UpdateAsync(offer);

          Product product=  await _productService.GetByIDAsync(offer.ProductID);
            product.IsSold= true;
            product.IsOfferable = false;
            product.AppUserID=offer.AppUserID;
            product.UnitPrice = offer.Price;
            await _productService.UpdateAsync(product);

            List<Offer> offers =await _offerRepository.GetByOffersProductIDAsync(offer.ProductID);//İlgili üründeki tüm Offerları siliyoruz.
            foreach (Offer item in offers)
            {
                Delete(item.ID);
            }
        }

        public async Task UpdateAsync(Offer entity)
        {
            try
            {
                await _offerRepository.UpdateAsync(entity);
            }
            catch (Exception e)
            {

                throw new Exception($"Update_Error  =>  {e.Message}");
            }
        }
    }
}

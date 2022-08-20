using FinalProject.DataAccess;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class OfferService : GenericService<Offer>, IOfferService
    {
        private readonly IOfferRepository _offerRepository;
        private readonly IProductService _productService;

        public OfferService( IOfferRepository offerRepository, IProductService productService) : base(offerRepository)
        {
            _offerRepository = offerRepository;
            _productService = productService;
        }

        /// <summary>
        /// Ürün satın alma işlemleri
        /// </summary>
        /// <param name="offer">Satın alınıcak teklif bilgisi</param>
        public async Task BuyProductAsync(Offer offer)//Ürün Satın alma 
        {
            Product product = await _productService.GetByIDAsync(offer.ProductID);
            product.UnitPrice = offer.Price;
            product.IsSold=true;
            product.IsOfferable = false;
            product.AppUserID=offer.AppUserID;
            await _productService.UpdateAsync(product);//Ürün bilgilerini güncelliyoruz.

            List<Offer> offers = await _offerRepository.GetByOffersProductIDAsync(offer.ProductID);//İlgili üründeki tüm Offerları siliyoruz.
            foreach (Offer item in offers)
            {
               if(item.DeletedDate ==null )
                    await DeleteAsync(item.ID);
            }
        }

        /// <summary>
        /// Girilen ID'li kullanıcının ürünlerine gelen teklifler
        /// </summary>
        /// <param name="id">Kullanıcı id bilgisi</param>
        public async Task<List<Product>> GetByAppUserProductsOffersAsync(int id)
        {
            return await _offerRepository.GetByAppUserProductsOffers(id);
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
               await _offerRepository.DeleteAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception($"Delete_Error  =>  {e.Message}");
            }
        }

        /// <summary>
        /// Girilen kullanıcı Id'sinin yaptıgı teklifler
        /// </summary>
        /// <param name="id">Kullanıcı id bilgisi</param>
        public async Task<List<Offer>> GetByAppUserOffersAsync(int id)
        {
            try
            {
              return  await _offerRepository.GetByAppUserOffersAsync(id);
            }
            catch (Exception e)
            {
                throw new Exception($"GetByAppUserOffers_Error  =>  {e.Message}");
            }
        }

        /// <summary>
        /// Teklif Onaylama işlemleri
        /// </summary>
        /// <param name="offer">Teklif Bilgisi</param>
        public async Task OfferApprovalAsync(Offer offer)
        {
            //Teklif Onaylandıysa ilgili Ürünün bilgilerini güncelliyoruz.
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
              await  DeleteAsync(item.ID);
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


        /// <summary>
        /// Girilen Ürün Id'sine yapılan teklifler 
        /// </summary>
        /// <param name="id">ürün id bilgisi</param>
        public async Task<List<Offer>> GetByOffersProductIDAsync(int id)
        {
           return await _offerRepository.GetByOffersProductIDAsync(id);
        }
    }
}

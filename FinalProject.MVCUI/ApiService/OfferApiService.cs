using FinalProject.Base;
using FinalProject.DTO;

namespace FinalProject.MVCUI
{
    public class OfferApiService
    {
        private HttpClient _httpClient;

        public OfferApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OfferListDto>> GetAllAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<OfferListDto>> response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<OfferListDto>>>("Offers/GetAll");

            return response.Data;
        }

        public async Task<List<OfferListDto>> GetActiveAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<OfferListDto>> response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<OfferListDto>>>("Offers/GetActive");

            return response.Data;
        }

        public async Task<List<OfferListDto>> GetPassiveAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<OfferListDto>> response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<OfferListDto>>>("Offers/GetPassive");

            return response.Data;
        }

        public async Task<OfferDto> GetByIDAsync(string token,int id)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<OfferDto> response = await _httpClient.GetFromJsonAsync<CustomResponseDto<OfferDto>>($"Offers/{id}");

            return response.Data;
        }

        public async Task<List<OfferListDto>> GetByAppUserOffersAsync(string token,int appUserID)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<OfferListDto>> response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<OfferListDto>>>($"Offers/GetByAppUserOffers/{appUserID}");

            return response.Data;
        }

        public async Task<List<ProductOffersListDto>> GetByOffersProductIDAsync(string token, int productID)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<ProductOffersListDto>> response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductOffersListDto>>>($"Offers/GetByOffersProductID/{productID}");

            return response.Data;
        }

        public async Task<List<AppUserProductsOfferListDto>> GetByAppUserProductsOffersAsync(string token, int appUserId)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<AppUserProductsOfferListDto>> response = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<AppUserProductsOfferListDto>>>($"Offers/GetByAppUserProductsOffers/{appUserId}");

            return response.Data;
        }

        public async Task<bool> BuyProductAsync(string token, OfferBuyProductDto offerBuyProductDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers/BuyProduct",offerBuyProductDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> OfferApprovalAsync(string token, OfferApprovalDto offerApprovalDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers/OfferApproval", offerApprovalDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddAsync(string token, OfferAddDto offerAddDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers", offerAddDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, OfferUpdateDto offerUpdateDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers", offerUpdateDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Offers/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

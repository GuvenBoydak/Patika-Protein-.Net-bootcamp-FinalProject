using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class OfferApiService
    {
        private HttpClient _httpClient;

        public OfferApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OfferModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<OfferModel>> response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<OfferModel>>>("Offers/GetAll");

            return response.Data;
        }

        public async Task<List<OfferModel>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<OfferModel>> response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<OfferModel>>>("Offers/GetActive");

            return response.Data;
        }

        public async Task<List<OfferModel>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<OfferModel>> response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<OfferModel>>>("Offers/GetPassive");

            return response.Data;
        }

        public async Task<OfferModel> GetByIDAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<OfferModel> response = await _httpClient.GetFromJsonAsync<CustomResponseModel<OfferModel>>($"Offers/{id}");

            return response.Data;
        }

        public async Task<List<OfferModel>> GetByAppUserOffersAsync(string token, int appUserID)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<OfferModel>> response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<OfferModel>>>($"Offers/GetByAppUserOffers/{appUserID}");

            return response.Data;
        }

        public async Task<List<OfferModel>> GetByOffersProductIDAsync(string token, int productID)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<OfferModel>> response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<OfferModel>>>($"Offers/GetByOffersProductID/{productID}");

            return response.Data;
        }

        public async Task<List<OfferModel>> GetByAppUserProductsOffersAsync(string token, int appUserId)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<OfferModel>> response = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<OfferModel>>>($"Offers/GetByAppUserProductsOffers/{appUserId}");

            return response.Data;
        }

        public async Task<bool> BuyProductAsync(string token, OfferModel offerModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers/BuyProduct", offerModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> OfferApprovalAsync(string token, OfferModel offerModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers/OfferApproval", offerModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> AddAsync(string token, OfferModel offerModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers", offerModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, OfferModel offerModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Offers", offerModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Offers/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
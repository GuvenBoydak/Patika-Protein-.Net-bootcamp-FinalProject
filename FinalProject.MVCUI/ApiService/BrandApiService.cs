using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class BrandApiService
    {
        private HttpClient _httpClient;

        public BrandApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<BrandModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseModel<List<BrandModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<BrandModel>>>("Brands/GetAll");

            return responseDto.Data;
        }

        public async Task<List<BrandModel>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseModel<List<BrandModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<BrandModel>>>("Brands/GetActive");

            return responseDto.Data;
        }

        public async Task<List<BrandModel>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseModel<List<BrandModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<BrandModel>>>("Brands/GetPassive");

            return responseDto.Data;
        }

        public async Task<BrandModel> GetByIDAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseModel<BrandModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<BrandModel>>($"Brands/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token, BrandModel brandModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Brands", brandModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, BrandModel brandModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Brands", brandModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Brands/{id}");

            return response.IsSuccessStatusCode;
        }

    }
}
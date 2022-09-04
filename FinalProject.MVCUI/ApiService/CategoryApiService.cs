using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class CategoryApiService
    {
        private HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<CategoryModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<CategoryModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<CategoryModel>>>("Categories/GetAll");

            return responseDto.Data;
        }

        public async Task<List<CategoryModel>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<CategoryModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<CategoryModel>>>("Categories/GetActive");

            return responseDto.Data;
        }

        public async Task<List<CategoryModel>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<CategoryModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<CategoryModel>>>("Categories/GetPassive");

            return responseDto.Data;
        }

        public async Task<CategoryModel> GetByIDAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<CategoryModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<CategoryModel>>($"Categories/{id}");

            return responseDto.Data;
        }

        public async Task<ProductModel> GetCategoryWithProductsAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<ProductModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<ProductModel>>($"Categories/GetCategoryWithProducts/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token, CategoryModel categoryModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Categories", categoryModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Categories/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, CategoryModel categoryModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Categories", categoryModel);

            return response.IsSuccessStatusCode;
        }
    }
}
using FinalProject.Base;
using FinalProject.DTO;
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


        public async Task<List<CategoryListDto>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseDto<List<CategoryListDto>> responseDto =await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryListDto>>>("Categories/GetAll");

            return responseDto.Data;
        }

        public async Task<List<CategoryListDto>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseDto<List<CategoryListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryListDto>>>("Categories/GetActive");

            return responseDto.Data;
        }

        public async Task<List<CategoryListDto>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseDto<List<CategoryListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<CategoryListDto>>>("Categories/GetPassive");

            return responseDto.Data;
        }

        public async Task<CategoryDto> GetByIDAsync(int id,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseDto<CategoryDto> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryDto>>($"Categories/{id}");

            return responseDto.Data;
        }

        public async Task<CategoryWithProductsDto> GetCategoryWithProductsAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseDto<CategoryWithProductsDto> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<CategoryWithProductsDto>>($"Categories/GetCategoryWithProducts/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token,CategoryAddDto categoryAddDto)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response =await _httpClient.PostAsJsonAsync("Categories", categoryAddDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token,int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Categories/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, CategoryUpdateDto categoryUpdateDto)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"Categories",categoryUpdateDto);

            return response.IsSuccessStatusCode;
        }
    }
}

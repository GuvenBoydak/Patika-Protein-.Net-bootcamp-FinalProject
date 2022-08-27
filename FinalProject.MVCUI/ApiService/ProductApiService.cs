using FinalProject.Base;
using FinalProject.DTO;
using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class ProductApiService
    {
        private  HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<List<ProductListDto>> GetActiveProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

            CustomResponseDto<List<ProductListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductListDto>>>("Products/GetActive");
            return responseDto.Data;
        }

        public async Task<List<ProductListDto>> GetPassiveProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<List<ProductListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductListDto>>>("Products/GetPassive");
            return responseDto.Data;
        }

        public async Task<List<ProductListDto>> GetAllProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<List<ProductListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductListDto>>>("Products/GetAll");
            return responseDto.Data;
        }

        public async Task<List<ProductListDto>> GetByProductsPaginationAsync(int limit,int page, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<List<ProductListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ProductListDto>>>($"Products/GetByProductsPagination?limit={limit}&page={page}");
            return responseDto.Data;
        }

        public async Task<ProductDto> GetByIDAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<ProductDto> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<ProductDto>>($"Products/{id}");
            return responseDto.Data;
        }

        public async Task<bool> AddAsync(ProductAddDto productAddDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Products", productAddDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Products", productUpdateDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage response = await _httpClient.DeleteAsync($"Products/{id}");

            return response.IsSuccessStatusCode;
        }


    }
}

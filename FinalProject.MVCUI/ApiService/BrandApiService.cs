using FinalProject.Base;
using FinalProject.DTO;
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

        public async Task<List<BrandListDto>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<List<BrandListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<BrandListDto>>>("Brands/GetAll");

            return responseDto.Data;
        }

        public async Task<List<BrandListDto>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<List<BrandListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<BrandListDto>>>("Brands/GetActive");

            return responseDto.Data;
        }

        public async Task<List<BrandListDto>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<List<BrandListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<BrandListDto>>>("Brands/GetPassive");

            return responseDto.Data;
        }

        public async Task<BrandDto> GetByIDAsync(int id,string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            CustomResponseDto<BrandDto> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<BrandDto>>($"Brands/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token,BrandAddDto brandAddDto)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Brands", brandAddDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, BrandUpdateDto brandUpdateDto)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Brands", brandUpdateDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token,int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Brands/{id}" );

            return response.IsSuccessStatusCode;
        }

    }
}

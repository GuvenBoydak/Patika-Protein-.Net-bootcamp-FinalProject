using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class ColorApiService
    {
        private HttpClient _httpClient;

        public ColorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ColorModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ColorModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ColorModel>>>("Colors/GetAll");

            return responseDto.Data;
        }

        public async Task<List<ColorModel>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ColorModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ColorModel>>>("Colors/GetActive");

            return responseDto.Data;
        }

        public async Task<List<ColorModel>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ColorModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ColorModel>>>("Colors/GetPasive");

            return responseDto.Data;
        }

        public async Task<ColorModel> GetByIDAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<ColorModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<ColorModel>>($"Colors/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token, ColorModel colorModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Colors", colorModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, ColorModel colorModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Colors", colorModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Colors/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}
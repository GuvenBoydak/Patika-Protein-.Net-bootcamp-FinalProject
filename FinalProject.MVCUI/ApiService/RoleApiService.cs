using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class RoleApiService
    {
        private HttpClient _httpClient;

        public RoleApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<RoleModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<RoleModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<RoleModel>>>("Roles/GetAll");

            return responseDto.Data;
        }

        public async Task<List<RoleModel>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<RoleModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<RoleModel>>>("Roles/GetActive");

            return responseDto.Data;
        }

        public async Task<List<RoleModel>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<RoleModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<RoleModel>>>("Roles/GetPassive");

            return responseDto.Data;
        }

        public async Task<RoleModel> GetByIDAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<RoleModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<RoleModel>>($"Roles/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token, RoleModel roleModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Roles", roleModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, RoleModel roleModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Roles", roleModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Roles/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

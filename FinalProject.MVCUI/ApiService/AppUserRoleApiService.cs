using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class AppUserRoleApiService
    {
        private HttpClient _httpClient;

        public AppUserRoleApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppUserRoleModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<AppUserRoleModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<AppUserRoleModel>>>("AppUserRoles/GetAll");

            return responseDto.Data;
        }

        public async Task<List<AppUserRoleModel>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<AppUserRoleModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<AppUserRoleModel>>>("AppUserRoles/GetActive");

            return responseDto.Data;
        }

        public async Task<List<AppUserRoleModel>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<AppUserRoleModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<AppUserRoleModel>>>("AppUserRoles/GetPassive");

            return responseDto.Data;
        }

        public async Task<AppUserRoleModel> GetByIDAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<AppUserRoleModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<AppUserRoleModel>>($"AppUserRoles/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token, AppUserRoleModel appUserRoleModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("AppUserRoles", appUserRoleModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, AppUserRoleModel appUserRoleModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("AppUserRoles", appUserRoleModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"AppUserRoles/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

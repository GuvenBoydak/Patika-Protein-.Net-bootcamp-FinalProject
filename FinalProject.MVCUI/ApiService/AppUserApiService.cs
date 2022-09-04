using System.Net.Http.Headers;
using System.Text.Json;

namespace FinalProject.MVCUI
{
    public class AppUserApiService
    {
        private HttpClient _httpClient;

        public AppUserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppUserModel>> GetAllAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<AppUserModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<AppUserModel>>>("AppUsers/GetAll");

            return responseDto.Data;
        }

        public async Task<List<AppUserModel>> GetActiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<AppUserModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<AppUserModel>>>("AppUsers/GetActive");

            return responseDto.Data;
        }

        public async Task<List<AppUserModel>> GetPassiveAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<AppUserModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<AppUserModel>>>("AppUsers/GetPassive");

            return responseDto.Data;
        }

        public async Task<List<Role>> GetRolesAsync(string token,int appUserID)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<Role>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<Role>>>($"AppUsers/GetRoles/{appUserID}");

            return responseDto.Data;
        }

        public async Task<AppUserModel> GetByIDAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<AppUserModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<AppUserModel>>($"AppUsers/{id}");

            return responseDto.Data;
        }

        public async Task<AppUserModel> GetByEmailAsync(string token, string email)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<AppUserModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<AppUserModel>>($"AppUsers/GetAppUserByEmail/{email}");

            return responseDto.Data;

        }

        public async Task<bool> GetActivationAsync(string token, Guid code)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.GetFromJsonAsync<HttpResponseMessage>($"AppUsers/Activation/{code}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<ProductModel>> GetAppUserProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ProductModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ProductModel>>>("AppUsers/GetAppUserProducts");

            return responseDto.Data;
        }

        public async Task<HttpResponseMessage> RegisterAsync(AppUserRegisterModel appUserRegisterModel)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("AppUsers/Register", appUserRegisterModel);

            return response;
        }

        public async Task<HttpResponseMessage> LoginAsync(AppUserLoginModel appUserLoginModel)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("AppUsers/Login", appUserLoginModel);

            return response;
        }

        public async Task<bool> ChangePasswordAsync(string token, AppUserPasswordUpdateModel appUserPasswordUpdateModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("AppUsers/ChangePassword", appUserPasswordUpdateModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, AppUserModel appUserModel)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("AppUsers", appUserModel);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token, int id)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"AppUsers/{id}");

            return response.IsSuccessStatusCode;
        }


        private string DeserialezeToken(string json)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            CustomResponseModel<AccessToken> result = JsonSerializer.Deserialize<CustomResponseModel<AccessToken>>(json, options);


            return result.Data.Token;
        }
    }
}

﻿using FinalProject.Base;
using FinalProject.DTO;

namespace FinalProject.MVCUI
{
    public class AppUserApiService
    {
        private HttpClient _httpClient;

        public AppUserApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<AppUserListDto>> GetAllAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<AppUserListDto>> responseDto =await _httpClient.GetFromJsonAsync<CustomResponseDto<List<AppUserListDto>>>("AppUsers/GetAll");

            return responseDto.Data;
        }

        public async Task<List<AppUserListDto>> GetActiveAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<AppUserListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<AppUserListDto>>>("AppUsers/GetActive");

            return responseDto.Data;
        }

        public async Task<List<AppUserListDto>> GetPassiveAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<AppUserListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<AppUserListDto>>>("AppUsers/GetPassive");

            return responseDto.Data;
        }

        public async Task<AppUserDto> GetByIDAsync(string token,int id)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<AppUserDto> responseDto =await _httpClient.GetFromJsonAsync<CustomResponseDto<AppUserDto>>($"AppUsers/{id}");

            return responseDto.Data;
        }

        public async Task<bool> GetActivationAsync(string token, Guid code)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.GetFromJsonAsync<HttpResponseMessage>($"AppUsers/Activation/{code}");

            return response.IsSuccessStatusCode;
        }

        public async Task<List<AppUserProductsDto>> GetAppUserProductsAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<AppUserProductsDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<AppUserProductsDto>>>("AppUsers/GetAppUserProducts");

            return responseDto.Data;
        }

        public async Task<string> RegisterAsync( AppUserRegisterDto appUserRegisterDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("AppUsers/Register",appUserRegisterDto);

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> LoginAsync(AppUserLoginDto appUserLoginDto)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("AppUsers/Login", appUserLoginDto);

            
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> ChangePasswordAsync(string token, AppUserPasswordUpdateDto appUserPasswordUpdateDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("AppUsers/ChangePassword", appUserPasswordUpdateDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token,AppUserUpdateDto appUserUpdateDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("AppUsers", appUserUpdateDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token,int id)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"AppUsers/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

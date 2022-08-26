using FinalProject.Base;
using FinalProject.DTO;

namespace FinalProject.MVCUI
{
    public class ColorApiService
    {
        private HttpClient _httpClient;

        public ColorApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ColorListDto>> GetAllAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<ColorListDto>> responseDto =await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ColorListDto>>>("Colors/GetAll");

            return responseDto.Data;
        }

        public async Task<List<ColorListDto>> GetActiveAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<ColorListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ColorListDto>>>("Colors/GetActive");

            return responseDto.Data;
        }

        public async Task<List<ColorListDto>> GetPassiveAsync(string token)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<List<ColorListDto>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<List<ColorListDto>>>("Colors/GetPasive");

            return responseDto.Data;
        }

        public async Task<ColorDto> GetByIDAsync(string token,int id)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            CustomResponseDto<ColorDto> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseDto<ColorDto>>($"Colors/{id}");

            return responseDto.Data;
        }

        public async Task<bool> AddAsync(string token,ColorAddDto colorAddDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Colors",colorAddDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(string token, ColorUpdateDto colorUpdateDto)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Colors", colorUpdateDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string token,int id)
        {
            _httpClient = Authorization.AuthorizationWithToken(token);

            HttpResponseMessage response = await _httpClient.DeleteAsync($"Colors/{id}");

            return response.IsSuccessStatusCode;
        }
    }
}

using AutoMapper;
using FinalProject.Base;
using FinalProject.DTO;
using FinalProject.Entities;
using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class ProductApiService
    {
        private HttpClient _httpClient;
        private  readonly IMapper _mapper;
        private  readonly IFileHelper _fileHelper;
        private readonly IConfiguration _configuration;


        public ProductApiService(HttpClient httpClient, IMapper mapper, IFileHelper fileHelper, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _configuration = configuration;
        }


        public async Task<List<ProductListDto>> GetActiveProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        public async Task<List<ProductListDto>> GetByProductsPaginationAsync(int limit, int page, string token)
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

        public async Task<bool> AddAsync(ProductAddWitFileDto productAddWitFileDto, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (productAddWitFileDto.ImageUrl == null)
                return false;

            ProductAddDto productAddDto = _mapper.Map<ProductAddWitFileDto, ProductAddDto>(productAddWitFileDto);

            productAddDto.ImageUrl = $"{_configuration.GetSection("BaseUrlImage").Value}{_fileHelper.Add(productAddWitFileDto.ImageUrl, _configuration.GetSection("ImageUrl").Value)}";//Resim ekleme işlemi

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Products", productAddDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ProductUpdateDto productUpdateDto,IFormFile file, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            if (file!=null)
                productUpdateDto.ImageUrl= $"{_configuration.GetSection("BaseUrlImage").Value}{_fileHelper.Update(file, _configuration.GetSection("ImageUrl").Value + productUpdateDto.ImageUrl, _configuration.GetSection("ImageUrl").Value)}";

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

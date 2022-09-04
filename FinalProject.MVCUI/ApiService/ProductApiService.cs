using AutoMapper;
using System.Net.Http.Headers;

namespace FinalProject.MVCUI
{
    public class ProductApiService
    {
        private HttpClient _httpClient;
        private readonly IFileHelper _fileHelper;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;


        public ProductApiService(HttpClient httpClient, IFileHelper fileHelper, IConfiguration configuration, IMapper mapper)
        {
            _httpClient = httpClient;
            _fileHelper = fileHelper;
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<List<ProductModel>> GetActiveProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ProductModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ProductModel>>>("Products/GetActive");
            return responseDto.Data;
        }

        public async Task<List<ProductModel>> GetPassiveProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ProductModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ProductModel>>>("Products/GetPassive");
            return responseDto.Data;
        }

        public async Task<List<ProductModel>> GetAllProductsAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ProductModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ProductModel>>>("Products/GetAll");
            return responseDto.Data;
        }

        public async Task<List<ProductModel>> GetByProductsPaginationAsync(int limit, int page, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<List<ProductModel>> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<List<ProductModel>>>($"Products/GetByProductsPagination?limit={limit}&page={page}");

            return responseDto.Data;
        }

        public async Task<ProductModel> GetByIDAsync(int id, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            CustomResponseModel<ProductModel> responseDto = await _httpClient.GetFromJsonAsync<CustomResponseModel<ProductModel>>($"Products/{id}");
            return responseDto.Data;
        }

        public async Task<bool> AddAsync(ProductAddWithFileModel productAddWithFileModel, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (productAddWithFileModel.ImageUrl == null)
                return false;

            ProductModel productAddDto = _mapper.Map<ProductAddWithFileModel, ProductModel>(productAddWithFileModel);

            productAddDto.ImageUrl = $"{_configuration.GetSection("BaseUrlImage").Value}{_fileHelper.Add(productAddWithFileModel.ImageUrl, _configuration.GetSection("ImageUrl").Value)}";//Resim ekleme işlemi

            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("Products", productAddDto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(ProductModel productModel, IFormFile file, string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (file != null)
                productModel.ImageUrl = $"{_configuration.GetSection("BaseUrlImage").Value}{_fileHelper.Update(file, _configuration.GetSection("ImageUrl").Value + productModel.ImageUrl, _configuration.GetSection("ImageUrl").Value)}";

            HttpResponseMessage response = await _httpClient.PutAsJsonAsync("Products", productModel);

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
using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinalProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IFileHelper _fileHelper;
        private readonly IConfiguration _configuration;

        public ProductsController(IProductService productService, IMapper mapper, IFileHelper fileHelper, IConfiguration configuration)
        {
            _productService = productService;
            _mapper = mapper;
            _fileHelper = fileHelper;
            _configuration = configuration;
        }


        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<Product> products = await _productService.GetActiveAsync();

            List<ProductListDto> productListDtos = _mapper.Map<List<ProductListDto>>(products);

            return CreateActionResult(CustomResponseDto<List<ProductListDto>>.Success(200, productListDtos, "Active Ürünler listelendi"));
        }

        [HttpGet("GetPasive")]
        public async Task<IActionResult> GetPasiveAsync()
        {
            List<Product> products = await _productService.GetPassiveAsync();

            List<ProductListDto> productListDtos = _mapper.Map<List<ProductListDto>>(products);

            return CreateActionResult(CustomResponseDto<List<ProductListDto>>.Success(200, productListDtos, "Pasive Ürünler listelendi"));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Product> products = await _productService.GetActiveAsync();

            List<ProductListDto> productListDtos = _mapper.Map<List<ProductListDto>>(products);

            return CreateActionResult(CustomResponseDto<List<ProductListDto>>.Success(200, productListDtos, "Tüm Ürünler listelendi"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            Product product = await _productService.GetByIDAsync(id);

            ProductDto productDto = _mapper.Map<ProductDto>(product);

            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDto, $"{id} numarali ürün listelendi"));
        }

        [Authorize]
        [HttpPost]
        public IActionResult Add([FromForm(Name = "Image")] IFormFile file,/* [FromForm] */ProductAddDto productAddDto)//Postman de çalişiyor 
        {
            if(file==null)//Girelen resim vamrı bakıyoruz.
                throw new Exception("REsim kısmı boş geçilemez");

            Product product = _mapper.Map<Product>(productAddDto);

            product.AppUserID = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AppUserID").Value);//Kullanıcı claimlerinden Id sini aldık.

            product.ImageUrl = _fileHelper.Add(file,_configuration.GetSection("ImageUrl").Value);//Resim ekleme işlemi

            _productService.Add(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Ekleme işlemi Başarılı"));
        }


        [HttpPut]
        public async Task<IActionResult> Update(/*[FromForm]*/ProductUpdateDto productUpdateDto,[FromForm(Name = "Image")] IFormFile file)//Postman de Çalişiyor.
        {
            Product product = _mapper.Map<Product>(productUpdateDto);

            if (productUpdateDto.ImageUrl != null)//Gonderilen mmodellin ImageUrl boş degilse resim günceliyoruz.
                _fileHelper.Update(file, _configuration.GetSection("ImageUrl").Value + product.ImageUrl, _configuration.GetSection("ImageUrl").Value);

            await _productService.UpdateAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Güncelleme İşlemi başarılı"));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            _productService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme İşlemi Başarılı"));
        }
    }
}

using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FinalProject.Api
{
    [Authorize]
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

        [HttpGet]
        [Route("GetByProductsPagination")]
        public async Task<IActionResult> GetByProductsPaginationAsync([FromQuery]int limit,[FromQuery]int page)
        {
            List<Product> products = await _productService.GetByProductsPaginationAsync(limit,page);

            List<ProductListDto> productListDtos = _mapper.Map<List<Product>, List<ProductListDto>>(products);

            return CreateActionResult(CustomResponseDto<List<ProductListDto>>.Success(200, productListDtos, "Ürünler sayfalanarak listelendi"));
        }

        [HttpGet]
        [Route("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<Product> products = await _productService.GetActiveAsync();

            List<ProductListDto> productListDtos = _mapper.Map<List<ProductListDto>>(products);

            return CreateActionResult(CustomResponseDto<List<ProductListDto>>.Success(200, productListDtos, "Active Ürünler listelendi"));
        }

        [HttpGet]
        [Route("GetPassive")]
        public async Task<IActionResult> GetPassiveAsync()
        {
            List<Product> products = await _productService.GetPassiveAsync();

            List<ProductListDto> productListDtos = _mapper.Map<List<ProductListDto>>(products);

            return CreateActionResult(CustomResponseDto<List<ProductListDto>>.Success(200, productListDtos, "Pasive Ürünler listelendi"));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Product> products = await _productService.GetAllAsync();

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

        
        [HttpPost]
        public IActionResult Add(ProductAddDto productAddDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Product  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Product product = _mapper.Map<Product>(productAddDto);

            product.AppUserID = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AppUserID").Value);//Kullanıcı claimlerinden Id sini aldık.

            _productService.Add(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Ekleme işlemi Başarılı"));
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync(ProductUpdateDto productUpdateDto)
        {
            Log.Information($"{User.Identity?.Name}: Update a Product  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Product product = _mapper.Map<Product>(productUpdateDto);

            product.AppUserID = int.Parse((User.Identity as ClaimsIdentity).FindFirst("AppUserID").Value);//Kullanıcı claimlerinden Id sini aldık.

            await _productService.UpdateAsync(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Güncelleme İşlemi başarılı"));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            _productService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme İşlemi Başarılı"));
        }
    }
}

using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
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

        [HttpPost]
        public IActionResult Add([FromBody] ProductAddDto productAddDto)
        {
            Product product=_mapper.Map<Product>(productAddDto);

            _productService.Add(product);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Ekleme işlemi Başarılı"));
        }

        [HttpPut]
        public IActionResult Update([FromBody]ProductUpdateDto productUpdateDto)
        {
            Product product = _mapper.Map<Product>(productUpdateDto);

            _productService.Update(product);

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

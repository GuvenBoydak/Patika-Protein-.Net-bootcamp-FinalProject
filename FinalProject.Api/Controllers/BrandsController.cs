using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : CustomBaseController
    {
        private readonly IBrandService _brandService;
        private readonly IMapper _mapper;

        public BrandsController(IBrandService brandService, IMapper mapper)
        {
            _brandService = brandService;
            _mapper = mapper;
        }

        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<Brand> brands = await _brandService.GetActiveAsync();

            List<BrandListDto> brandListDtos = _mapper.Map<List<BrandListDto>>(brands);

            return CreateActionResult(CustomResponseDto<List<BrandListDto>>.Success(200,brandListDtos,"Active Markalar listelendi"));
        }

        [HttpGet("GetPasive")]
        public async Task<IActionResult> GetPassiveAsync()
        {
            List<Brand> brands = await _brandService.GetPassiveAsync();

            List<BrandListDto> brandListDtos = _mapper.Map<List<BrandListDto>>(brands);

            return CreateActionResult(CustomResponseDto<List<BrandListDto>>.Success(200, brandListDtos, "Passive Markalar listelendi"));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Brand> brands = await _brandService.GetAllAsync();

            List<BrandListDto> brandListDtos = _mapper.Map<List<BrandListDto>>(brands);

            return CreateActionResult(CustomResponseDto<List<BrandListDto>>.Success(200, brandListDtos, "Tüm Markalar listelendi"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            Brand brand = await _brandService.GetByIDAsync(id);

            BrandDto brandDto=_mapper.Map<BrandDto>(brand);

            return CreateActionResult(CustomResponseDto<BrandDto>.Success(200,brandDto,$"{id} numaralı marka listelendi"));
        }

        [HttpPost]
        public IActionResult Add([FromBody]BrandAddDto brandAddDto)
        {
            Brand brand=_mapper.Map<Brand>(brandAddDto);

            _brandService.Add(brand);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Ekleme işlemi Başarılı"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]BrandUpdateDto brandUpdateDto)
        {
            Brand brand = _mapper.Map<Brand>(brandUpdateDto);

            await _brandService.UpdateAsync(brand);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Güncelleme işlemi Başarılı"));
        }

        [HttpDelete]
        public IActionResult Delete([FromRoute]int id)
        {
            _brandService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme işlemi Başarılı"));
        }
    }
}

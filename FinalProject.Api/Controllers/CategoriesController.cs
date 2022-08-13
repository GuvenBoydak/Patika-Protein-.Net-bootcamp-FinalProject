using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DataAccess;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : CustomBaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;


        public CategoriesController(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }



        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<Category> categories = await _categoryService.GetActiveAsync();

            List<CategoryListDto> categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryListDto>>.Success(200, categoryListDtos, "Active Categoryler listelendi"));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Category> categories = await _categoryService.GetAllAsync();

            List<CategoryListDto> categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryListDto>>.Success(200, categoryListDtos, "Tüm Categoryler listelendi"));
        }

        [HttpGet("GetPasive")]
        public async Task<IActionResult> GetPasiveAsync()
        {
            List<Category> categories = await _categoryService.GetPassiveAsync();

            List<CategoryListDto> categoryListDtos = _mapper.Map<List<CategoryListDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryListDto>>.Success(200, categoryListDtos, "Pasive Categoryler listelendi"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            Category category = await _categoryService.GetByIDAsync(id);

            CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto, $"{id} numaralı Category Listlendi."));
        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryAddDto categoryAddDto)
        {
            Category category = _mapper.Map<Category>(categoryAddDto);

            _categoryService.Add(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(200,"Ekleme işlem başarılı"));
        }

        [HttpPut]
        public IActionResult Update([FromBody]CategoryUpdateDto categoryUpdateDto)
        {
            Category category = _mapper.Map<Category>(categoryUpdateDto);
            _categoryService.Update(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Güncelleme Başarılı"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            _categoryService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme işlemi başrılı"));
        }

    }
}

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

            List<CategoryListDto> categoryListDtos = _mapper.Map<List<Category>, List<CategoryListDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryListDto>>.Success(200, categoryListDtos, "Active Categoryler listelendi"));
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Category> categories = await _categoryService.GetAllAsync();

            List<CategoryListDto> categoryListDtos = _mapper.Map<List<Category>, List<CategoryListDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryListDto>>.Success(200, categoryListDtos, "Tüm Categoryler listelendi"));
        }

        [HttpGet("GetPassive")]
        public async Task<IActionResult> GetPassiveAsync()
        {
            List<Category> categories = await _categoryService.GetPassiveAsync();

            List<CategoryListDto> categoryListDtos = _mapper.Map<List<Category>, List<CategoryListDto>>(categories);

            return CreateActionResult(CustomResponseDto<List<CategoryListDto>>.Success(200, categoryListDtos, "Pasive Categoryler listelendi"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            Category category = await _categoryService.GetByIDAsync(id);

            CategoryDto categoryDto = _mapper.Map<Category, CategoryDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryDto>.Success(200, categoryDto, $"{id} numaralı Category Listlendi."));
        }

        [HttpGet]
        [Route("GetCategoryWithProducts/{id}")]
        public async Task<IActionResult> GetCategoryWithProductsAsync([FromRoute] int id)
        {
            Category category = await _categoryService.GetCategoryWithProductsAsync(id);

            CategoryWithProductsDto categoryDto = _mapper.Map<Category, CategoryWithProductsDto>(category);

            return CreateActionResult(CustomResponseDto<CategoryWithProductsDto>.Success(200,categoryDto, $"{id} numaralı Category ve Productlar Listlendi."));
        }

        [HttpPost]
        public IActionResult Add([FromBody] CategoryAddDto categoryAddDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Category  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Category category = _mapper.Map<CategoryAddDto, Category>(categoryAddDto);

            _categoryService.Add(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Ekleme işlem başarılı"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] CategoryUpdateDto categoryUpdateDto)
        {
            Log.Information($"{User.Identity?.Name}: Update a Category  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Category category = _mapper.Map<CategoryUpdateDto, Category>(categoryUpdateDto);
            await _categoryService.UpdateAsync(category);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Güncelleme Başarılı"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _categoryService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme işlemi başrılı"));
        }

    }
}

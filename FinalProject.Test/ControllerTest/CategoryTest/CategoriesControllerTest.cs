using AutoMapper;
using FinalProject.Api;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinalProject.Test
{
    public class CategoriesControllerTest
    {
        private readonly Mock<ICategoryService> _categoryService;
        private readonly IMapper _mapper;
        private readonly CategoriesController _categoriesController;

        public CategoriesControllerTest()
        {
            MapperConfiguration map = new MapperConfiguration(configure =>
            {
                configure.AddProfile(new MapProfile());
            });
            _categoryService = new Mock<ICategoryService>();
            _mapper = map.CreateMapper();
            _categoriesController = new CategoriesController(_categoryService.Object, _mapper);
        }


        List<Category> categories = new List<Category>
        {
            new Category(){ID=1, Name="Bilgisayar",Description="Bilgisayar malzemeleri",CreatedDate=DateTime.UtcNow,Status=DataStatus.Inserted},
            new Category(){ID=1, Name="Giyim",Description="Bay Bayan Kıyafetler",CreatedDate=DateTime.UtcNow,Status=DataStatus.Inserted},
            new Category(){ID=1, Name="Ev Aletleri",Description="Ev aletleri malzemeleri",CreatedDate=DateTime.UtcNow,Status=DataStatus.Inserted},
        };

        [Fact]
        public async void GetAll_ActionExecutes_Return200WithCategoryListDto()
        {
            _categoryService.Setup(x => x.GetAllAsync()).ReturnsAsync(categories);

            IActionResult result =await _categoriesController.GetAllAsync();

            ObjectResult objectResult=Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<CategoryListDto>> responseDto=Assert.IsAssignableFrom<CustomResponseDto<List<CategoryListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }


        [Fact]
        public async void GetActive_ActionExecutes_Return200WithCategoryListDto()
        {
            _categoryService.Setup(x => x.GetActiveAsync()).ReturnsAsync(categories);

            IActionResult result = await _categoriesController.GetActiveAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<CategoryListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<CategoryListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }


        [Fact]
        public async void GetPassive_ActionExecutes_Return200WithCategoryListDto()
        {
            _categoryService.Setup(x => x.GetPassiveAsync()).ReturnsAsync(categories);

            IActionResult result = await _categoriesController.GetPassiveAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<CategoryListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<CategoryListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }



        [Theory]
        [InlineData(1)]
        public async void GetByID_ActionExecutes_Return200WithCategoryDto(int id)
        {
            Category category = categories.FirstOrDefault(x=>x.ID==id);

            _categoryService.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync(category);

            IActionResult result = await _categoriesController.GetByIdAsync(id);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            Assert.IsAssignableFrom<CustomResponseDto<CategoryDto>>(objectResult.Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetCategoryWithProducts_ActionExecutes_Return200WithCategoryWithProductsDto(int id)
        {
            Category category = categories.FirstOrDefault(x=>x.ID==id);

            _categoryService.Setup(x => x.GetCategoryWithProductsAsync(It.IsAny<int>())).ReturnsAsync(category);

            IActionResult result =await _categoriesController.GetCategoryWithProductsAsync(id);

            ObjectResult objectResult=Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            Assert.IsAssignableFrom<CustomResponseDto<CategoryWithProductsDto>>(objectResult.Value);
        }



        [Fact]
        public void Add_ActionExecutes_Return204NoContentDto()
        {
            Category category = new Category() { ID = 4, Name = "Bilgisayar", Description = "Bilgisayar malzemeleri", CreatedDate = DateTime.UtcNow, Status = DataStatus.Inserted };

            _categoryService.Setup(x => x.Add(category));

            CategoryAddDto categoryAddDto = _mapper.Map<Category, CategoryAddDto>(category);

            IActionResult result = _categoriesController.Add(categoryAddDto);

            ObjectResult objectResult=Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204,objectResult.StatusCode.Value);
        }


        [Fact]
        public async void Update_ActionExecutes_Return204NoContentDto()
        {
            Category category = new Category() { ID = 1, Name = "Mobilya", Description = "Mobilya malzemeleri", CreatedDate = DateTime.UtcNow, Status = DataStatus.Inserted };

            _categoryService.Setup(x => x.UpdateAsync(category));

            CategoryUpdateDto categoryUpdateDto = _mapper.Map<Category, CategoryUpdateDto>(category);

            IActionResult result =await _categoriesController.UpdateAsync(categoryUpdateDto);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204, objectResult.StatusCode.Value);
        }

        [Fact]
        public  void Delete_ActionExecutes_Return204NoContentDto()
        {
            int id = 1;

            _categoryService.Setup(x => x.Delete(id));

            IActionResult result = _categoriesController.Delete(id);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204, objectResult.StatusCode.Value);
        }


    }
}

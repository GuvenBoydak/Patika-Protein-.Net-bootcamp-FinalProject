using AutoMapper;
using FinalProject.Api.Controllers;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FinalProject.Test
{
    public class BrandsControllerTest
    {

        private readonly Mock<IBrandService> _brandService;
        private readonly IMapper _mapper;
        private readonly BrandsController _brandsController;

        public BrandsControllerTest()
        {
            MapperConfiguration map = new MapperConfiguration(configure =>
            {
                configure.AddProfile(new MapProfile());
            });
            _brandService = new Mock<IBrandService>();
            _mapper = map.CreateMapper();
            _brandsController = new BrandsController(_brandService.Object, _mapper);
        }


        List<Brand> brands = new List<Brand>
        {
            new Brand(){ID=1, Name="Elektironik", CreatedDate=DateTime.Now, Status=DataStatus.Inserted},
            new Brand(){ID=2, Name="Giyim", CreatedDate=DateTime.Now, Status=DataStatus.Inserted},
            new Brand(){ID=3, Name="Ev aletleri", CreatedDate=DateTime.Now, Status=DataStatus.Inserted},
        };


        [Fact]
        public async void GetAll_ActionExecutes_Return200WithBrandListDto()
        {
            _brandService.Setup(x => x.GetAllAsync()).ReturnsAsync(brands);

            IActionResult result = await _brandsController.GetAllAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<BrandListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<BrandListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }

        [Fact]
        public async void GetActive_ActionExecutes_Return200WithBrandListDto()
        {
            _brandService.Setup(x => x.GetActiveAsync()).ReturnsAsync(brands);

            IActionResult result = await _brandsController.GetActiveAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<BrandListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<BrandListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }

        [Fact]
        public async void GetPassive_ActionExecutes_Return200WithBrandListDto()
        {
            _brandService.Setup(x => x.GetPassiveAsync()).ReturnsAsync(brands);

            IActionResult result = await _brandsController.GetPassiveAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<BrandListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<BrandListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public async void GetByID_ActionExecutes_Return200WithBrandDto(int id)
        {
            Brand brand = brands.FirstOrDefault(x => x.ID == id);

            _brandService.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync(brand);

            IActionResult result = await _brandsController.GetByIdAsync(id);

            ObjectResult objectResult=Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            Assert.IsAssignableFrom<CustomResponseDto<BrandDto>>(objectResult.Value);
        }

        [Fact]
        public void Add_ActionExecutes_Return204NoContentDto()
        {
            Brand brand = new Brand() { ID = 4, Name = "Elektironik", CreatedDate = DateTime.Now, Status = DataStatus.Inserted };

            _brandService.Setup(x => x.Add(brand));

            BrandAddDto brandAddDto = _mapper.Map<Brand, BrandAddDto>(brand);

            IActionResult result = _brandsController.Add(brandAddDto);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204,objectResult.StatusCode.Value);
        }

        [Fact]
        public async void Update_ActionExecutes_Return204NoContenDto()
        {
            Brand brand = new Brand() { ID = 1, Name = "Spor Aletleri", CreatedDate = DateTime.Now, Status = DataStatus.Inserted };

            _brandService.Setup(x => x.UpdateAsync(brand));

            BrandUpdateDto brandUpdateDto = _mapper.Map<Brand, BrandUpdateDto>(brand);

            IActionResult result =await _brandsController.Update(brandUpdateDto);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204, objectResult.StatusCode.Value);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void Delete_ActionExecutes_Return204NoContenDto(int id)
        {
            _brandService.Setup(x => x.Delete(It.IsAny<int>()));

            IActionResult result = _brandsController.Delete(id);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204, objectResult.StatusCode.Value);
        }


    }
}

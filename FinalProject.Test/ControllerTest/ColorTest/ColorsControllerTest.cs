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
    public class ColorsControllerTest
    {

        private readonly Mock<IColorService> _colorService;
        private readonly IMapper _mapper;
        private readonly ColorsController _colorController;

        public ColorsControllerTest()
        {
            MapperConfiguration map = new MapperConfiguration(configure =>
            {
                configure.AddProfile(new MapProfile());
            });
            _colorService = new Mock<IColorService>();
            _mapper = map.CreateMapper();
            _colorController = new ColorsController(_colorService.Object, _mapper);
        }


        List<Color> colors = new List<Color>
        {
            new Color(){ID=1,Name="siyah",CreatedDate=DateTime.Now,Status=DataStatus.Inserted},
            new Color(){ID=2,Name="beyaz",CreatedDate=DateTime.Now,Status=DataStatus.Inserted},
            new Color(){ID=3,Name="kırmızı",CreatedDate=DateTime.Now,Status=DataStatus.Inserted}
        };

        


        [Fact]
        public async void GetAll_ActionsExecutes_Return200WithColorListDto()
        {
            _colorService.Setup(x => x.GetAllAsync()).ReturnsAsync(colors);

            IActionResult result = await _colorController.GetAllAsync();

            ObjectResult objectResult=Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<ColorListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<ColorListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }

        [Fact]
        public async void GetActive_ActionsExecutes_Return200WithColorListDto()
        {
            _colorService.Setup(x => x.GetActiveAsync()).ReturnsAsync(colors);

            IActionResult result = await _colorController.GetActiveAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<ColorListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<ColorListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }

        [Fact]
        public async void GetPassive_ActionsExecutes_Return200WithColorListDto()
        {
            _colorService.Setup(x => x.GetPassiveAsync()).ReturnsAsync(colors);

            IActionResult result = await _colorController.GetPassiveAsync();

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            CustomResponseDto<List<ColorListDto>> responseDto = Assert.IsAssignableFrom<CustomResponseDto<List<ColorListDto>>>(objectResult.Value);

            Assert.Equal<int>(3, responseDto.Data.Count);
        }


        [Theory]
        [InlineData(1)]
        public async void GetByID_ActionExecutes_Return200WithColorDto(int id)
        {
            Color color = colors.FirstOrDefault(x => x.ID == id);

            _colorService.Setup(x => x.GetByIDAsync(It.IsAny<int>())).ReturnsAsync(color);

            IActionResult result = await _colorController.GetByIdAsync(id);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(200, objectResult.StatusCode.Value);

            Assert.IsAssignableFrom<CustomResponseDto<ColorDto>>(objectResult.Value);
        }

        [Fact]
        public void Add_ActionsExecutes_Return204NoContentDto()
        {
            Color color = new Color() { ID = 4, Name = "gri", CreatedDate = DateTime.Now, Status = DataStatus.Inserted };

            _colorService.Setup(x => x.Add(color));

            ColorAddDto colorAddDto = _mapper.Map<Color, ColorAddDto>(color);

            IActionResult result = _colorController.Add(colorAddDto);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204, objectResult.StatusCode.Value);
        }


        [Fact]
        public async void Update_ActionsExecutes_Return204NoContentDto()
        {
            Color color = new Color() { ID=1, Name = "sarı", CreatedDate = DateTime.Now, Status = DataStatus.Inserted };

            _colorService.Setup(x => x.UpdateAsync(color));

            ColorUpdateDto colorUpdateDto = _mapper.Map<Color, ColorUpdateDto>(color);

            IActionResult result =await _colorController.Update(colorUpdateDto);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204, objectResult.StatusCode.Value);
        }


        [Fact]
        public void Delete_ActionsExecutes_Return204NoContentDto()
        {
            int id = 1;

            _colorService.Setup(x => x.Delete(id));

            IActionResult result = _colorController.Delete(id);

            ObjectResult objectResult = Assert.IsType<ObjectResult>(result);

            Assert.Equal<int>(204, objectResult.StatusCode.Value);
        }

    }
}

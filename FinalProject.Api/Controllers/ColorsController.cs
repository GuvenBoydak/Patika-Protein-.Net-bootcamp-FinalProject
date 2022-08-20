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
    public class ColorsController :CustomBaseController
    {

        private readonly IColorService _colorService;
        private readonly IMapper _mapper;

        public ColorsController(IColorService colorService, IMapper mapper)
        {
            _colorService = colorService;
            _mapper = mapper;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Color> colors =await _colorService.GetAllAsync();

            List<ColorListDto> colorListDtos=_mapper.Map<List<ColorListDto>>(colors);

            return CreateActionResult(CustomResponseDto<List<ColorListDto>>.Success(200, colorListDtos, "Tüm Renkler Listelendi"));
        }


        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<Color> colors = await _colorService.GetActiveAsync();

            List<ColorListDto> colorListDtos = _mapper.Map<List<ColorListDto>>(colors);

            return CreateActionResult(CustomResponseDto<List<ColorListDto>>.Success(200, colorListDtos, "Active Renkler Listelendi"));
        }

        [HttpGet("GetPassive")]
        public async Task<IActionResult> GetPassiveAsync()
        {
            List<Color> colors = await _colorService.GetPassiveAsync();

            List<ColorListDto> colorListDtos = _mapper.Map<List<ColorListDto>>(colors);

            return CreateActionResult(CustomResponseDto<List<ColorListDto>>.Success(200, colorListDtos, "Passive Renkler Listelendi"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            Color color = await _colorService.GetByIDAsync(id);

            ColorDto colorDto = _mapper.Map<ColorDto>(color);

            return CreateActionResult(CustomResponseDto<ColorDto>.Success(200, colorDto, $"{id} numaralı renk listelendi"));
        }


        [HttpPost]
        public IActionResult Add([FromBody] ColorAddDto colorAddDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Color color = _mapper.Map<Color>(colorAddDto);

            _colorService.Add(color);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Ekeleme İşlemi Başarılı"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ColorUpdateDto colorUpdateDto)
        {
            Log.Information($"{User.Identity?.Name}: Update a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Color color = _mapper.Map<Color>(colorUpdateDto);
            
           await  _colorService.UpdateAsync(color);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Günceleme işlemi Başarılı"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            _colorService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Silme işlemi Başarılı"));
        }
    }
}

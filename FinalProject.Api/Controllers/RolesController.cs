using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : CustomBaseController
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RolesController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<Role> roles = await _roleService.GetActiveAsync();

            List<RoleListDto> roleListDtos = _mapper.Map<List<Role>, List<RoleListDto>>(roles);

            return CreateActionResult(CustomResponseDto<List<RoleListDto>>.Success(200, roleListDtos, "Active Roller listelendi."));
        }

        [HttpGet]
        [Route("GetPassive")]
        public async Task<IActionResult> GetPassiveAsync()
        {
            List<Role> roles = await _roleService.GetPassiveAsync();

            List<RoleListDto> roleListDtos = _mapper.Map<List<Role>, List<RoleListDto>>(roles);

            return CreateActionResult(CustomResponseDto<List<RoleListDto>>.Success(200, roleListDtos, "Passive Roller listelendi."));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Role> roles = await _roleService.GetAllAsync();

            List<RoleListDto> roleListDtos = _mapper.Map<List<Role>, List<RoleListDto>>(roles);

            return CreateActionResult(CustomResponseDto<List<RoleListDto>>.Success(200, roleListDtos, "Tüm Roller listelendi."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            Role role = await _roleService.GetByIDAsync(id);

            RoleDto roleDto = _mapper.Map<Role, RoleDto>(role);

            return CreateActionResult(CustomResponseDto<RoleDto>.Success(200, roleDto, $"{id} li Rol listelendi."));
        }

        [HttpPost]
        public IActionResult Add(RoleAddDto roleAddDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Role role =_mapper.Map<RoleAddDto,Role>(roleAddDto);

            _roleService.Add(role);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Ekleme İşlemi Başarılı"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync(RoleUpdateDto roleUpdateDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Role role = _mapper.Map<RoleUpdateDto, Role>(roleUpdateDto);

            await _roleService.UpdateAsync(role);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Günceleme İşlemi Başarılı"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Log.Information($"{User.Identity?.Name}: Add a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            _roleService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme İşlemi Başarılı"));
        }



    }
}

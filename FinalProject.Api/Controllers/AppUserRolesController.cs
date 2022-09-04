using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FinalProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserRolesController : CustomBaseController
    {
        private readonly IAppUserRoleService _appUserRoleService;
        private readonly IMapper _mapper;

        public AppUserRolesController(IAppUserRoleService appUserRoleService, IMapper mapper)
        {
            _appUserRoleService = appUserRoleService;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<AppUserRole> appUserRoles = await _appUserRoleService.GetActiveAsync();

            List<AppUserRoleListDto> appUserRoleListDtos = _mapper.Map<List<AppUserRole>, List<AppUserRoleListDto>>(appUserRoles);

            return CreateActionResult(CustomResponseDto<List<AppUserRoleListDto>>.Success(200, appUserRoleListDtos, "Active Kullanıcı Rolleri listelendi."));
        }

        [HttpGet]
        [Route("GetPassive")]
        public async Task<IActionResult> GetPassiveAsync()
        {
            List<AppUserRole> appUserRoles = await _appUserRoleService.GetPassiveAsync();

            List<AppUserRoleListDto> appUserRoleListDtos = _mapper.Map<List<AppUserRole>, List<AppUserRoleListDto>>(appUserRoles);

            return CreateActionResult(CustomResponseDto<List<AppUserRoleListDto>>.Success(200, appUserRoleListDtos, "Passive Kullanıcı Rolleri listelendi."));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<AppUserRole> appUserRoles = await _appUserRoleService.GetAllAsync();

            List<AppUserRoleListDto> appUserRoleListDtos = _mapper.Map<List<AppUserRole>, List<AppUserRoleListDto>>(appUserRoles);

            return CreateActionResult(CustomResponseDto<List<AppUserRoleListDto>>.Success(200, appUserRoleListDtos, "Tüm Kullanıcı Rolleri listelendi."));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            AppUserRole appUserRole = await _appUserRoleService.GetByIDAsync(id);

            AppUserRoleDto appUserRoleDto = _mapper.Map<AppUserRole, AppUserRoleDto>(appUserRole);

            return CreateActionResult(CustomResponseDto<AppUserRoleDto>.Success(200, appUserRoleDto, $"{id} li Kullanıcı Rolu listelendi."));
        }

        [HttpPost]
        public IActionResult Add(AppUserRoleAddDto appUserRoleAddDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            AppUserRole appUserRole = _mapper.Map<AppUserRoleAddDto, AppUserRole>(appUserRoleAddDto);

            _appUserRoleService.Add(appUserRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Ekleme İşlemi Başarılı"));
        }


        [HttpPut]
        public IActionResult Update(AppUserRoleUpdateDto appUserRoleUpdateDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            AppUserRole appUserRole = _mapper.Map<AppUserRoleUpdateDto, AppUserRole>(appUserRoleUpdateDto);

            _appUserRoleService.UpdateAsync(appUserRole);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Güncelleme İşlemi Başarılı"));
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Log.Information($"{User.Identity?.Name}: Add a Color  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            _appUserRoleService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme İşlemi Başarılı"));
        }
    }
}

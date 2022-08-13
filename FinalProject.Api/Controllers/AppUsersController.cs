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
    public class AppUsersController : CustomBaseController
    {

        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AppUsersController(IAppUserService appUserService, IMapper mapper)
        {
            _appUserService = appUserService;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<AppUser> appUsers = await _appUserService.GetAllAsync();

            List<AppUserListDto> appUserListDtos = _mapper.Map<List<AppUser>, List<AppUserListDto>>(appUsers);

            return CreateActionResult(CustomResponseDto<List<AppUserListDto>>.Success(200,appUserListDtos,"Tüm Kulanıcılar Listelendi"));
        }

        [HttpGet]
        [Route("GetActive")]
        public async Task<IActionResult> GetActive()
        {
            List<AppUser> appUsers = await _appUserService.GetActiveAsync();

            List<AppUserListDto> appUserListDtos = _mapper.Map<List<AppUser>, List<AppUserListDto>>(appUsers);

            return CreateActionResult(CustomResponseDto<List<AppUserListDto>>.Success(200, appUserListDtos, "Active Kulanıcılar Listelendi"));
        }

        [HttpGet]
        [Route("GetPassive")]
        public async Task<IActionResult> GetPassive()
        {
            List<AppUser> appUsers = await _appUserService.GetPassiveAsync();

            List<AppUserListDto> appUserListDtos = _mapper.Map<List<AppUser>, List<AppUserListDto>>(appUsers);

            return CreateActionResult(CustomResponseDto<List<AppUserListDto>>.Success(200, appUserListDtos, "Passive Kulanıcılar Listelendi"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByID(int id)
        {
            AppUser appUser = await _appUserService.GetByIDAsync(id);

            AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(appUser);

            return CreateActionResult(CustomResponseDto<AppUserDto>.Success(200, appUserDto, $"{id} numaralı Kullanıcı Listelendi"));
        }
    }
}

using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public async Task<IActionResult> GetByID([FromRoute] int id)
        {
            AppUser appUser = await _appUserService.GetByIDAsync(id);

            AppUserDto appUserDto = _mapper.Map<AppUser, AppUserDto>(appUser);

            return CreateActionResult(CustomResponseDto<AppUserDto>.Success(200, appUserDto, $"{id} numaralı Kullanıcı Listelendi"));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody]AppUserRegisterDto registerDto)
        {
            AppUser appUser =await _appUserService.RegisterAsync(registerDto);

            AccessToken token = _appUserService.CreateAccessToken(appUser);

            return CreateActionResult(CustomResponseDto<AccessToken>.Success(200, token, "Kayıt Başarılı Token olışturuldu"));
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] AppUserLoginDto loginDto)
        {
            AppUser appUser =await _appUserService.LoginAsync(loginDto);

            AccessToken token = _appUserService.CreateAccessToken(appUser);

            ProducerService.Producer(appUser);

            return CreateActionResult(CustomResponseDto<AccessToken>.Success(200, token, "Giriş Başarılı Token olışturuldu"));
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] AppUserUpdateDto updateDto)
        {
            AppUser appUser = _mapper.Map<AppUserUpdateDto, AppUser>(updateDto);

            //user claimlerinden AppUserId'nin degerini alıyoruz.
            string userId = (User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value;

            appUser.ID = int.Parse(userId);
           await _appUserService.UpdateAsync(appUser);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Gücelleme İşlemi Başarılı"));
        }


        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            _appUserService.Delete(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Silme işlemi Başarılı"));
        }

        [HttpPost]
        [Route("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] AppUserPasswordUpdateDto passwordUpdateDto)
        {
            if (!CheckPassword.CheckingPassword(passwordUpdateDto.NewPassword, passwordUpdateDto.ConfirmPassword))
                return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(400,"Girilen Şifreler Uyuşmuyor"));

            //user claimlerinden AppUserId'nin degerini alıyoruz.
            string userId = (User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value;

            await _appUserService.UpdatePasswordAsync(int.Parse(userId), passwordUpdateDto);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Şifre Başarıyla Degiştirildi"));
        }
    }
}

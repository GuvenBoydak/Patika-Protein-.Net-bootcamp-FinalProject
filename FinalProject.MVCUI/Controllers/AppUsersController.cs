using FinalProject.Base;
using FinalProject.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace FinalProject.MVCUI.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly AppUserApiService _appUserApiService;


        public AppUsersController(AppUserApiService appUserApiService)
        {
            _appUserApiService = appUserApiService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginDto appUserLoginDto)
        {
            string token =await _appUserApiService.LoginAsync(appUserLoginDto);

            HttpContext.Session.SetString("token", DeserialezeToken(token));

            return RedirectToAction("Index","Products");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDto appUserRegisterDto)
        {
            await _appUserApiService.RegisterAsync(appUserRegisterDto);

            return RedirectToAction("Login");
        }
 






        [NonAction]
        private string DeserialezeToken(string json)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            CustomResponseDto<AccessToken> result = JsonSerializer.Deserialize<CustomResponseDto<AccessToken>>(json, options);

            return result.Data.Token;
        }
    }



}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AppUsersController : Controller
    {
        private readonly AppUserApiService _appUserApiService;
        private readonly OfferApiService _offerApiService;
        private readonly BrandApiService _brandApiService;
        private readonly ColorApiService _colorApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly ProductApiService _productApiService;


        public AppUsersController(AppUserApiService appUserApiService, OfferApiService offerApiService, BrandApiService brandApiService, ColorApiService colorApiService, CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _appUserApiService = appUserApiService;
            _offerApiService = offerApiService;
            _brandApiService = brandApiService;
            _colorApiService = colorApiService;
            _categoryApiService = categoryApiService;
            _productApiService = productApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");
            string email = HttpContext.Session.GetString("User");

            AppUserVM vM = new AppUserVM
            {
                AppUser = await _appUserApiService.GetByEmailAsync(token, email),
                Categories = await _categoryApiService.GetActiveAsync(token),
                Brands = await _brandApiService.GetActiveAsync(token),
                Colors = await _colorApiService.GetActiveAsync(token),
                Products = await _appUserApiService.GetAppUserProductsAsync(token)
            };
            return View(vM);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginModel appUserLoginModel)
        {
            HttpResponseMessage result = await _appUserApiService.LoginAsync(appUserLoginModel);

            CustomResponseModel<AccessToken> response = await DeserialezeToken(result);

            if (response.StatusCode != 200)
            {
                ViewBag.FailPassword = response.Error.FirstOrDefault();
                return View();
            }

            AppUserModel appUser = await _appUserApiService.GetByEmailAsync(response.Data.Token, appUserLoginModel.Email);

            List<Role> roles = await _appUserApiService.GetRolesAsync(response.Data.Token, appUser.ID);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,appUser.UserName),
                new Claim(ClaimTypes.Email,appUser.Email)
            };

            foreach (Role item in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, item.Name));
            }

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

            HttpContext.Session.SetString("token", response.Data.Token);

            HttpContext.Session.SetString("User", appUserLoginModel.Email);

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterModel appUserRegisterModel)
        {
            HttpResponseMessage result = await _appUserApiService.RegisterAsync(appUserRegisterModel);

            CustomResponseModel<AccessToken> response = await DeserialezeToken(result);

            if (response.StatusCode != 200)
            {
                ViewBag.FailRegister = response.Error.FirstOrDefault();
                return View();
            }

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult LogOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> GetByProductsOffer(int id)
        {
            string token = HttpContext.Session.GetString("token");
            string email = HttpContext.Session.GetString("User");

            AppUserVM vM = new AppUserVM
            {
                Offers = await _offerApiService.GetByOffersProductIDAsync(token, id),
                Products =await _productApiService.GetActiveProductsAsync(token),
                AppUser =await _appUserApiService.GetByEmailAsync(token, email),
            };

            return View(vM);
        }

        [HttpGet]
        public async Task<IActionResult> Profile()
        {
            string email = HttpContext.Session.GetString("User");
            string token = HttpContext.Session.GetString("token");

            AppUserVM vM = new AppUserVM
            {
                AppUser = await _appUserApiService.GetByEmailAsync(token, email)
            };

            return View(vM);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile(int id)
        {
            string token = HttpContext.Session.GetString("token");

            AppUserVM vM = new AppUserVM
            {
                AppUser = await _appUserApiService.GetByIDAsync(token, id)
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUserModel AppUser)
        {
            if ((int)TempData["ID"] == AppUser.ID)
            {
                string token = HttpContext.Session.GetString("token");

                bool result = await _appUserApiService.UpdateAsync(token, AppUser);
                if (!result)
                {
                    ViewBag.FailUpdate = "Güncelleme İşlemi Başarısız.";
                    return View();
                }
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View();
        }


        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            TempData["ID"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AppUserPasswordUpdateModel appUserPasswordUpdateModel)
        {
            if ((int)TempData["ID"] == appUserPasswordUpdateModel.ID)
            {
                string token = HttpContext.Session.GetString("token");

                bool result = await _appUserApiService.ChangePasswordAsync(token, appUserPasswordUpdateModel);
                if (!result)
                {
                    ViewBag.FailUpdate = "Şifre Güncelleme işlemi Başarısız. ";
                    return View();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> GetByAppUserOffers(int id)
        {
            string token = HttpContext.Session.GetString("token");
            string email = HttpContext.Session.GetString("User");

            AppUserVM vM = new AppUserVM
            {
                Offers = await _offerApiService.GetByAppUserOffersAsync(token, id),
                Products = await _productApiService.GetActiveProductsAsync(token),
                AppUser = await _appUserApiService.GetByEmailAsync(token, email)
            };

            return View(vM);
        }

        [NonAction]
        private async Task<CustomResponseModel<AccessToken>> DeserialezeToken(HttpResponseMessage json)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            string jsonResult = await json.Content.ReadAsStringAsync();

            CustomResponseModel<AccessToken> result = JsonSerializer.Deserialize<CustomResponseModel<AccessToken>>(jsonResult, options);
            result.StatusCode = (int)json.StatusCode;

            return result;
        }
    }
}

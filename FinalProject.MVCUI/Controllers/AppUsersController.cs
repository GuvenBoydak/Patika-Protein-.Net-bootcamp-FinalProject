using AutoMapper;
using FinalProject.Base;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text.Json;

namespace FinalProject.MVCUI.Controllers
{
    public class AppUsersController : Controller
    {
        private readonly AppUserApiService _appUserApiService;
        private readonly OfferApiService _offerApiService;
        private readonly BrandApiService _brandApiService;
        private readonly ColorApiService _colorApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly ProductApiService _productApiService;
        private readonly IMapper _mapper;


        public AppUsersController(AppUserApiService appUserApiService, IMapper mapper, OfferApiService offerApiService, BrandApiService brandApiService, ColorApiService colorApiService, CategoryApiService categoryApiService, ProductApiService productApiService)
        {
            _appUserApiService = appUserApiService;
            _mapper = mapper;
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
                AppUser = _mapper.Map<AppUserDto, AppUser>(await _appUserApiService.GetByEmailAsync(token, email)),
                Categories = _mapper.Map<List<CategoryListDto>, List<Category>>(await _categoryApiService.GetActiveAsync(token)),
                Brands = _mapper.Map<List<BrandListDto>, List<Brand>>(await _brandApiService.GetActiveAsync(token)),
                Colors = _mapper.Map<List<ColorListDto>, List<Color>>(await _colorApiService.GetActiveAsync(token)),
                Products = _mapper.Map<List<AppUserProductsDto>, List<Product>>(await _appUserApiService.GetAppUserProductsAsync(token))
            };
            return View(vM);
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

            HttpContext.Session.SetString("User", appUserLoginDto.Email);
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

        [HttpGet]
        public async Task<IActionResult> GetByProductsOffer(int id)
        {
            string token = HttpContext.Session.GetString("token");
            string email = HttpContext.Session.GetString("User");

            AppUserVM vM = new AppUserVM
            {
                Offers = _mapper.Map<List<ProductOffersListDto>, List<Offer>>(await _offerApiService.GetByOffersProductIDAsync(token, id)),
                Products = _mapper.Map<List<ProductListDto>, List<Product>>(await _productApiService.GetActiveProductsAsync(token)),
                AppUser = _mapper.Map<AppUserDto, AppUser>(await _appUserApiService.GetByEmailAsync(token, email)),
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
                AppUser = _mapper.Map<AppUserDto, AppUser>(await _appUserApiService.GetByEmailAsync(token, email))
            };

            return View(vM);
        }

        [HttpGet] 
        public async Task<IActionResult> UpdateProfile(int id)
        {
            string token = HttpContext.Session.GetString("token");

            AppUserVM vM = new AppUserVM
            {
                AppUser = _mapper.Map<AppUserDto, AppUser>(await _appUserApiService.GetByIDAsync(token, id))
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(AppUser appUser)
        {
            if ((int)TempData["ID"] == appUser.ID)
            {
                string token = HttpContext.Session.GetString("token");
                AppUserUpdateDto appUserUpdateDto = _mapper.Map<AppUser, AppUserUpdateDto>(appUser);

                await _appUserApiService.UpdateAsync(token,appUserUpdateDto);
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View(appUser);
        }


        [HttpGet]
        public IActionResult ChangePassword(int id)
        {
            TempData["ID"] = id;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(AppUserPasswordUpdateDto appUserPasswordUpdateDto)
        {
            if ((int)TempData["ID"] == appUserPasswordUpdateDto.ID)
            {
                string token = HttpContext.Session.GetString("token");

                await _appUserApiService.ChangePasswordAsync(token, appUserPasswordUpdateDto);

                return RedirectToAction("Index");
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View(appUserPasswordUpdateDto);
        }


        [HttpGet]
        public async Task<IActionResult> GetByAppUserOffers(int id)
        {
            string token = HttpContext.Session.GetString("token");

            AppUserVM vM = new AppUserVM
            {
                Offers = _mapper.Map<List<OfferListDto>, List<Offer>>(await _offerApiService.GetByAppUserOffersAsync(token, id)),
                Products= _mapper.Map<List<ProductListDto>,List<Product>>(await _productApiService.GetActiveProductsAsync(token))
            };

            return View(vM);
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

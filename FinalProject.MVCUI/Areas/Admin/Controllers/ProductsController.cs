using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly BrandApiService _brandApiService;
        private readonly ColorApiService _colorApiService;
        private readonly CategoryApiService _categoryApiService;
        private readonly AppUserApiService _appUserApiService;

        public ProductsController(ProductApiService productApiService, CategoryApiService categoryApiService, BrandApiService brandApiService, ColorApiService colorApiService, AppUserApiService appUserApiService)
        {
            _productApiService = productApiService;
            _categoryApiService = categoryApiService;
            _brandApiService = brandApiService;
            _colorApiService = colorApiService;
            _appUserApiService = appUserApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {
            string token = HttpContext.Session.GetString("token");

            List<ProductModel> products = await _productApiService.GetActiveProductsAsync(token);

            ProductVM vM = new ProductVM
            {
                Products = id != null ? products.Where(x => x.CategoryID == id).ToList() : products,
                Categories = await _categoryApiService.GetActiveAsync(token)
            };

            return View(vM);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            string token = HttpContext.Session.GetString("token");
            string userEmail = HttpContext.Session.GetString("User");

            ProductModel product = await _productApiService.GetByIDAsync(id, token);

            ProductVM vM = new ProductVM
            {
                PriceList = PriceList(await _productApiService.GetByIDAsync(id, token)),
                Product = await _productApiService.GetByIDAsync(id, token),
                Category = await _categoryApiService.GetByIDAsync(product.CategoryID, token),
                AppUser = await _appUserApiService.GetByEmailAsync(token, userEmail)
            };

            return View(vM);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string token = HttpContext.Session.GetString("token");

            ProductVM vM = new ProductVM
            {
                Categories =await _categoryApiService.GetActiveAsync(token),
                Brands =await _brandApiService.GetActiveAsync(token),
                Colors =await _colorApiService.GetActiveAsync(token)
            };
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddWithFileModel productAddWithFileModel)
        {
            string token = HttpContext.Session.GetString("token");

            await _productApiService.AddAsync(productAddWithFileModel, token);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            ProductVM vM = new ProductVM
            {
                Categories = await _categoryApiService.GetActiveAsync(token),
                Brands = await _brandApiService.GetActiveAsync(token),
                Colors = await _colorApiService.GetActiveAsync(token),
                Product =await _productApiService.GetByIDAsync(id, token)
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductModel product, IFormFile file)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"] == product.ID)
            {
                await _productApiService.UpdateAsync(product, file, token);

                return RedirectToAction("Index");
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View();
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _productApiService.DeleteAsync(id, token);
            if (!result)
            {
                ViewBag.FailDelete = "Ürün silme İşlmemi Başarısız.";
                ProductVM vM = new ProductVM
                {
                    Product =await _productApiService.GetByIDAsync(id, token)
                };
                return RedirectToAction("ProductDetail", "Products", vM);
            }

            return RedirectToAction("Index");
        }

        private List<SelectListItem> PriceList(ProductModel product)
        {
            List<SelectListItem> priceList = new List<SelectListItem>();
            for (int i = 10; i <= 100; i += 10)
            {
                decimal priceCheck = (decimal)(product.UnitPrice * i / 100);
                priceList.Add(new SelectListItem() { Text = $"%{i} : ₺{priceCheck}", Value = priceCheck.ToString() });
            }

            return priceList;
        }
    }
}

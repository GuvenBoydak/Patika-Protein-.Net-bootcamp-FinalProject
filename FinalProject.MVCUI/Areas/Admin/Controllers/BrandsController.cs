using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Authorize()]
    [Area("Admin")]
    public class BrandsController : Controller
    {

        private readonly BrandApiService _brandApiService;

        public BrandsController(BrandApiService brandApiService)
        {
            _brandApiService = brandApiService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            BrandVM vM = new BrandVM
            {
                Brands =await _brandApiService.GetActiveAsync(token)
            };
            return View(vM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BrandModel brandModel)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _brandApiService.AddAsync(token, brandModel);
            if (!result)
            {
                ViewBag.FailAdd = "Ekleme işlemi Başarısız.";
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            BrandVM vM = new BrandVM
            {
                Brand = await _brandApiService.GetByIDAsync(id, token)
            };

            TempData["ID"] = id;

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandModel brandModel)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"] == brandModel.ID)
            {
                bool result = await _brandApiService.UpdateAsync(token, brandModel);

                if (!result)
                {
                    ViewBag.FailUpdate = "Güncelleme İşlemi Başarısız";
                    return View();
                }

                return RedirectToAction("Index");
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _brandApiService.DeleteAsync(token, id);
            if (!result)
                ViewBag.FailDelete = "Silme İşlemi Başarısız";

            return RedirectToAction("Index");
        }
    }
}

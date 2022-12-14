using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly ColorApiService _colorApiService;

        public ColorsController(ColorApiService colorApiService)
        {
            _colorApiService = colorApiService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            ColorVM vM = new ColorVM
            {
                Colors = await _colorApiService.GetActiveAsync(token)
            };

            return View(vM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ColorModel color)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _colorApiService.AddAsync(token, color);

            if (!result)
            {
                ViewBag.FailAdd = "Ekleme İşlemi Başarısız";
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            ColorVM vM = new ColorVM
            {
                Color =await _colorApiService.GetByIDAsync(token, id)
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ColorModel color)
        {
            if ((int)TempData["ID"] == color.ID)
            {
                string token = HttpContext.Session.GetString("token");

                bool result = await _colorApiService.UpdateAsync(token, color);

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

            bool result = await _colorApiService.DeleteAsync(token, id);

            if (!result)
                ViewBag.FailDelete = "Silme İşlemi Başarısız";

            return RedirectToAction("Index");
        }
    }
}

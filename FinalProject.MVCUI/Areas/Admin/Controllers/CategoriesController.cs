using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly CategoryApiService _categoryApiService;

        public CategoriesController(CategoryApiService categoryApiService)
        {
            _categoryApiService = categoryApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            CategoryVM vM = new CategoryVM()
            {
                Categories = await _categoryApiService.GetActiveAsync(token)
            };

            return View(vM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryModel categoryModel)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _categoryApiService.AddAsync(token, categoryModel);
            if (!result)
            {
                ViewBag.FailAdd = "Ekleme İşlemi Başarsız.";
                return View();
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            CategoryVM vM = new CategoryVM()
            {
                Category = await _categoryApiService.GetByIDAsync(id, token)
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryModel categoryModel)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"] == categoryModel.ID)
            {
                bool result = await _categoryApiService.UpdateAsync(token, categoryModel);
                if (!result)
                {
                    ViewBag.FailUpdate = "Güncelleme İşlemi Başarsız.";
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

            bool result = await _categoryApiService.DeleteAsync(token, id);
            if (!result)
                ViewBag.FailDelete = "Silme İşlemi Başarsız.";

            return RedirectToAction("Index");
        }
    }
}

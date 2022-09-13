using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleApiService _roleApiService;

        public RolesController(RoleApiService roleApiService)
        {
            _roleApiService = roleApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            RoleVM vm = new RoleVM
            {
                Roles = await _roleApiService.GetActiveAsync(token)
            };

            return View(vm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(RoleModel Role)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _roleApiService.AddAsync(token, Role);
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

            RoleVM vM = new RoleVM
            {
                Role = await _roleApiService.GetByIDAsync(token,id)
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(RoleModel role)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"] == role.ID)
            {
                bool result = await _roleApiService.UpdateAsync(token, role);
                if (!result)
                {
                    ViewBag.FailUpdate = "Güncelleme İşlemi Başarısız";
                    return View();
                }
                return RedirectToAction("Index");
            };

            ViewBag.Fail = "Girilen ID Yanlış";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _roleApiService.DeleteAsync(token, id);
            if (!result)
                ViewBag.FailDelete = "Silme İşlemi Başarısız";

            return RedirectToAction("Index");
        }


    }
}

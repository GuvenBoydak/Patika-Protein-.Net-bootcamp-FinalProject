using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class AppUserRolesController : Controller
    {
        private readonly AppUserRoleApiService _appUserRoleApiService;
        private readonly RoleApiService _RoleApiService;
        private readonly AppUserApiService _appUserApiService;

        public AppUserRolesController(AppUserRoleApiService appUserRoleApiService, RoleApiService roleApiService, AppUserApiService appUserApiService)
        {
            _appUserRoleApiService = appUserRoleApiService;
            _RoleApiService = roleApiService;
            _appUserApiService = appUserApiService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            AppUserRoleVM vm = new AppUserRoleVM
            {
                AppUserRoles = await _appUserRoleApiService.GetActiveAsync(token),
                AppUsers = await _appUserApiService.GetActiveAsync(token),
                Roles = await _RoleApiService.GetActiveAsync(token)
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string token = HttpContext.Session.GetString("token");

            AppUserRoleVM vm = new AppUserRoleVM
            {
                AppUsers = await _appUserApiService.GetActiveAsync(token),
                Roles = await _RoleApiService.GetActiveAsync(token)
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AppUserRoleModel appUserRole)
        {
            string token = HttpContext.Session.GetString("token");

            bool result = await _appUserRoleApiService.AddAsync(token,appUserRole);
            if(!result)
            {
                ViewBag.FailAdd= "Ekleme işlemi Başarısız.";
                return View();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            AppUserRoleVM vM = new AppUserRoleVM
            {
                AppUserRole = await _appUserRoleApiService.GetByIDAsync(token, id),
                AppUsers = await _appUserApiService.GetActiveAsync(token),
                Roles = await _RoleApiService.GetActiveAsync(token)
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AppUserRoleModel appUserRole)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"] == appUserRole.ID)
            {
                bool result = await _appUserRoleApiService.UpdateAsync(token, appUserRole);
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

            bool result =await _appUserRoleApiService.DeleteAsync(token, id);
            if(!result)
                ViewBag.FailDelete = "Silme İşlemi Başarısız";

            return RedirectToAction("Index");
        }

    }
}

using AutoMapper;
using FinalProject.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly CategoryApiService _categoryApiService;
        private readonly IMapper _mapper;

        public CategoriesController(CategoryApiService categoryApiService, IMapper mapper)
        {
            _categoryApiService = categoryApiService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            CategoryVM vM = new CategoryVM()
            {
                CategoryListDtos = await _categoryApiService.GetActiveAsync(token)
            };

            return View(vM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(CategoryAddDto categoryAddDto)
        {
            string token = HttpContext.Session.GetString("token");

           bool result= await _categoryApiService.AddAsync(token, categoryAddDto);
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
                CategoryUpdateDto =_mapper.Map<CategoryDto,CategoryUpdateDto>(await _categoryApiService.GetByIDAsync(id, token))
            };

            TempData["ID"] = id;

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateDto categoryUpdateDto)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"]==categoryUpdateDto.ID)
            {
               bool result= await _categoryApiService.UpdateAsync(token, categoryUpdateDto);
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

            bool result =await _categoryApiService.DeleteAsync(token,id);
            if (!result)
                ViewBag.FailDelete = "Silme İşlemi Başarsız.";

            return RedirectToAction("Index");
        }


    }
}

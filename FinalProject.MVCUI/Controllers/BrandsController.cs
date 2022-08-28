using AutoMapper;
using FinalProject.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Controllers
{
    public class BrandsController : Controller
    {

        private readonly BrandApiService _brandApiService;
        private readonly IMapper _mapper;

        public BrandsController(BrandApiService brandApiService, IMapper mapper)
        {
            _brandApiService = brandApiService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            BrandVM vM = new BrandVM
            {
                BrandListDtos = await _brandApiService.GetActiveAsync(token)
            };
            return View(vM);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(BrandAddDto brandAddDto)
        {
            string token = HttpContext.Session.GetString("token");

            await _brandApiService.AddAsync(token, brandAddDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            BrandVM vM = new BrandVM
            {
                BrandUpdateDto =_mapper.Map<BrandDto,BrandUpdateDto>(await _brandApiService.GetByIDAsync(id, token))
            };

            TempData["ID"] = id;

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BrandUpdateDto brandUpdateDto)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"] == brandUpdateDto.ID)
            {
                await _brandApiService.UpdateAsync(token,brandUpdateDto);
                return RedirectToAction("Index");
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View(brandUpdateDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

            await _brandApiService.DeleteAsync(token,id);

            return RedirectToAction("Index");
        }
    }
}

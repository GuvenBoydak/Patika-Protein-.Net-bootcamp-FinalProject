using AutoMapper;
using FinalProject.DTO;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Controllers
{
    public class ColorsController : Controller
    {
        private readonly ColorApiService _colorApiService;
        private readonly IMapper _mapper;

        public ColorsController(ColorApiService colorApiService, IMapper mapper)
        {
            _colorApiService = colorApiService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            ColorVM vM = new ColorVM
            {
                ColorListDtos = await _colorApiService.GetActiveAsync(token)
            };

            return View(vM);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ColorAddDto colorAddDto)
        {
            string token = HttpContext.Session.GetString("token");

            await _colorApiService.AddAsync(token,colorAddDto);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            ColorVM vM = new ColorVM
            {
                ColorUpdateDto = _mapper.Map<ColorDto, ColorUpdateDto>(await _colorApiService.GetByIDAsync(token, id))
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ColorUpdateDto colorUpdateDto)
        {
            if ((int)TempData["ID"] == colorUpdateDto.ID)
            {
                string token = HttpContext.Session.GetString("token");

                await _colorApiService.UpdateAsync(token, colorUpdateDto);
                return RedirectToAction("Index");
            }


            ViewBag.Fail = "Girilen ID Yanlış";
            return View(colorUpdateDto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

            await _colorApiService.DeleteAsync(token,id);

            return RedirectToAction("Index");
        }

    }
}

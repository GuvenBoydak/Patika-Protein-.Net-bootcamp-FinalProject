using AutoMapper;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinalProject.MVCUI.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductApiService _productApiService;
        private readonly BrandApiService _brandApiService;
        private readonly ColorApiService _colorApiService;
        private readonly CategoryApiService _categoryApiService;
        
        private readonly IMapper _mapper;

        public ProductsController(ProductApiService productApiService, IMapper mapper, CategoryApiService categoryApiService, BrandApiService brandApiService, ColorApiService colorApiService)
        {
            _productApiService = productApiService;
            _mapper = mapper;
            _categoryApiService = categoryApiService;
            _brandApiService = brandApiService;
            _colorApiService = colorApiService;
        }
        [HttpGet]
        public async Task<IActionResult> Index(int? id)
        {          
            string token = HttpContext.Session.GetString("token");

            List<ProductListDto> products = await _productApiService.GetActiveProductsAsync(token);
            List<CategoryListDto> categories = await _categoryApiService.GetActiveAsync(token);

            ProductsVM vM = new ProductsVM
            {
                Products = _mapper.Map<List<ProductListDto>, List<Product>>(id != null ? products.Where(x=>x.CategoryID==id).ToList() : products),
                Categories = _mapper.Map<List<CategoryListDto>,List<Category>>(categories)        
            };

            return View(vM);
        }

        [HttpGet]
        public async Task<IActionResult> ProductDetail(int id)
        {
            string token = HttpContext.Session.GetString("token");

            ProductDto product = await _productApiService.GetByIDAsync(id, token);

            ProductsVM vM = new ProductsVM
            {
                PriceList = PriceList(await _productApiService.GetByIDAsync(id, token)),
                Product = _mapper.Map<ProductDto, Product>(await _productApiService.GetByIDAsync(id, token)),
                Category = await _categoryApiService.GetByIDAsync(product.CategoryID, token)
            };

            return View(vM);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            string token = HttpContext.Session.GetString("token");

            ProductsVM vM = new ProductsVM()
            {
                Categories = _mapper.Map<List<CategoryListDto>, List<Category>>(await _categoryApiService.GetActiveAsync(token)),
                Brands = _mapper.Map<List<BrandListDto>, List<Brand>>(await _brandApiService.GetActiveAsync(token)),
                Colors = _mapper.Map<List<ColorListDto>, List<Color>>(await _colorApiService.GetActiveAsync(token))
            };
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ProductAddDto productAddDto)
        {
            string token = HttpContext.Session.GetString("token");

            await _productApiService.AddAsync(productAddDto, token);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            ProductsVM vM = new ProductsVM()
            {
                Categories = _mapper.Map<List<CategoryListDto>, List<Category>>(await _categoryApiService.GetActiveAsync(token)),
                Brands = _mapper.Map<List<BrandListDto>, List<Brand>>(await _brandApiService.GetActiveAsync(token)),
                Colors = _mapper.Map<List<ColorListDto>, List<Color>>(await _colorApiService.GetActiveAsync(token)),
                Product = _mapper.Map<ProductDto, Product>(await _productApiService.GetByIDAsync(id,token))
            };

            TempData["ID"] = id;
            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateDto productUpdateDto)
        {
            string token = HttpContext.Session.GetString("token");

            if ((int)TempData["ID"] == productUpdateDto.ID)
            {
                await _productApiService.UpdateAsync(productUpdateDto, token);

                return RedirectToAction("Index");
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View(productUpdateDto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

           bool result= await _productApiService.DeleteAsync(id,token);
            if (!result)
            {
                ViewBag.FailDelete = "Ürün silme İşlmemi Başarısız.";
                ProductsVM vM = new ProductsVM
                {
                    Product = _mapper.Map<ProductDto, Product>(await _productApiService.GetByIDAsync(id, token))
                };
                return RedirectToAction("ProductDetail", "Products", vM);
            }

            return RedirectToAction("Index");
        }

        private List<SelectListItem> PriceList(ProductDto product)
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

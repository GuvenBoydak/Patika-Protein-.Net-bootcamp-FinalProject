using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OffersController : Controller
    {
        private readonly OfferApiService _offerApiService;
        private readonly ProductApiService _productApiService;
        private readonly AppUserApiService _appUserApiService;

        public OffersController(OfferApiService offerApiService, ProductApiService productApiService, AppUserApiService appUserApiService)
        {
            _offerApiService = offerApiService;
            _productApiService = productApiService;
            _appUserApiService = appUserApiService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            OfferVM vM = new OfferVM
            {
                Offers =await _offerApiService.GetActiveAsync(token),
                AppUsers =await _appUserApiService.GetActiveAsync(token),
                Products = await _productApiService.GetActiveProductsAsync(token)
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(OfferModel color, string process)
        {
            string token = HttpContext.Session.GetString("token");

            ProductModel product = await _productApiService.GetByIDAsync(color.ProductID, token);

            if (process == "MakeOffer" && product.IsOfferable == true)
            {
                bool result = await _offerApiService.AddAsync(token, color);
                if (!result)
                {
                    ViewBag.FailAdd = "Teklif Ekleme işlemi başarısız.";
                    return RedirectToAction("ProductDetail", "Products");
                }
            }
            else if (process == "BuyProduct" && product.IsSold == false)
            {
                color.IsApproved = true;

                bool result = await _offerApiService.BuyProductAsync(token, color);
                if (!result)
                {
                    ViewBag.FailBuyProduct = "Satın Alma işlemi başarısız.";
                    return RedirectToAction("ProductDetail", "Products");
                }
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            OfferVM vM = new OfferVM
            {
                Offer = await _offerApiService.GetByIDAsync(token, id)
            };

            TempData["ID"] = id;
            return View(vM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(OfferModel color)
        {
            string token = HttpContext.Session.GetString("token");
            if ((int)TempData["ID"] == color.ID)
            {
                bool result = await _offerApiService.UpdateAsync(token, color);
                if (!result)
                {
                    ViewBag.FailUpdate = "Güncelleme işlemi başarısız.";
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

            bool result = await _offerApiService.DeleteAsync(token, id);
            if (!result)
                ViewBag.FailDelete = "Silme işlemi başarısız.";

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> OfferApproval(int id)
        {
            string token = HttpContext.Session.GetString("token");

            OfferVM vM = new OfferVM
            {
                Offer = await _offerApiService.GetByIDAsync(token, id)
            };

            vM.Offer.IsApproved = true;

            bool result = await _offerApiService.OfferApprovalAsync(token,vM.Offer);
            if (!result)
            {
                ViewBag.FailOfferApproval = "Teklif Onaylama işlemi başarısız.";
                return RedirectToAction("GetByProductsOffer", "AppUsers");
            }

           return RedirectToAction("Index");
        }
    }
}

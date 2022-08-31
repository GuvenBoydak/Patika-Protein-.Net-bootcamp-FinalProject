using AutoMapper;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FinalProject.MVCUI.Controllers
{
    public class OffersController : Controller
    {
        private readonly OfferApiService _offerApiService;
        private readonly ProductApiService _productApiService;
        private readonly AppUserApiService _appUserApiService;
        private readonly IMapper _mapper;

        public OffersController(OfferApiService offerApiService, IMapper mapper, ProductApiService productApiService, AppUserApiService appUserApiService)
        {
            _offerApiService = offerApiService;
            _mapper = mapper;
            _productApiService = productApiService;
            _appUserApiService = appUserApiService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("token");

            OfferVM vM = new OfferVM
            {
                Offers =_mapper.Map<List<OfferListDto>, List<Offer>>(await _offerApiService.GetActiveAsync(token)),
                AppUsers = _mapper.Map<List<AppUserListDto>, List<AppUser>>(await _appUserApiService.GetActiveAsync(token)),
                Products = _mapper.Map<List<ProductListDto>, List<Product>>(await _productApiService.GetActiveProductsAsync(token))
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Offer offer, string process)
        {
            string token = HttpContext.Session.GetString("token");

            ProductDto product = await _productApiService.GetByIDAsync(offer.ProductID, token);

            if (process == "MakeOffer" && product.IsOfferable == true)
            {
                bool result = await _offerApiService.AddAsync(token, _mapper.Map<Offer, OfferAddDto>(offer));
                if (!result)
                {
                    ViewBag.FailAdd = "Teklif Ekleme işlemi başarısız.";
                    return RedirectToAction("ProductDetail", "Products");
                }
            }
            else if (process == "BuyProduct" && product.IsSold == false)
            {
                offer.IsApproved = true;

               bool result= await _offerApiService.BuyProductAsync(token, _mapper.Map<Offer, OfferBuyProductDto>(offer));
                if(!result)
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
                Offer = _mapper.Map<OfferDto, Offer>(await _offerApiService.GetByIDAsync(token, id))
            };

            TempData["ID"] = id;
            return View(vM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(Offer offer)
        {
            string token = HttpContext.Session.GetString("token");
            if ((int)TempData["ID"] == offer.ID)
            {
                OfferUpdateDto offerUpdateDto = _mapper.Map<Offer, OfferUpdateDto>(offer);

               bool result= await _offerApiService.UpdateAsync(token, offerUpdateDto);
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

           bool result= await _offerApiService.DeleteAsync(token, id);
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
                Offer = _mapper.Map<OfferDto, Offer>(await _offerApiService.GetByIDAsync(token, id))
            };

            vM.Offer.IsApproved = true;
            OfferApprovalDto offerApprovalDto = _mapper.Map<Offer, OfferApprovalDto>(vM.Offer);

           bool result= await _offerApiService.OfferApprovalAsync(token, offerApprovalDto);
            if (!result)
            {
                ViewBag.FailOfferApproval = "Teklif Onaylama işlemi başarısız.";
                return RedirectToAction("GetByProductsOffer","AppUsers");
            }


            return RedirectToAction("Index");
        }



    }
}

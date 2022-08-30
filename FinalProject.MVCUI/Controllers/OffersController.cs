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
                OfferListDtos = await _offerApiService.GetActiveAsync(token),
                AppUserListDtos = await _appUserApiService.GetActiveAsync(token),
                ProductListDtos = await _productApiService.GetActiveProductsAsync(token)
            };       

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Offer offer,string process)
        {
            string token = HttpContext.Session.GetString("token");

            ProductDto product = await _productApiService.GetByIDAsync(offer.ProductID,token);

            if(process=="MakeOffer" && product.IsOfferable == true)
            {
                await _offerApiService.AddAsync(token, _mapper.Map<Offer, OfferAddDto>(offer));
            }
            else if(process=="BuyProduct" && product.IsSold==false)
            {
                offer.IsApproved = true;

                await _offerApiService.BuyProductAsync(token, _mapper.Map<Offer, OfferBuyProductDto>(offer));
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            string token = HttpContext.Session.GetString("token");

            OfferVM vM = new OfferVM
            {
                OfferUpdateDto = _mapper.Map<OfferDto, OfferUpdateDto>(await _offerApiService.GetByIDAsync(token, id))
            };

            TempData["ID"] = id;
            return View(vM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(OfferUpdateDto offerUpdateDto)
        {
            if ((int)TempData["ID"] == offerUpdateDto.ID)
            {
                string token = HttpContext.Session.GetString("token");

                await _offerApiService.UpdateAsync(token,offerUpdateDto);

                return RedirectToAction("Index");
            }

            ViewBag.Fail = "Girilen ID Yanlış";
            return View(offerUpdateDto);
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("token");

            await _offerApiService.DeleteAsync(token,id);

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

            vM.Offer.IsApproved=true;
            OfferApprovalDto offerApprovalDto = _mapper.Map<Offer, OfferApprovalDto>(vM.Offer);

            await _offerApiService.OfferApprovalAsync(token,offerApprovalDto);

            return RedirectToAction("Index");
        }



    }
}

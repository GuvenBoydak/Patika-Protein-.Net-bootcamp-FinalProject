using AutoMapper;
using FinalProject.Base;
using FinalProject.Business;
using FinalProject.DTO;
using FinalProject.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Security.Claims;

namespace FinalProject.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : CustomBaseController
    {
        private readonly IOfferService _offerService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OffersController(IOfferService offerService, IMapper mapper, IProductService productService)
        {
            _offerService = offerService;
            _mapper = mapper;
            _productService = productService;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<Offer> offers = await _offerService.GetAllAsync();

            List<OfferListDto> offerListDtos = _mapper.Map<List<OfferListDto>>(offers);

            return CreateActionResult(CustomResponseDto<List<OfferListDto>>.Success(200,offerListDtos,"Tüm Teklifler Listelendi"));
        }


        [HttpGet("GetActive")]
        public async Task<IActionResult> GetActiveAsync()
        {
            List<Offer> offers = await _offerService.GetActiveAsync();

            List<OfferListDto> offerListDtos = _mapper.Map<List<OfferListDto>>(offers);

            return CreateActionResult(CustomResponseDto<List<OfferListDto>>.Success(200, offerListDtos, "Active Teklifler Listelendi"));
        }

        [HttpGet("GetPassive")]
        public async Task<IActionResult> GetPassiveAsync()
        {
            List<Offer> offers = await _offerService.GetPassiveAsync();

            List<OfferListDto> offerListDtos = _mapper.Map<List<OfferListDto>>(offers);

            return CreateActionResult(CustomResponseDto<List<OfferListDto>>.Success(200, offerListDtos, "Passive Teklifler Listelendi"));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
        {
            Offer offer =await _offerService.GetByIDAsync(id);

            OfferDto offerDto = _mapper.Map<OfferDto>(offer);

            return CreateActionResult(CustomResponseDto<OfferDto>.Success(200, offerDto, $"{id} numaralı teklif Listelendi"));
        }

        [HttpGet]
        [Route("GetByAppUserOffers/{appUserId}")]
        public async Task<IActionResult> GetByAppUserOffersAsync(int appUserId)
        {
            List<Offer> offers = await _offerService.GetByAppUserOffersAsync(appUserId);

            List<OfferListDto> offerListDtos = _mapper.Map<List<Offer>, List<OfferListDto>>(offers);

            return CreateActionResult(CustomResponseDto<List<OfferListDto>>.Success(200,offerListDtos,"Kulanıcının yaptıgı Teklifleri Listelendi."));
        }

        [HttpGet]
        [Route("GetByOffersProductID/{productId}")]
        public async Task<IActionResult> GetByOffersProductIDAsync(int productId)
        {
            List<Offer> offers = await _offerService.GetByOffersProductIDAsync(productId);

            List<ProductOffersListDto> productOffersListDto = _mapper.Map<List<Offer>, List<ProductOffersListDto>>(offers);

            return CreateActionResult(CustomResponseDto<List<ProductOffersListDto>>.Success(200, productOffersListDto, "Girilen Ürüne göre Teklifler Listelendi."));
        }

        [HttpGet]
        [Route("GetByAppUserProductsOffers/{appUserId}")]
        public async Task<IActionResult> GetByAppUserProductsOffersAsync(int appUserId)
        {
            List<Product> products = await _offerService.GetByAppUserProductsOffersAsync(appUserId);

            List<AppUserProductsOfferListDto> listDtos = _mapper.Map<List<Product>, List<AppUserProductsOfferListDto>>(products);

            return CreateActionResult(CustomResponseDto<List<AppUserProductsOfferListDto>>.Success(200, listDtos, "Kullanıcı ürünlerinin teklifleri Listelendi"));
        }


        [HttpPost]
        [Route("BuyProduct")]
        public async Task<IActionResult> BuyProductAsync([FromBody]OfferBuyProductDto offerBuyProductDto)
        {
            Log.Information($"{User.Identity?.Name}: BuyProduct a Offer  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Offer offer = _mapper.Map<OfferBuyProductDto, Offer>(offerBuyProductDto);

            await _offerService.BuyProductAsync(offer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Satın alma başarıyla Onaylandı."));
        }

        [HttpPost]
        [Route("OfferApproval")]
        public async Task<IActionResult> OfferApprovalAsync([FromBody]OfferApprovalDto offerApprovalDto)
        {
            Log.Information($"{User.Identity?.Name}: OfferApproval a Offer  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Offer offer = _mapper.Map<OfferApprovalDto, Offer>(offerApprovalDto);

            await _offerService.OfferApprovalAsync(offer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Teklif başarıyla Onaylandı."));
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]OfferAddDto offerAddDto)
        {
            Log.Information($"{User.Identity?.Name}: Add a Offer  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Product product = await _productService.GetByIDAsync(offerAddDto.ProductID);
            if (product.IsOfferable == false)
            return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404,"Bu Ürününe teklif verilemiyor."));

            Offer offer =_mapper.Map<Offer>(offerAddDto);

            _offerService.Add(offer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Eklem İşlemi Başarılı"));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody]OfferUpdateDto offerUpdateDto)
        {
            Log.Information($"{User.Identity?.Name}: Update a Offer  AppUserID is {(User.Identity as ClaimsIdentity).FindFirst("AppUserId").Value}.");

            Offer offer = _mapper.Map<Offer>(offerUpdateDto);

          await  _offerService.UpdateAsync(offer);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204, "Güncelleme İşlemi Başarılı"));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute]int id)
        {
           await _offerService.DeleteAsync(id);

            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204,"Silme İşlemi Başarılı"));
        }


    }
}

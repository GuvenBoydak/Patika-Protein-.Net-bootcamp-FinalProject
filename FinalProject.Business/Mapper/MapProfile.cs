using AutoMapper;
using FinalProject.DTO;
using FinalProject.Entities;
using FinalProject.Base;


namespace FinalProject.Business
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryListDto>().ReverseMap();
            CreateMap<Category, CategoryUpdateDto>().ReverseMap();
            CreateMap<Category, CategoryWithProductsDto>().ReverseMap();
            CreateMap<CategoryDto, CategoryUpdateDto>().ReverseMap();


            CreateMap<Product, ProductUpdateDto>().ReverseMap();
            CreateMap<Product, ProductAddDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap();
            CreateMap<Product, AppUserProductsOfferListDto>().ReverseMap();
            CreateMap<Product, AppUserProductsDto>().ReverseMap();
            CreateMap<ProductAddDto, ProductAddWitFileDto>().ReverseMap();

            CreateMap<Brand, BrandListDto>().ReverseMap();
            CreateMap<Brand, BrandDto>().ReverseMap();
            CreateMap<Brand, BrandAddDto>().ReverseMap();
            CreateMap<Brand, BrandUpdateDto>().ReverseMap();
            CreateMap<BrandDto, BrandUpdateDto>().ReverseMap();

            CreateMap<Color, ColorListDto>().ReverseMap();
            CreateMap<Color, ColorDto>().ReverseMap();
            CreateMap<Color, ColorAddDto>().ReverseMap();
            CreateMap<Color, ColorUpdateDto>().ReverseMap();
            CreateMap<ColorDto, ColorUpdateDto>().ReverseMap();

            CreateMap<Offer, OfferListDto>().ReverseMap();
            CreateMap<Offer, OfferDto>().ReverseMap();
            CreateMap<Offer, OfferAddDto>().ReverseMap();
            CreateMap<Offer, OfferUpdateDto>().ReverseMap();
            CreateMap<Offer, OfferApprovalDto>().ReverseMap();
            CreateMap<Offer, OfferBuyProductDto>().ReverseMap();
            CreateMap<Offer, ProductOffersListDto>().ReverseMap();
            CreateMap<OfferDto, OfferUpdateDto>().ReverseMap();
            CreateMap<Offer, AppUserProductsOfferListDto>().ReverseMap().ForMember(x => x.Price, x => x.MapFrom(x => x.UnitPrice));


            CreateMap<AppUser, AppUserListDto>().ReverseMap();
            CreateMap<AppUser, AppUserDto>().ReverseMap();
            CreateMap<AppUser, AppUserUpdateDto>().ReverseMap();
            CreateMap<AppUser, AppUserWithOffersDto>().ReverseMap();

            CreateMap<AppUserRole, AppUserRoleDto>().ReverseMap();
            CreateMap<AppUserRole, AppUserRoleListDto>().ReverseMap();
            CreateMap<AppUserRole, AppUserRoleAddDto>().ReverseMap();
            CreateMap<AppUserRole, AppUserRoleUpdateDto>().ReverseMap();

            CreateMap<Role, RoleUpdateDto>().ReverseMap();
            CreateMap<Role, RoleAddDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Role, RoleListDto>().ReverseMap();




        }
    }
}

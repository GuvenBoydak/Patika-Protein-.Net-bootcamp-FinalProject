using AutoMapper;

namespace FinalProject.MVCUI
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<ProductModel, ProductAddWithFileModel>().ReverseMap();
        }
    }
}

using AutoMapper;
using FinalProject.DTO;
using FinalProject.Entities;

namespace FinalProject.Business
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            CreateMap<Category, CategoryAddDto>().ReverseMap();
        }
    }
}

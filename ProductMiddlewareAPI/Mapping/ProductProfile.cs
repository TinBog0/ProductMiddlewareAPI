using AutoMapper;
using ProductMiddlewareAPI.ViewModels;
using ProductMiddlewareDataAcces.Models;

namespace ProductMiddlewareAPI.Mapping
{
    public class ProductProfile : Profile
    {
        public ProductProfile() 
        {
            CreateMap<Product, ProductVM>()
                .ForMember(dest => dest.ShortDescription, opt => opt.MapFrom(src => src.Description.Length > 100 ? src.Description.Substring(0, 100) + "..." : src.Description));
        }
    }
}

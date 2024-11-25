using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Pl.DTO;

namespace Talabat.Pl.Helper
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                //السطرين اللي تحت دول علشان اعدل بس في شكل الداتا 
            .ForMember(P => P.ProductBrand, B => B.MapFrom(S => S.ProductBrand.Name))
                .ForMember(P => P.ProductType, O => O.MapFrom(S => S.ProductType.Name))
                //Maooing السطر علشان اعمل 
                .ForMember(P => P.PictureUrl, O => O.MapFrom<PictureUrlResolver>());
        }
    }
}

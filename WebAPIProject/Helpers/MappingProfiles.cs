using AutoMapper;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Order_Aggregate;
using TalabatAPIS.Dtos;

namespace TalabatAPIS.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(D => D.ProductBrand , O => O.MapFrom(S => S.ProductBrand.Name))
                .ForMember(D => D.ProductType , O => O.MapFrom(S => S.ProductType.Name))
                .ForMember(D => D.PictureUrl , O => O.MapFrom<ProductPictureUrlResolver>());

            CreateMap<Talabat.Core.Entities.Order_Aggregate.Address, AddressDto>().ReverseMap();
            
            CreateMap<Talabat.Core.Entities.Identity.Address, AddressDto>().ReverseMap();
            
            CreateMap<CustomerBasketDto, CustomerBasket>().ReverseMap();
            
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            
            CreateMap<Order,OrderToReturnDto>()
                .ForMember(d => d.DeliveryMethod ,O => O.MapFrom(S => S.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryMethodCost,O =>O.MapFrom(S => S.DeliveryMethod.Cost));

            CreateMap<OrderItem,OrderItemDto>()
                .ForMember(d => d.ProductId,O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(d => d.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(d => d.PictureUrl,O => O.MapFrom(S => S.Product.PictureUrl))
                .ForMember(d => d.PictureUrl, O => O.MapFrom<OrderItemPictureUrlResolver>());
        }
    }
}

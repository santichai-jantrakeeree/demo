using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Core.Entities.Orders;

namespace WebMvc.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name))
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>());

            CreateMap<ProductDto, Product>();

            CreateMap<Core.Entities.Identities.Address, AddressDto>().ReverseMap();
            CreateMap<BasketDto, Basket>().ReverseMap();
            CreateMap<BasketItemDto, BasketItem>().ReverseMap();
            CreateMap<AddressDto, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderDto>()
                .ForMember(d => d.DeliveryMethodName, o => o.MapFrom(s => s.DeliveryInfo.Name))
                .ForMember(d => d.DeliveryMethodId, o => o.MapFrom(s => s.DeliveryInfo.Id))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryInfo.Price)).ReverseMap();
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();

            CreateMap<Order, OrderListDto>()
                .ForMember(d => d.DeliveryMethodName, o => o.MapFrom(s => s.DeliveryInfo.Name))
                .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryInfo.Price));

            CreateMap<ProductBrand, ProductBrandDto>().ReverseMap();
            CreateMap<ProductType, ProductTypeDto>().ReverseMap();
            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();

            CreateMap<Product, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Id))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlToOrderItemResolver>()).ReverseMap();

            CreateMap<DeliveryMethod, DeliveryInfo>();
        }
    }
}
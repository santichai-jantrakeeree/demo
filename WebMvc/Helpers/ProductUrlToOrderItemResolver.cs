using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace WebMvc.Helpers
{
    public class ProductUrlToOrderItemResolver : IValueResolver<Product, OrderItemDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlToOrderItemResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Product source, OrderItemDto destination, string destMember,
            ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _config["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
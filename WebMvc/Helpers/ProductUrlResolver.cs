using AutoMapper;
using Core.Dtos;
using Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace WebMvc.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductDto, string>
    {
        private readonly IWebHostEnvironment _environment;

        public ProductUrlResolver(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string Resolve(Product source, ProductDto destination, string destMember,
            ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return Path.Combine("~/images/products/", source.PictureUrl);
            }

            return null;
        }
    }
}
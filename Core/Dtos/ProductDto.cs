using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Dtos
{
    public class ProductDto : IDto<int>
    {
        public ProductDto()
        {
            PictureChanged = true;
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        
        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Product Type")]
        public int ProductTypeId { get; set; }
        public string ProductType { get; set; }

        [UIHint("DropDownList")]
        [Required]
        [DisplayName("Product Brand")]
        public int ProductBrandId { get; set; }
        public string ProductBrand { get; set; }

        [UIHint("FileUpload")]
        public IFormFile PictureFile { get; set; }
        public string PictureUrl { get; set; }
        public bool PictureChanged { get; set; }
    }
}
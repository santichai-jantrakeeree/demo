using Core.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Interfaces;

namespace WebMvc.Models.PageModel
{
    public class ProductPageDetailModel : BasePageModel<ProductDto, int>
    {
        public ProductPageDetailModel()
        {
            ProductTypeSelectList = new List<SelectListItem>();
            ProductBrandSelectList = new List<SelectListItem>();
        }

        public IList<SelectListItem> ProductTypeSelectList { get; set; }
        public IList<SelectListItem> ProductBrandSelectList { get; set; }
    }
}

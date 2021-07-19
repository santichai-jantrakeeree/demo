using Core.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Interfaces;

namespace WebMvc.Models.PageModel
{
    public class CheckoutPageModel
    {
        public OrderDto Model { get; set; }
        public IList<DeliveryMethodDto> DeliveryMethod { get; set; }
    }
}

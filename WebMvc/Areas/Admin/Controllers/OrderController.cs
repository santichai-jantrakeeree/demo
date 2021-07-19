using AutoMapper;
using Core.Data;
using Core.Dtos;
using Core.Entities;
using Core.Entities.Orders;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebMvc.Controllers;
using WebMvc.Models.PageModel;

namespace WebMvc.Areas.Admin.Controllers
{    
    [Authorize("Admin")]
    [Area("Admin")]
    public class OrderController : BaseDbController<
        IOrderService, 
        IOrderRepository,
        OrderPageDetailModel,
        Order, 
        OrderDto, 
        int>
    {
        public OrderController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }

        public async Task<ActionResult<IList<OrderListDto>>> List2([FromQuery] DataSource source)
        {
            var result = await Service.List(source);
            DataResult<OrderListDto> dataResult = new DataResult<OrderListDto>
            {
                PageIndex = result.PageIndex,
                Count = result.Count,
                Data = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderListDto>>(result.Data),
                PageSize = result.PageSize
            };
            return Ok(dataResult);
        }
    }
}

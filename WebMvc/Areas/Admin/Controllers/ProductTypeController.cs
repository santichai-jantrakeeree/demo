using AutoMapper;
using Core.Dtos;
using Core.Entities;
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
    public class ProductTypeController : BaseDbController<
        IProductTypeService, 
        IProductTypeRepository,
        ProductTypePageDetailModel,
        ProductType, 
        ProductTypeDto, 
        int>
    {
        public ProductTypeController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
    }
}

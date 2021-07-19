using AutoMapper;
using Core.Data;
using Core.Dtos;
using Core.Entities;
using Core.Interfaces;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Areas.Shop.Models;
using WebMvc.Models.PageModel;

namespace WebMvc.Areas.Shop.Controllers
{
    [Area("Shop")]
    public class ShopController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;

        public ShopController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _environment = environment;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            ProductPageDetailModel pageModel = new ProductPageDetailModel();

            IProductTypeService typeService = _unitOfWork.GetService(typeof(IProductTypeService)) as IProductTypeService;
            var types = await typeService.List();
            pageModel.ProductTypeSelectList.Add(new SelectListItem("All Types", "", true));
            foreach (var t in types)
            {
                pageModel.ProductTypeSelectList.Add(new SelectListItem(t.Name, t.Id.ToString()));
            }

            IProductBrandService brandService = _unitOfWork.GetService(typeof(IProductBrandService)) as IProductBrandService;
            var brands = await brandService.List();
            pageModel.ProductBrandSelectList.Add(new SelectListItem("All Brands", "", true));
            foreach (var b in brands)
            {
                pageModel.ProductBrandSelectList.Add(new SelectListItem(b.Name, b.Id.ToString()));
            }

            return View(pageModel);
        }

        public async Task<ActionResult<ProductDto>> Detail(int id)
        {
            IProductService service = _unitOfWork.GetService(typeof(IProductService)) as IProductService;
            var item = await service.Find(id);
            var ret = _mapper.Map<Product, ProductDto>(item);
            return View(ret);
        }

        public async Task<ActionResult<DataResult<ProductDto>>> List([FromQuery] DataSource source, int brandId, int typeId, string search)
        {
            System.Threading.Thread.Sleep(1000);
            IProductService service = _unitOfWork.GetService(typeof(IProductService)) as IProductService;
            var result = await service.List(source, brandId, typeId, search);
            ProductListPageModel dataResult = new ProductListPageModel
            {
                PageIndex = result.PageIndex,
                Count = result.Count,
                Data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDto>>(result.Data),
                PageSize = result.PageSize
            };
            return PartialView(dataResult);
        }

        public IActionResult Basket()
        {
            return View();
        }

        public async Task<IActionResult> GetProducts([FromQuery(Name = "ids[]")] int[] ids)
        {
            if(ids != null && ids.Length > 0)
            {
                IProductService service = _unitOfWork.GetService(typeof(IProductService)) as IProductService;
                return Ok(await service.List(ids));
            }
            return Ok();
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            CheckoutPageModel pageModel = new CheckoutPageModel();
            pageModel.Model = new OrderDto();

            var deliveryMethodService = _unitOfWork.GetService(typeof(IDeliveryMethodService)) as IDeliveryMethodService;
            var deliveryItems = await deliveryMethodService.List();

            pageModel.DeliveryMethod = _mapper.Map<IList<DeliveryMethod>, IList<DeliveryMethodDto>>(deliveryItems);
            return View(pageModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SubmitOrder([Bind(Prefix = "Model")]OrderDto dto)
        {
            if (ModelState.IsValid)
            {
                var orderService = _unitOfWork.GetService(typeof(IOrderService)) as IOrderService;
                var ret = await orderService.CreateOrder(dto);
                await _unitOfWork.Commit();
                return Ok(ret);
            }
            return NotFound();
        }
    }
}

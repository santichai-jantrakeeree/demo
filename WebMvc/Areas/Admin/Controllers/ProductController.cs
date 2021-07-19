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
using WebMvc.Enums;
using WebMvc.Models.PageModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using AutoMapper;

namespace WebMvc.Areas.Admin.Controllers
{
    [Authorize("Admin")]
    [Area("Admin")]
    public class ProductController : BaseDbController<
        IProductService, 
        IProductRepository,
        ProductPageDetailModel,
        Product, 
        ProductDto, 
        int>
    {
        private readonly IWebHostEnvironment _environment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment environment, IMapper mapper) : base(unitOfWork, mapper)
        {
            _environment = environment;
        }

        public override async Task<ActionResult<ProductPageDetailModel>> Details(int id)
        {
            System.Threading.Thread.Sleep(1000);
            ProductPageDetailModel pageModel = new ProductPageDetailModel();

            IProductTypeService typeService = _unitOfWork.GetService(typeof(IProductTypeService)) as IProductTypeService;
            var types = await typeService.List();
            foreach (var t in types)
            {
                pageModel.ProductTypeSelectList.Add(new SelectListItem(t.Name, t.Id.ToString()));
            }

            IProductBrandService brandService = _unitOfWork.GetService(typeof(IProductBrandService)) as IProductBrandService;
            var brands = await brandService.List();
            foreach (var b in brands)
            {
                pageModel.ProductBrandSelectList.Add(new SelectListItem(b.Name, b.Id.ToString()));
            }

            if (id.Equals(default(int)))
            {
                pageModel.Model = new ProductDto();
                pageModel.PageMode = PageMode.Create;
            }
            else
            {
                var entity = await Service.Find(id);
                pageModel.Model = _mapper.Map<Product, ProductDto>(entity);
                pageModel.PageMode = PageMode.Edit;
            }

            return PartialView(pageModel);
        }

        public override async Task<ActionResult> Create([Bind(Prefix = "Model")] ProductDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (dto.PictureFile != null)
                    {
                        string wwwrootPath = _environment.WebRootPath;
                        string contentPath = "images/products";
                        dto.PictureUrl = $"{Guid.NewGuid()}.{GetExtensions(dto.PictureFile.FileName)}";
                        using (FileStream stream = new FileStream(Path.Combine(wwwrootPath, contentPath, dto.PictureUrl), FileMode.Create))
                        {
                            dto.PictureFile.CopyTo(stream);
                            await stream.FlushAsync();
                        }
                    }
                    var entity = Map(dto);
                    Service.Add(entity);
                    await _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok();
        }

        public override async Task<ActionResult> Delete(int id)
        {
            var entity = await Service.Delete(id);
            string wwwrootPath = _environment.WebRootPath;
            string contentPath = "images/products";
            string path = Path.Combine(wwwrootPath, contentPath, entity.PictureUrl);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            await _unitOfWork.Commit();
            return Ok();
        }

        public override async Task<ActionResult> Edit([Bind(Prefix = "Model")] ProductDto dto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var entity = await Service.Find(dto.Id);
                    if (dto.PictureFile != null)
                    {
                        string wwwrootPath = _environment.WebRootPath;
                        string contentPath = "images/products";
                        dto.PictureUrl = $"{Guid.NewGuid()}.{GetExtensions(dto.PictureFile.FileName)}";
                        using (FileStream stream = new FileStream(Path.Combine(wwwrootPath, contentPath, dto.PictureUrl), FileMode.Create))
                        {
                            dto.PictureFile.CopyTo(stream);
                            await stream.FlushAsync();
                        }

                        var currentPicturePath = Path.Combine(wwwrootPath, contentPath, entity.PictureUrl);
                        if (System.IO.File.Exists(currentPicturePath))
                        {
                            System.IO.File.Delete(currentPicturePath);
                        }
                    }
                    var target = Map(dto, entity);
                    var ret = Service.Update(target);
                    await _unitOfWork.Commit();
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }
            }

            return Ok();
        }

        protected string GetExtensions(string fileName)
        {
            string ret = string.Empty;
            
            if (!String.IsNullOrEmpty(fileName))
            {
                int lastInx = fileName.LastIndexOf('.');
                if(lastInx > default(int) && lastInx < fileName.Length)
                {
                    int startWith = lastInx + 1;
                    ret = fileName.Substring(startWith, fileName.Length - startWith);
                }
            }
            return ret;
        }
    }
}

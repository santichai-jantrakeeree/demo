using AutoMapper;
using Core.Data;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Enums;
using WebMvc.Interfaces;

namespace WebMvc.Controllers
{
    public class BaseDbController<TService, TRepository, TPageModel, TEntity, TDto, TKey> : Controller
        where TService : class, IDbService<TKey, TEntity, TDto, TRepository>
        where TRepository : class, IDbRepository<TEntity, TDto, TKey>
        where TPageModel : class, IPageModel<TDto, TKey>, new()
        where TEntity : class, IEntity<TKey>, new()
        where TDto : class, IDto<TKey>, new()
        where TKey : IEquatable<TKey>

    {
        private TService _service;
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseDbController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        protected TService Service
        {
            get
            {
                if (_service == null)
                {
                    _service = _unitOfWork.GetService(typeof(TService)) as TService;
                }
                return _service;
            }
        }

        public virtual IActionResult Index()
        {
            return View(new TDto());
        }

        public virtual async Task<ActionResult<DataResult<TDto>>> List([FromQuery] DataSource source)
        {
            System.Threading.Thread.Sleep(1000);
            try
            {
                var result = await Service.List(source);
                DataResult<TDto> dataResult = new DataResult<TDto>
                {
                    PageIndex = result.PageIndex,
                    Count = result.Count,
                    Data = _mapper.Map<IReadOnlyList<TEntity>, IReadOnlyList<TDto>>(result.Data),
                    PageSize = result.PageSize
                };

                return Ok(dataResult);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [ActionName("items")]
        public virtual async Task<ActionResult<IReadOnlyList<TDto>>> List()
        {
            try
            {
                var ret = await Service.List();
                return Ok(ret);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        public virtual async Task<ActionResult<TPageModel>> Details(TKey id)
        {
            try
            {
                System.Threading.Thread.Sleep(1000);
                TPageModel pageModel = new TPageModel();

                if (id.Equals(default(int)))
                {
                    pageModel.Model = new TDto();
                    pageModel.PageMode = PageMode.Create;
                }
                else
                {
                    var entity = await Service.Find(id);
                    pageModel.Model = Map(entity);
                    pageModel.PageMode = PageMode.Edit;
                }

                return PartialView(pageModel);
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        public virtual async Task<ActionResult> Create([Bind(Prefix = "Model")] TDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = Map(dto);
                    Service.Add(entity);
                    await _unitOfWork.Commit();
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Edit([Bind(Prefix = "Model")] TDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var entity = await Service.Find(dto.Id);
                    var target = Map(dto, entity);
                    Service.Update(target);
                    await _unitOfWork.Commit();
                }
                
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete]
        public virtual async Task<ActionResult> Delete(TKey id)
        {
            try
            {
                await Service.Delete(id);
                await _unitOfWork.Commit();
                return Ok();
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null && ex.InnerException.Message.StartsWith("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    return NotFound("Cannot delete this object. It's already in use.");
                }
                else
                {
                    return NotFound(ex.Message);
                }
            }
        }

        protected TEntity Map(TDto dto)
        {
            return _mapper.Map<TDto, TEntity>(dto);
        }

        protected TEntity Map(TDto dto, TEntity entity)
        {
            return _mapper.Map<TDto, TEntity>(dto, entity);
        }

        protected TDto Map(TEntity entity)
        {
            return _mapper.Map<TEntity, TDto>(entity);
        }
    }
}

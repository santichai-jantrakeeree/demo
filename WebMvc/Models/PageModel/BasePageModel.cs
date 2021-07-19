using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMvc.Enums;
using WebMvc.Interfaces;

namespace WebMvc.Models.PageModel
{
    public class BasePageModel<TDto, TKey> : IPageModel<TDto, TKey>
        where TDto : class, IDto<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        public BasePageModel()
        {

        }

        public BasePageModel(TDto model, PageMode pageMode)
        {
            Model = model;
            PageMode = pageMode;
        }

        public PageMode PageMode { get; set; }
        public TDto Model { get; set; }

    }
}

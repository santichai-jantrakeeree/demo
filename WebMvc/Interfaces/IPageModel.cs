using Core.Interfaces;
using System;
using WebMvc.Enums;

namespace WebMvc.Interfaces
{
    public interface IPageModel<TDto, TKey>
        where TDto : class, IDto<TKey>, new()
        where TKey : IEquatable<TKey>
    {
        PageMode PageMode { get; set; }
        TDto Model { get; set; }
    }
}

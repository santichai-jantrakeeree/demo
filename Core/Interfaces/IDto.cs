using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces
{
    public interface IDto<TKey>
        where TKey : IEquatable<TKey>
    {
        TKey Id { get; set; }
    }
}
